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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork uni, IWebHostEnvironment webHost)
        {
            _unitOfWork = uni;
            _webHostEnvironment = webHost;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objProductList);
        }

        public IActionResult UpSert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            if (id == null || id == 0)
                return View(productVM);

            else
            {
                productVM.Product = _unitOfWork.Product.Get1(u => u.Id == id);
                return View(productVM);

            }
        }
        [HttpPost]
        public IActionResult UpSert(ProductVM p, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"img\products");
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    p.Product.ImageUrl = @"\img\products\" + filename;
                } ;
                _unitOfWork.Product.Add(p.Product);
                _unitOfWork.save();
                TempData["success"] = "Added product successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                p.CategoryList = _unitOfWork.Category.GetAll().Select(a =>
                new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });
                return View(p);
            }
        }

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
                
            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM p)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(p.Product);
                _unitOfWork.save();
                TempData["success"] = "Added product successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                p.CategoryList = _unitOfWork.Category.GetAll().Select(a =>
                new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });
                return View(p);
            }
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
