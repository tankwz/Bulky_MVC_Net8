using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_temp.Data;
using Razor_temp.Model;

namespace Razor_temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _datacontext;
        [BindProperty]
        public Category category { get; set; } 
        public CreateModel(DatabaseContext db)
        {
            _datacontext = db;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            _datacontext.Categories.Add(category);
            _datacontext.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }

    }
}
