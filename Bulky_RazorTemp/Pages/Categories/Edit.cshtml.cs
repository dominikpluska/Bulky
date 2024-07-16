using Bulky_RazorTemp.Data;
using Bulky_RazorTemp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_RazorTemp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext context)
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
                 _context.Categories.Update(category);
                 _context.SaveChangesAsync();
                TempData["success"] = "Category updated!";
                return RedirectToAction("Index");
            }
            return Page();
        }
    }
}
