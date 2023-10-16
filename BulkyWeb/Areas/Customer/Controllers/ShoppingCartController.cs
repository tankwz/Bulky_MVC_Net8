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


        public IActionResult Summary()
        {
            return View();
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
                cart.currentprice = cart.price * cart.count;

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
