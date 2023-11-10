using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pj.DataAccess.Repository;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using pj.Utility;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer"), Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public ShoppingCartController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public async Task<IActionResult> Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var LCarts = await _unitOfWork.ShoppingCart.GetAllAsync(a => a.AppUserId == UserId, includeProperties: "Product");
            ShoppingCartVM = new()
            {
                ListCarts = LCarts.ToList(),
                OrderHead = new(),
                TotalBase = 0
            };
            foreach (var cart in ShoppingCartVM.ListCarts)
            {
                cart.price = PricenQuantityCal(cart);
                cart.currentprice = cart.price * cart.count;

                ShoppingCartVM.OrderHead.OrderTotal += (cart.price * cart.count);
                ShoppingCartVM.TotalBase += (cart.Product.ListPrice * cart.count);
            }
            return View(ShoppingCartVM);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ShoppingCartVM cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //     IEnumerable<ShoppingCart> car = cart.ListCarts.AsEnumerable();
            //      var filteredCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties: "Product").ToList();
            var cartsFromDb = await _unitOfWork.ShoppingCart.GetAllAsync(a => a.AppUserId == userId, includeProperties: "Product");

            ShoppingCartVM = new()
            {
                // ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId && a == cart.ListCarts[0].selected).ToList()
                //ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId && cart.ListCarts.Any(item => item.selected)).ToList(),
                //                ListCarts = filteredCarts.Where(item => cart.ListCarts.Any(c => c.Id == item.Id && c.selected)).ToList(),
                //ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties: "Product").ToList().Where(item => cart.ListCarts.Any(cart => cart.Id == item.Id && cart.selected)).ToList(),
                ListCarts = cartsFromDb.Where(c => cart.ListCarts.Any(cart => cart.Id == c.Id && cart.selected)).ToList(),
                OrderHead = new()
                {
                    AppUserId = userId,
                    AppUser = await _unitOfWork.AppUser.Get1Async(a => a.Id == userId),

                    OrderTotal = 0
                }
            };
            foreach (var carta in ShoppingCartVM.ListCarts)
            {
                carta.price = PricenQuantityCal(carta);
                carta.currentprice = carta.price * carta.count;

                ShoppingCartVM.OrderHead.OrderTotal += (carta.price * carta.count);
                ShoppingCartVM.TotalBase += (carta.Product.ListPrice * carta.count);
            }

            TempData["cart"] = JsonConvert.SerializeObject(ShoppingCartVM);

            //    TempData.Keep("cart");
            return RedirectToAction(nameof(Summary));
        }
      
       
        public async Task<IActionResult> Summary(OrderHead? head)
        {
            string? cartData = TempData.Peek("cart") as string;
            //  ShoppingCartVM? cart = JsonConvert.DeserializeObject(cartData) as ShoppingCartVM;
            ShoppingCartVM cart = JsonConvert.DeserializeObject<ShoppingCartVM>(cartData);
            ShoppingCartVM = new()
            {
                ListCarts = cart.ListCarts,
                OrderHead = cart.OrderHead,
                TotalBase = cart.TotalBase
            };
            if (head.Name != null)
            {
                OrderHead userData = head;
                await PopulateOrderHeadFromUserDataAsync(ShoppingCartVM.OrderHead, userData);
            }
            else
            {
                
                await PopulateOrderHeadFromAppUserAsync(ShoppingCartVM.OrderHead);
            }

            return View(ShoppingCartVM);
        }

        private async Task PopulateOrderHeadFromUserDataAsync(OrderHead orderHead, OrderHead userData)
        {
            orderHead.PhoneNumber = userData.PhoneNumber;
            orderHead.StreetAddress = userData.StreetAddress;
            orderHead.PostalCode = userData.PostalCode;
            orderHead.City = userData.City;
            orderHead.State = userData.State;
            orderHead.Name = userData.Name;
        }

        private async Task PopulateOrderHeadFromAppUserAsync(OrderHead orderHead)
        {
            orderHead.PhoneNumber = orderHead.AppUser.PhoneNumber;
            orderHead.StreetAddress = orderHead.AppUser.StreetAddress;
            orderHead.PostalCode = orderHead.AppUser.PostalCode;
            orderHead.City = orderHead.AppUser.City;
            orderHead.State = orderHead.AppUser.State;
            orderHead.Name = orderHead.AppUser.Name;
        }


        [HttpPost, ActionName("Summary")]
        public async Task<IActionResult> SummaryPost()
        {

            var claimsUser = (ClaimsIdentity)User.Identity;
            var userId = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartFromDb = await _unitOfWork.ShoppingCart.GetAllAsync(a => a.AppUserId == userId, includeProperties: "Product");
            //bad code, should get that sinle item instead of all the cart then filter it back
            IList<ShoppingCart> carts = cartFromDb.Where(items => ShoppingCartVM.ListCarts.Any(c => c.Id == items.Id)).ToList();

            ShoppingCartVM.ListCarts = carts;


            AppUser userObject = await _unitOfWork.AppUser.Get1Async(a => a.Id == userId);
            ShoppingCartVM.OrderHead.AppUserId = userId;

            ShoppingCartVM.OrderHead.OrderDate = DateTime.Now.AddHours(7);

            if (userObject.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartVM.OrderHead.OrderStatus = SD.StatusPending;
                ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusPending;
                //implement more logic here if the you want to have any different payment, cod (no logic) by default)
            }
            else
            {
                ShoppingCartVM.OrderHead.OrderStatus = SD.StatusApproved;
                ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHead.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }
            await _unitOfWork.OrderHead.AddAsync(ShoppingCartVM.OrderHead);
            foreach (var cart in carts)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            await _unitOfWork.SaveAsync();
            foreach (var cart in ShoppingCartVM.ListCarts)
            {
                cart.price = PricenQuantityCal(cart);
                ShoppingCartVM.OrderHead.OrderTotal += (cart.price * cart.count);
                OrderDetail detail = new()
                {
                    OrderHeadId = ShoppingCartVM.OrderHead.Id,
                    ProductId = cart.ProductId,
                    Count = cart.count,
                    Price = cart.price
                };


                await _unitOfWork.OrderDetail.AddAsync(detail);
            }

            await _unitOfWork.SaveAsync();


        
            
                var carts2 = await _unitOfWork.ShoppingCart.GetAllAsync(c => c.AppUserId == userId);
                int carC = carts2.Distinct().Count();
                HttpContext.Session.SetInt32(SD.SessionCart, carC);
           
            /* throwaway
             *         public int Id { get; set; }
    public int OrderHeadId { get; set; }
    [ForeignKey(nameof(OrderHeadId)), ValidateNever]
    public OrderHead OrderHead { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId)), ValidateNever]
    public Product Product { get; set; }
    public int Count { get; set; }

    public double Price { get; set; }

             */


            //     carts.Where()

            //    string? cartData = TempData.Peek("cart") as string;
            //  ShoppingCartVM? cart = JsonConvert.DeserializeObject(cartData) as ShoppingCartVM;
            //  ShoppingCartVM cart = JsonConvert.DeserializeObject<ShoppingCartVM>(cartData);

            //  return RedirectToAction(nameof(OrderDetail)
            return RedirectToAction("Details", "Order", new { area = "Admin", orderId = ShoppingCartVM.OrderHead.Id });
 

            //  return RedirectToAction(nameof(OrderConfirm), new { id = ShoppingCartVM.OrderHead.Id });
        }

        //#region Sync code
        //[HttpPost, ActionName("Sum5mary")]
        //public IActionResult Summar4yPost()
        //{

        //    var claimsUser = (ClaimsIdentity)User.Identity;
        //    var userId = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    IList<ShoppingCart> carts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties: "Product").ToList().Where(items => ShoppingCartVM.ListCarts.Any(c => c.Id == items.Id)).ToList();

        //    ShoppingCartVM.ListCarts = carts;

        //    AppUser userObject = _unitOfWork.AppUser.Get1(a => a.Id == userId);
        //    ShoppingCartVM.OrderHead.AppUserId = userId;

        //    foreach (var cart in ShoppingCartVM.ListCarts)
        //    {
        //        cart.price = PricenQuantityCal(cart);
        //        ShoppingCartVM.OrderHead.OrderTotal += (cart.price * cart.count);
        //    }

        //    ShoppingCartVM.OrderHead.OrderDate = DateTime.Now;

        //    if (userObject.CompanyId.GetValueOrDefault() == 0)
        //    {
        //        ShoppingCartVM.OrderHead.OrderStatus = SD.StatusPending;
        //        ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusPending;
        //        //implement more logic here if the you want to have any different payment, cod (no logic) by default)
        //    }
        //    else
        //    {
        //        ShoppingCartVM.OrderHead.OrderStatus = SD.StatusApproved;
        //        ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusDelayedPayment;
        //        ShoppingCartVM.OrderHead.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
        //    }
        //    _unitOfWork.OrderHead.Add(ShoppingCartVM.OrderHead);
        //    _unitOfWork.save();

        //    foreach (var cart in ShoppingCartVM.ListCarts)
        //    {
        //        OrderDetail detail = new()
        //        {
        //            OrderHeadId = ShoppingCartVM.OrderHead.Id,
        //            ProductId = cart.ProductId,
        //            Count = cart.count,
        //            Price = cart.price
        //        };
        //        _unitOfWork.OrderDetail.Add(detail);

        //    }
        //    _unitOfWork.save();
        //    return RedirectToAction(nameof(OrderConfirm), new { id = ShoppingCartVM.OrderHead.Id });
        //}



        public async Task<IActionResult> OrderConfirm(int id)
        {
            OrderHead head = await _unitOfWork.OrderHead.Get1Async(a => a.Id == id);
            if (head == null)
            {
                return NotFound();
            }
            if ((head.PaymentStatus != SD.PaymentStatusApprovedCOD) || (head.PaymentStatus != SD.PaymentStatusDelayedPayment))
            {
                //this section for implement other payment method that not cod
                //check if user's payment isn't cod nor its a company user, company can pay later
           //     return RedirectToAction("insert action here");
            }

            return View(id);
        }
        //this is for changing address 
        public async Task<IActionResult> ShippingDetails()
        {
            var claimsUser = (ClaimsIdentity)User.Identity;
            var userId = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
          //  var cart = await _unitOfWork.ShoppingCart.GetAllAsync(a => a.AppUser.Id == userId);
            AppUser user = await _unitOfWork.AppUser.Get1Async(a => a.Id == userId);
            if (user == null)
            {

                return NotFound();
            }
            OrderHead order = new OrderHead()
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode

            };
            return View(order);
        }

        [HttpPost]
        public IActionResult ShippingDetails(OrderHead head)
        {


            //   TempData["UserEditData"] = JsonConvert.SerializeObject(head);
            return RedirectToAction(nameof(Summary), head);


        }

        private double PricenQuantityCal(ShoppingCart cart)
        {
            if (cart.count <= 50)
            {
                return cart.Product.Price;
            }
            else
            {
                if (cart.count <= 100)
                {
                    return cart.Product.Price50;
                }
                else return cart.Product.Price100;
            }
        }

        /*  public IActionResult More(int? id)
          {
              if (id == null || id == 0) return NotFound();
              ShoppingCart cart = _unitOfWork.ShoppingCart.Get1(a => a.Id == id);
              if(cart.count <=1000)
              {
                  cart.count++;
                  _unitOfWork.save();
                  return RedirectToAction(nameof(Index));
              }
              else
              {

              }

          }*/

        public async Task<IActionResult> More(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = await _unitOfWork.ShoppingCart.Get1Async(c => c.Id == cartid);
            if (cart.count < 1000) cart.count++;
            else
            {
                TempData["error"] = "Quantity cant larger than 1000";
            }
             _unitOfWork.ShoppingCart.Update(cart);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Less(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = await _unitOfWork.ShoppingCart.Get1Async(c => c.Id == cartid);
            if (cart.count > 1) cart.count--;
            else
            {
                TempData["error"] = "Quantity cant smaller than 1";
            }
            _unitOfWork.ShoppingCart.Update(cart);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteOne(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = await _unitOfWork.ShoppingCart.Get1Async(a => a.Id == cartid);

            _unitOfWork.ShoppingCart.Remove(cart);
            await _unitOfWork.SaveAsync();

         
            var carts = await _unitOfWork.ShoppingCart.GetAllAsync(c => c.AppUserId == cart.AppUserId);
            int carC = carts.Distinct().Count();
            HttpContext.Session.SetInt32(SD.SessionCart, carC);

            TempData["success"] = "Delete item successfully";
            return RedirectToAction(nameof(Index));
        }
        //still need mass delete, probably later if i remember

    }
}
