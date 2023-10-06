using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Repository.IRepository;
using pj.Models;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork uni)
        {
            _unitOfWork = uni;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.save();
                TempData["success"] = "Added product successfully";
                return RedirectToAction("Index","Product");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.save();
                TempData["success"] = "Edited product successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            Product? product = _unitOfWork.Product.Get1(p=>p.Id == id);
            return View(product);
        }











    }
}
