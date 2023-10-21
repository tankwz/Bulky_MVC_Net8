using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitofwr)
        {
            _logger = logger;
            _unitOfWork = unitofwr;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products =  await _unitOfWork.Product.GetAllAsync(includeProperties:"Category");
            return View(products);
        }



        //public IActionResult Details(int? id)
        //{
        //    ShoppingCart cart = new ShoppingCart();

        //    if (id == null || id==0)
        //        return NotFound();
        //    Product p = _unitOfWork.Product.Get1(p => p.Id == id, includeProperties: "Category");
        //    cart.Product = p;

        //    cart.ProductId = cart.Product.Id;
        //    return View(cart);
        //}

        public async Task<IActionResult> Details(int id)
        {
            ShoppingCart cart = new()
            {
                 Product = await _unitOfWork.Product.Get1Async(p => p.Id == id, includeProperties: "Category"),
                count = 1,
                ProductId = id
            };
           return View(cart);
        }





        [HttpPost, Authorize]
        public async Task<IActionResult> Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCart cartcheck = await _unitOfWork.ShoppingCart.Get1Async(s => s.AppUserId == userId && s.ProductId == cart.ProductId);
            if (cartcheck == null)
            {
                if (cart.count  > 1000)
                {
                    TempData["error"] = "Can't have more than 1000 items to cart";
                    return RedirectToAction(nameof(Details));
                }

                cart.AppUserId = userId;
                await _unitOfWork.ShoppingCart.AddAsync(cart);
            }
            else
            {
                if ((cartcheck.count + cart.count) <= 1000)
                {
                     _unitOfWork.ShoppingCart.Remove(cartcheck);
                    await _unitOfWork.SaveAsync();
                    cartcheck.count += cart.count;
                    cartcheck.Id = 0;
                    await _unitOfWork.ShoppingCart.AddAsync(cartcheck);

                }
                else
                {
                    TempData["error"] = "Can't have more than 1000 items to cart";
                    return RedirectToAction(nameof(Details));
                }

            }
            TempData["success"] = "Added to cart";
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "ShoppingCart");
        }
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Product p = await _unitOfWork.Product.Get1Async(p => p.Id == id, includeProperties: "Category");
            return View(p);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
