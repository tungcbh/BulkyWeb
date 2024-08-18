using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Category { get; set; }

        public void OnGet(int? id)
        {
            if (id != null && id != 0) Category = _db.Categories.FirstOrDefault(o => o.Id == id);
            else RedirectToPage("Index");
        }
        public IActionResult OnPost()
        {
            Category? category = _db.Categories.Find(Category.Id);
            if (category == null)
            {
                return Page();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Delete Successful";
            return RedirectToPage("Index", "Category");
        }
    }
}
