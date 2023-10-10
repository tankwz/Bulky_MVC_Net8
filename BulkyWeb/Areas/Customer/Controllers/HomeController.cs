using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using System.Diagnostics;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitofwr)
        {
            _logger = logger;
            _unitOfWork = unitofwr;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products =  _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id==0)
                return NotFound();
            Product p = _unitOfWork.Product.Get1(p => p.Id == id, includeProperties: "Category");
            return View(p);
        }
        public IActionResult Details2(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Product p = _unitOfWork.Product.Get1(p => p.Id == id, includeProperties: "Category");
            return View(p);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
