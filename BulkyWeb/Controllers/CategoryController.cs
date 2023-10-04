
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.Models;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyAppDatabaseContext _dbContext;
        public CategoryController(MyAppDatabaseContext db)
        {
            _dbContext = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
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
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = _dbContext.Categories.Find(id);
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
                _dbContext.Categories.Update(obj);
                TempData["success"] = "Category updated successfully";
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _dbContext.Categories.Find(id);
            if (obj == null)   return NotFound();
            _dbContext.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
