using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.DataAccess.Repository;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using pj.Utility;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin )]

    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork uni)
        {
            _unitOfWork = uni;
        }
        
        public IActionResult Index()
        {
            //    if(id== null || id==0)
            //    {
            //        return NotFound();
            //    }
            return View();
        }

        [HttpGet]
        public IActionResult getAllAPI(string status)
        {
            List<OrderHead> head = _unitOfWork.OrderHead.GetAll(includeProperties: "AppUser").ToList();

            switch (status)
            {
                case "pending":
                    head = head.Where(a => a.OrderStatus == SD.StatusPending).ToList();
                    break;
                case "paymentpending":
                    head = head.Where(a => a.PaymentStatus == SD.PaymentStatusPending).ToList();
                    break;
                case "inprocess":
                    head = head.Where(a => a.OrderStatus == SD.StatusProcessing).ToList();
                    break;
                case "completed":
                    head = head.Where(a => a.OrderStatus == SD.StatusShipped).ToList();
                    break;
                case "approved":
                    head = head.Where(a => a.OrderStatus == SD.StatusApproved).ToList();
                    break;
                default:

                    break;
            }

            //   if (head == null) return Json(new { data = "No Record" });
            return Json(new { data = head });
        }
        public OrderVM OrderVM { get; set; }
        public IActionResult Details(int? orderId)
        {

            OrderHead head = _unitOfWork.OrderHead.Get1(a => a.Id == orderId);
            IEnumerable<OrderDetail> details = _unitOfWork.OrderDetail.GetAll(a=> a.OrderHeadId == orderId);
            AppUser user = _unitOfWork.AppUser.Get1(a=> a.Id == head.AppUserId);
            head.AppUser = user;
            OrderVM = new()
            {
                orderHead = head,
                orderDetail = details
            };

            return View(OrderVM);
        }



    }
}
