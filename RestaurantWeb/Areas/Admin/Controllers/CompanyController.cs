using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
//using RestaurantWeb.Data;
//using RestaurantWeb.Models;

namespace RestaurantWeb.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        //we will use this dbcontext from from repository it will not use any more
        //ApplicationDbContext will Replace with Caegory Repository

        private readonly IUnitOfWork _unitWork;

        public CompanyController(IUnitOfWork unitWork)
        {
            _unitWork = unitWork;
        }

        public IActionResult Index()
        {
           
            return View();
        }



        //Get
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _unitWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);

            }
            
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
               
                if (obj.Id == 0)
                {
                    _unitWork.Company.Add(obj);
                    TempData["sucess"] = "Restaurant Created Sucessfully";

                }
                else
                {
                    _unitWork.Company.Update(obj);
                    TempData["sucess"] = "Restaurant Updated Sucessfully";

                }
                _unitWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitWork.Company.GetAll();
            return Json(new { data = companyList });
        }


        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitWork.Company.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while delete" });
            }

            _unitWork.Company.Remove(obj);
            _unitWork.Save();
            return Json(new { success = true, message = "Delete Sucessfully" });


            //return View(obj);
        }


        #endregion
    }
}