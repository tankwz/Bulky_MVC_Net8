using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_temp.Data;
using Razor_temp.Model;

namespace Razor_temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext _data;

        public Category category {  get; set; }
        public DeleteModel(DatabaseContext db)
        {
            _data = db;
        }

        public void OnGet(int? id)
        {   
            if(id != null)
            {
                category = _data.Categories.Find(id);
            }
        }

        public IActionResult Onpost()
        {

            Category? obj = _data.Categories.Find(category.Id);
            if(obj == null)
            {
                return NotFound();
            }

            _data.Categories.Remove(obj);
            _data.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToPage("Index");
        }










    }
}
