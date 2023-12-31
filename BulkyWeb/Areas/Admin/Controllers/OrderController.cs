﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pj.DataAccess.Data;
using pj.DataAccess.Repository;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Models.ViewModels;
using pj.Utility;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

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
            IEnumerable<OrderHead> headfromdatabase;
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                 headfromdatabase = await _unitOfWork.OrderHead.GetAllAsync(includeProperties: "AppUser");

            }
            else
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                headfromdatabase = await _unitOfWork.OrderHead.GetAllAsync(a=>a.AppUserId == userId,includeProperties:"AppUser");
            }

            List<OrderHead> head = headfromdatabase.ToList();
            head.Reverse();

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
                orderHead = await _unitOfWork.OrderHead.Get1Async(u => u.Id == orderId, includeProperties: "AppUser"),
                orderDetail = await _unitOfWork.OrderDetail.GetAllAsync(a => a.OrderHeadId == orderId, includeProperties: "Product")
            };


            return View(OrderVM);
        }

        public async Task<IActionResult> HandleStatus(int id, string status)
        {
            Console.WriteLine(status);

            OrderHead heade = await _unitOfWork.OrderHead.Get1Async(a => a.Id == id);
            switch (status)
            {
                case SD.StatusApproved:
                    {
                      //  if(heade.OrderStatus == SD.StatusPending)
                        heade.OrderStatus = SD.StatusApproved;
                    //    
                        break;
                    }
                case SD.StatusProcessing:
                    {
                        heade.OrderStatus = SD.StatusProcessing;
                        break;
                    }
                case SD.StatusShipped:
                    {
                        heade.OrderStatus = SD.StatusShipped;
                        heade.PaymentStatus = SD.PaymentStatusApproved;
                        break;
                    }
                case SD.StatusCanceled:
                    {
                        heade.OrderStatus = SD.StatusCanceled;
                        break;
                    }
                default:
                    {
                        TempData["error"] = "Status isn't right";
                    }
                    break;
            }

            _unitOfWork.OrderHead.Update(heade);

            await _unitOfWork.SaveAsync();

            TempData["success"] = "Update Order successfully";

            return RedirectToAction(nameof(Details), new{ orderId = id});
        }



    }
}
