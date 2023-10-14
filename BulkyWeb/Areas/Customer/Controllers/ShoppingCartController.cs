using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Repository.IRepository;
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
            return View(ShoppingCartVM);
        }
    }
}
