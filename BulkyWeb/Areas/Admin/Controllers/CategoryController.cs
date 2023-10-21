using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Utility;



namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public CategoryController(IUnitOfWork uni)
        {
            _uniOfWork = uni;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategoryList = await _uniOfWork.Category.GetAllAsync();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cant match name");
            }
            if (ModelState.IsValid)
            {
                _uniOfWork.Category.AddAsync(obj);
                _uniOfWork.SaveAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = await _uniOfWork.Category.Get1Async(u => u.Id == id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cant match name");
            }
            if (ModelState.IsValid)
            {
                _uniOfWork.Category.Update(obj);
                TempData["success"] = "Category updated successfully";
                await _uniOfWork.SaveAsync();

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Category? category = await _uniOfWork.Category.Get1Async(u => u.Id == id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Category? obj = await _uniOfWork.Category.Get1Async(u => u.Id == id);
            if (obj == null) return NotFound();
            _uniOfWork.Category.Remove(obj);
            _uniOfWork.SaveAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
