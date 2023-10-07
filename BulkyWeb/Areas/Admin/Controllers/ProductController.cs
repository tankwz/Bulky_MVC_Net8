using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;

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
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
                ,
                Product = new Product()
            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product.Product);
                _unitOfWork.save();
                TempData["success"] = "Added product successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Product? product = _unitOfWork.Product.Get1(p => p.Id == id);
            return View(product);
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
            Product? product = _unitOfWork.Product.Get1(p => p.Id == id);
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? product = _unitOfWork.Product.Get1(p => p.Id == id);
            if (product == null) return NotFound();
            _unitOfWork.Product.Remove(product);
            _unitOfWork.save();
            TempData["success"] = "Removed product successfully";
            return RedirectToAction("Index", "Product");
        }











    }
}
