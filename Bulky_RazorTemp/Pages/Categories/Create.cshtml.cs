using Bulky_RazorTemp.Data;
using Bulky_RazorTemp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_RazorTemp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success"] = "Category created succesfully!";
            return RedirectToPage("Index");
        }
    }
}
