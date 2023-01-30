using Microsoft.AspNetCore.Mvc;
using RestaurantWeb.Data;
using RestaurantWeb.Models;

namespace RestaurantWeb.Controllers
{
	public class CategoryController : Controller
	{

		private readonly ApplicationDbContext _db;
	
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
		IEnumerable<Category> objCategoryList = _db.Categories;
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
			if (obj.Name == obj.DisplayOrder.ToString()) {

				ModelState.AddModelError("name", "The Display Order not exactly matches the name");
			}
			if (ModelState.IsValid) {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["sucess"] = "Category Created Sucessfully";
                return RedirectToAction("Index");
            }
			return View(obj);
        }




        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }

               var category = _db.Categories.Find(id);
            //var category = _db.Categories.FirstOrDefault(x=> x.Id==id);
            //var category = _db.Categories.SingleOrDefault(x=>x.Id==id);

            if (category == null){
                return NotFound();
            }
            return View(category);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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

            var category = _db.Categories.Find(id);
            //var category = _db.Categories.FirstOrDefault(x=> x.Id==id);
            //var category = _db.Categories.SingleOrDefault(x=>x.Id==id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();
            }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["sucess"] = "Category Deleted Sucessfully";
            return RedirectToAction("Index");
            
            return View(obj);
        }


    }
}
