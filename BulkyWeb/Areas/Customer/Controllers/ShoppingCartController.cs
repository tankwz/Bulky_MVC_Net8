using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer"), Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
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
                ListCarts = _unitOfWork.ShoppingCart.GetAll(a => a.AppUserId == UserId, includeProperties: "Product"),

            };
            foreach(var cart in ShoppingCartVM.ListCarts)
            {
                cart.price = PricenQuantityCal(cart);
                ShoppingCartVM.TotalCartPrice += (cart.price *cart.count) ;
            }
            return View(ShoppingCartVM);
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
    }
}
