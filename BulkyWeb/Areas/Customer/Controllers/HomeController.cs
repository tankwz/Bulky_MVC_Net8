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

        public IActionResult Index()
        {
            IEnumerable<Product> products =  _unitOfWork.Product.GetAll(includeProperties:"Category");
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

        public IActionResult Details(int id)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get1(p => p.Id == id, includeProperties: "Category"),
                count = 1,
                ProductId = id
            };
           return View(cart);
        }





        [HttpPost, Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCart cartcheck = _unitOfWork.ShoppingCart.Get1(s => s.AppUserId == userId && s.ProductId == cart.ProductId);
            if (cartcheck == null)
            {
                cart.AppUserId = userId;
                _unitOfWork.ShoppingCart.Add(cart);
            }
            else
            {
                cartcheck.count += cart.count;
                _unitOfWork.ShoppingCart.Update(cartcheck);
            }
            TempData["success"] = "Added to cart";
            _unitOfWork.save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details2(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Product p = _unitOfWork.Product.Get1(p => p.Id == id, includeProperties: "Category");
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
