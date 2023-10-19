using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.DataAccess.Repository;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using pj.Utility;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin )]

    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork uni )
        {
            _unitOfWork = uni;
        }
        OrderVM orderVM = new();
        public IActionResult Index()
        {
        //    if(id== null || id==0)
        //    {
        //        return NotFound();
        //    }
            return View();
        }

        [HttpGet]
        public IActionResult getAllAPI()
        {
            List<OrderHead> head = _unitOfWork.OrderHead.GetAll(includeProperties: "AppUser").ToList();
         //   if (head == null) return Json(new { data = "No Record" });
            return Json(new { data = head });
        }




    }
}
