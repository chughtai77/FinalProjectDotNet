using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
//using RestaurantWeb.Data;
//using RestaurantWeb.Models;

namespace RestaurantWeb.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //we will use this dbcontext from from repository it will not use any more
        //ApplicationDbContext will Replace with Caegory Repository

        private readonly IUnitOfWork _unitWork;

        public CategoryController(IUnitOfWork unitWork)
        {
            _unitWork = unitWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitWork.Category.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("name", "The Display Order not exactly matches the name");
            }
            if (ModelState.IsValid)
            {
                _unitWork.Category.Add(obj);
                _unitWork.Save();
                TempData["sucess"] = "Category Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var category = _db.Categories.Find(id);
            var categoryDbFirst = _unitWork.Category.GetFirstOrDefault(x => x.Id == id);
            //var category = _db.Categories.SingleOrDefault(x=>x.Id==id);

            if (categoryDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("name", "The Display Order not exactly matches the name");
            }
            if (ModelState.IsValid)
            {
                _unitWork.Category.Update(obj);
                _unitWork.Save();
                TempData["sucess"] = "Category Edit Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Categories.Find(id);
            var category = _unitWork.Category.GetFirstOrDefault(x => x.Id == id);
            //var category = _db.Categories.SingleOrDefault(x=>x.Id==id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitWork.Category.Remove(obj);
            _unitWork.Save();
            TempData["sucess"] = "Category Deleted Sucessfully";
            return RedirectToAction("Index");

            //return View(obj);
        }

    }
}
