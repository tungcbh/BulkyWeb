using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
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
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (obj.Name == obj.DisplayOrder)
            //{
            //    ModelState.AddModelError("ModelOnly", "Không được đặt tên trùng với order");
            //}

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Create Successful";
                return RedirectToAction("Index", "Category");
            }
            else return View(category);

        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = _db.Categories.Find(id);
            //Category? category1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category Update Successful";
                return RedirectToAction("Index", "Category");
            }
            else return View(category.Id);

        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = _db.Categories.Find(id);
            //Category? category1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Delete Successful";
            return RedirectToAction("Index", "Category");
        }
    }
}
