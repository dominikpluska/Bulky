using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bulky_RazorTemp.Data;
using Bulky_RazorTemp.Models;

namespace Bulky_RazorTemp.Pages.Categories
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _context.Categories.Find(id);
            }
        }

        public IActionResult OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                var objectToDelete = _context.Categories.Find(Category.Id);
                if (objectToDelete != null)
                {
                    return NotFound();
                }
                _context.Categories.Remove(objectToDelete);
                _context.SaveChanges();
                TempData["success"] = "Category deleted succesfully!";
                return RedirectToAction("Index");
            }
            return Page();
        }
    }
}
    

