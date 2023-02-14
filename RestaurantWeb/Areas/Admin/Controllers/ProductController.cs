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
    public class ProductController : Controller
    {
        //we will use this dbcontext from from repository it will not use any more
        //ApplicationDbContext will Replace with Caegory Repository

        private readonly IUnitOfWork _unitWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitWork, IWebHostEnvironment hostEnvironment)
        {
            _unitWork = unitWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> objCategoryList = _unitWork.Category.GetAll();
            //return View(objCategoryList);
            return View();
        }



        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _unitWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                return View(productVM);
            }
            else
            {
                productVM.product = _unitWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
                //Updateproduct

            }

            
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageUrl != null) {

                       var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                       if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.product.Id == 0)
                {
                    _unitWork.Product.Add(obj.product);
                }
                else
                {
                    _unitWork.Product.Update(obj.product);
                }
                _unitWork.Save();
                TempData["sucess"] = "FoodItem Added Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        
      

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitWork.Product.GetAll(includeProperties:"Category");
            return Json(new { data = productList });
        }


        //post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while delete" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitWork.Product.Remove(obj);
            _unitWork.Save();
            return Json(new { success = true, message = "Delete Sucessfully" });


            //return View(obj);
        }


        #endregion
    }
}