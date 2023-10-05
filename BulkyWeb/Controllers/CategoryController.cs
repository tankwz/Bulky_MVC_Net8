
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public CategoryController(IUnitOfWork uni)
        {
            _uniOfWork = uni;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _uniOfWork.Category.GetAll().ToList();
            return View(objCategoryList); 
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cant match name");
            }
            if (ModelState.IsValid)
            {
                _uniOfWork.Category.Add(obj);
                _uniOfWork.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = _uniOfWork.Category.Get1(u=>u.Id==id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cant match name");
            }
            if (ModelState.IsValid)
            {
                _uniOfWork.Category.Update(obj);
                TempData["success"] = "Category updated successfully";
                _uniOfWork.save();

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = _uniOfWork.Category.Get1(u=>u.Id==id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _uniOfWork.Category.Get1(u => u.Id == id);
            if (obj == null)   return NotFound();
            _uniOfWork.Category.Remove(obj);
            _uniOfWork.save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
