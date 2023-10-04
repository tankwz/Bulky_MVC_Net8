using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_temp.Data;
using Razor_temp.Model;

namespace Razor_temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _data;
        public Category category { get; set; }
        public EditModel(DatabaseContext db)
        {
            _data = db;
        }
        public void OnGet(int id)
        {
            if(id!=null && id != 0)
            {

                category = _data.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {

            if(ModelState.IsValid)
            {
                _data.Categories.Update(category);
                _data.SaveChanges();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
