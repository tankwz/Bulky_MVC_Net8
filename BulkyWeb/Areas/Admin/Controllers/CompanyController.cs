﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(   IUnitOfWork uni)
        {
            _unitOfWork = uni;
        }
        public IActionResult Index()
        {
            List<Company> cp = _unitOfWork.Company.GetAll().ToList();
            return View(cp);
        }
        public IActionResult UpSert(int? id)
        {
            Company cp = new Company();

            if (id == null || id == 0)
            {
                return View(cp);
            }
            else
            {
                cp = _unitOfWork.Company.Get1(a => a.Id == id);

                return View(cp);
            }
        }
        [HttpPost] 
        public IActionResult UpSert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["success"] = "Added company successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["success"] = "Updated company successfully";
                }
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {

                return View(company);
            }



        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> cp = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data= cp });
        }

        /*     public IActionResult Delete(int? id)
             {
                 Company cp = _unitOfWork.Company.Get1(a => a.Id==id);

                 if(cp == null)
                     return Json( new {success = false, message= "Error while deleting" });
                 _unitOfWork.Company.Remove(cp);
                 _unitOfWork.save();
                 return Json(new { success = true, message = "Delete successfully" });
             }

             */
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company p = _unitOfWork.Company.Get1(p => p.Id == id);
            if (p == null) return Json(new { success = false, message = "Error while deleting" });
          
          
            _unitOfWork.Company.Remove(p);
            _unitOfWork.save();
            return Json(new { success = true, messsage = "Delete Successfully" });


        }

    }
}
