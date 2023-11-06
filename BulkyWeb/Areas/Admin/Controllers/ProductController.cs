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
        public async Task<IActionResult> Index()
        {
            var objProduct = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            
            List<Product> objProductList = objProduct.ToList();
            return View(objProductList);
        }                  

        public async Task<IActionResult> UpSert(int? id)
        {
            IEnumerable<Category> Category = await _unitOfWork.Category.GetAllAsync();
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = Category.Select(u => new SelectListItem
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
        public async Task<IActionResult> UpSert(ProductVM p, IFormFile? file)
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
                      //  file.CopyTo(fileStream);
                        await file.CopyToAsync(fileStream);
                    }

                    p.Product.ImageUrl = @"\img\products\" + filename;
                }


                if (p.Product.Id == 0)
                {

                    await _unitOfWork.Product.AddAsync(p.Product);
                    TempData["success"] = "Added product successfully";
                }
                else
                {
                     _unitOfWork.Product.Update(p.Product);
                    TempData["success"] = "Updated product successfully";
                }
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var category = await _unitOfWork.Category.GetAllAsync();
                p.CategoryList = category.Select(a =>
                new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });
                return View(p);
            }
        }

        public async Task<IActionResult> Create()
        {
            var category = await _unitOfWork.Category.GetAllAsync();

            ProductVM productVM = new()
            {

                Product = new Product(),
                CategoryList = category.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })

            };

            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM p)
        {//old code, not relevant anymore, now its upsert
            if (ModelState.IsValid)
            {
                await _unitOfWork.Product.AddAsync(p.Product);
                _unitOfWork.save();
                TempData["success"] = "Added product successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var categories = await _unitOfWork.Category.GetAllAsync();

                p.CategoryList = categories.Select(a =>
                new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });
                return View(p);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {//old code, not relevant anymore, now its upsert
            if (id == null || id == 0)
                return NotFound();
            Product? product = await _unitOfWork.Product.Get1Async(p => p.Id == id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {//old code, not relevant anymore, now its upsert
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveAsync();
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

        public async Task<IActionResult> GetAll()
        {

            var Products = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            List<Product> p = Products.ToList();
            return Json(new {data = p});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            Product p = await _unitOfWork.Product.Get1Async(p => p.Id == id);
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
            await _unitOfWork.SaveAsync();
            return Json(new { success=true, messsage="Delete Successfully" });
        }        
        
        #endregion







    }
}
