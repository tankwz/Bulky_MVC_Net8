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
    //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee )]

    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork uni)
        {
            _unitOfWork = uni;
        }
        
        public async Task<IActionResult> Index()
        {
            //    if(id== null || id==0)
            //    {
            //        return NotFound();
            //    }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getAllAPI(string status)
        {
            var headfromdatabase = await _unitOfWork.OrderHead.GetAllAsync(includeProperties: "AppUser");
            List<OrderHead> head = headfromdatabase.ToList();

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
        public async Task<IActionResult> Details(int? orderId)
        {

            //OrderHead head = _unitOfWork.OrderHead.Get1(a => a.Id == orderId);
            //IEnumerable<OrderDetail> details = _unitOfWork.OrderDetail.GetAll(a=> a.OrderHeadId == orderId);
            //AppUser user = _unitOfWork.AppUser.Get1(a=> a.Id == head.AppUserId);
            //foreach (OrderDetail detail in details)
            //{
            //    detail.Product = _unitOfWork.Product.Get1(a => a.Id == detail.ProductId);
            //}
            //head.AppUser = user;
            //OrderVM = new()
            //{
            //    orderHead = head,
            //    orderDetail = details
            //};

            //shorter, better, async

            OrderVM = new()
            {
                orderHead = await _unitOfWork.OrderHead.Get1Async(u => u.Id == orderId, includeProperties:"AppUser"),
                orderDetail = await _unitOfWork.OrderDetail.GetAllAsync(a=> a.OrderHeadId == orderId, includeProperties:"Product")
            };


            return View(OrderVM);
        }



    }
}
