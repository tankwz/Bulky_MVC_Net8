using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_temp.Data;
using Razor_temp.Model;

namespace Razor_temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _data;
        public List<Category> CategoryList { get; set; }
        public IndexModel(DatabaseContext db)
        {
            _data = db;
        }
        public void OnGet()
        {
            CategoryList = _data.Categories.ToList();
        }
    }
}
