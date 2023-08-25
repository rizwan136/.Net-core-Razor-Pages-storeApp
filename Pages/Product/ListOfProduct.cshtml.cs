using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;

namespace RazorPagesTutorial.Pages.Product
{
    public class ListOfProductModel : PageModel
    {
        private readonly DataBaseContext _context;

        public ListOfProductModel(DataBaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Data.Product> ProductList { get; set; }
        public IActionResult OnGet()
        {
            ProductList = _context.Products.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return Page();
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["success"] = "Deleted Successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = "Error on Deleting record";
            }
            return RedirectToPage();
        }
    }
}
