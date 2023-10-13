using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using pj.Utility;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
 //   [Authorize(Roles = SD.Role_Admin)]
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
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

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

                    if (!(string.IsNullOrEmpty(p.Product.ImageUrl)))
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, p.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    p.Product.ImageUrl = @"\img\products\" + filename;
                }


                if (p.Product.Id == 0)
                {

                    _unitOfWork.Product.Add(p.Product);
                    TempData["success"] = "Added product successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(p.Product);
                    TempData["success"] = "Updated product successfully";
                }
                _unitOfWork.save();
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

     /*   public IActionResult Delete(int? id)
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
        }*/

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            List<Product> p = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return Json(new {data = p});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product p = _unitOfWork.Product.Get1(p => p.Id == id);
            string wwwRoot = _webHostEnvironment.WebRootPath;
            if( p == null) return Json(new {success=false, message="Error while deleting"});
            if (!string.IsNullOrEmpty(p.ImageUrl))
            {
                string oldfile = Path.Combine(wwwRoot, p.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldfile))
                {
                    System.IO.File.Delete(oldfile);
                }
            }
            _unitOfWork.Product.Remove(p);
            _unitOfWork.save();
            return Json(new { success=true, messsage="Delete Successfully" });


        }        
        
        #endregion







    }
}
