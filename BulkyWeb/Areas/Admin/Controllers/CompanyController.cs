using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using pj.Utility;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(   IUnitOfWork uni)
        {
            _unitOfWork = uni;
        }
        public async Task<IActionResult> Index()
        { 
          //  IEnumerable<Company> companies = await _unitOfWork.Company.GetAllAsync();
            return View();
        }
        public async Task<IActionResult> UpSert(int? id)
        {
            Company cp = new Company();

            if (id == null || id == 0)
            {
                return View(cp);
            }
            else
            {
                cp = await _unitOfWork.Company.Get1Async(a => a.Id == id);

                return View(cp);
            }
        }
        [HttpPost] 
        public async Task<IActionResult> UpSert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    await _unitOfWork.Company.AddAsync(company);
                    TempData["success"] = "Added company successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["success"] = "Updated company successfully";
                }
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {

                return View(company);
            }



        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cpn = await _unitOfWork.Company.GetAllAsync();

            List<Company> cp = cpn.ToList();
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
        public async Task<IActionResult> Delete(int? id)
        {
            Company p = await _unitOfWork.Company.Get1Async(p => p.Id == id);
            if (p == null) return Json(new { success = false, message = "Error while deleting" });
            _unitOfWork.Company.Remove(p);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true, message = "Delete Successfully" });

        }

    }
}
