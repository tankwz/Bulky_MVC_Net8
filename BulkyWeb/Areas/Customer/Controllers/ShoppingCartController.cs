using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public ShoppingCartController( IUnitOfWork unit )
        {
            _unitOfWork = unit;
        }
        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == UserId, includeProperties: "Product").ToList(),
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
        public IActionResult Index(ShoppingCartVM cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //     IEnumerable<ShoppingCart> car = cart.ListCarts.AsEnumerable();
      //      var filteredCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties: "Product").ToList();

            ShoppingCartVM = new()
            {
                // ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId && a == cart.ListCarts[0].selected).ToList()
                //ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId && cart.ListCarts.Any(item => item.selected)).ToList(),
                //                ListCarts = filteredCarts.Where(item => cart.ListCarts.Any(c => c.Id == item.Id && c.selected)).ToList(),
            //    ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId && cart.ListCarts.Any(c => c.Id == a.Id && c.selected), includeProperties: "Product").ToList(),
            ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties: "Product").ToList().Where(item => cart.ListCarts.Any(cart => cart.Id == item.Id && cart.selected)).ToList(),
            OrderHead = new()
                {
                    AppUserId = userId,
                    AppUser = _unitOfWork.AppUser.Get1(a => a.Id == userId),

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
        public IActionResult Summary(OrderHead? head)
        {
            string? cartData = TempData.Peek("cart") as string;
            //  ShoppingCartVM? cart = JsonConvert.DeserializeObject(cartData) as ShoppingCartVM;
            ShoppingCartVM  cart = JsonConvert.DeserializeObject<ShoppingCartVM>(cartData);
            ShoppingCartVM = new()
            {
                ListCarts = cart.ListCarts,
                OrderHead = cart.OrderHead,
                TotalBase = cart.TotalBase
            };




           // if (TempData.Peek("UserEditData") != null)
            if(head.Name != null)
            {
                // string userDataString = TempData.Peek("UserEditData") as string;
                OrderHead userData = head; // JsonConvert.DeserializeObject<OrderHead>(userDataString);

                ShoppingCartVM.OrderHead.PhoneNumber = userData.PhoneNumber;
                ShoppingCartVM.OrderHead.StreetAddress = userData.StreetAddress;
                ShoppingCartVM.OrderHead.PostalCode = userData.PostalCode;
                ShoppingCartVM.OrderHead.City = userData.City;
                ShoppingCartVM.OrderHead.State = userData.State;
                ShoppingCartVM.OrderHead.Name = userData.Name;
            }
            else
            {

                ShoppingCartVM.OrderHead.PhoneNumber = ShoppingCartVM.OrderHead.AppUser.PhoneNumber;
                ShoppingCartVM.OrderHead.StreetAddress = ShoppingCartVM.OrderHead.AppUser.StreetAddress;
                ShoppingCartVM.OrderHead.PostalCode = ShoppingCartVM.OrderHead.AppUser.PostalCode;
                ShoppingCartVM.OrderHead.City = ShoppingCartVM.OrderHead.AppUser.City;
                ShoppingCartVM.OrderHead.State = ShoppingCartVM.OrderHead.AppUser.State;
                ShoppingCartVM.OrderHead.Name = ShoppingCartVM.OrderHead.AppUser.Name;
            }
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(ShoppingCartVM);
        }
    

        [HttpPost,ActionName("Summary")]
        public IActionResult SummaryPost()
        {

            var claimsUser = (ClaimsIdentity)User.Identity;
            var userId = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            IList<ShoppingCart> carts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == userId, includeProperties:"Product").ToList().Where(items => ShoppingCartVM.ListCarts.Any(c=> c.Id == items.Id)).ToList();

            ShoppingCartVM.ListCarts = carts;

            ShoppingCartVM.OrderHead.AppUser = _unitOfWork.AppUser.Get1(a => a.Id == userId);
            ShoppingCartVM.OrderHead.AppUserId = userId;

            foreach (var cart in ShoppingCartVM.ListCarts)
            {
                cart.price = PricenQuantityCal(cart);
                ShoppingCartVM.OrderHead.OrderTotal += (cart.price * cart.count);
            }

            ShoppingCartVM.OrderHead.OrderDate = DateTime.Now;
            
            if(ShoppingCartVM.OrderHead.AppUser.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartVM.OrderHead.OrderStatus = SD.StatusPending;
                ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusPending;
            }
            else
            {
                ShoppingCartVM.OrderHead.OrderStatus = SD.StatusApproved;
                ShoppingCartVM.OrderHead.PaymentStatus = SD.PaymentStatusDelayedPayment;
            }
            _unitOfWork.OrderHead.Add(ShoppingCartVM.OrderHead);
            _unitOfWork.save();

            foreach (var cart in ShoppingCartVM.ListCarts)
            {
                OrderDetail detail = new()
                {
                    OrderHeadId = ShoppingCartVM.OrderHead.Id,
                    ProductId = cart.ProductId,
                    Count = cart.count,
                    Price = cart.price
                };
                _unitOfWork.OrderDetail.Add(detail);
                _unitOfWork.save();
            }

                /*
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
                return RedirectToAction(nameof(OrderConfirm), ShoppingCartVM.OrderHead.Id);
        }
        public IActionResult OrderConfirm(int id)
        {
            return View(id);
        }
        public IActionResult ShippingDetails()
        {
            var claimsUser = (ClaimsIdentity)User.Identity;
            var userId = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _unitOfWork.ShoppingCart.GetAll(a => a.AppUser.Id == userId).ToList();



            AppUser user = _unitOfWork.AppUser.Get1(a => a.Id == userId);
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
            if(cart.count <= 50)
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

        public IActionResult More(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = _unitOfWork.ShoppingCart.Get1(c => c.Id == cartid);
            if (cart.count < 1000) cart.count++;
            else
            {
                TempData["error"] = "Quantity cant larger than 1000";
            }
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Less(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = _unitOfWork.ShoppingCart.Get1(c => c.Id == cartid);
            if (cart.count > 1) cart.count--;
            else
            {
                TempData["error"] = "Quantity cant smaller than 1";
            }
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteOne(int? cartid)
        {
            if (cartid == null || cartid == 0) return NotFound();
            ShoppingCart cart = _unitOfWork.ShoppingCart.Get1(a =>  a.Id == cartid);

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.save();
            TempData["success"] = "Delete item successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
