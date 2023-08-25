using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;

namespace RazorPagesTutorial.Pages.Person
{
    public class DisplayAllModel : PageModel
    {
        private readonly DataBaseContext _context;

        public DisplayAllModel(DataBaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Data.Person> Pepole { get; set; }
        public IActionResult OnGet()
        {
            Pepole = _context.Person.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var person = _context.Person.Find(id);
            if (person == null)
                return Page();
            try
            {
                _context.Person.Remove(person);
                _context.SaveChanges();
                TempData["success"] = "Deleted Successfully";

            }
            catch(Exception ex)
            {
                TempData["error"] = "Error on Deleting record";
            }
            return RedirectToPage();
        }
    }
}
