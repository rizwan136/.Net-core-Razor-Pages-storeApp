using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Person
{
    public class CreateModel : PageModel
    {
        private readonly DataBaseContext _ctx;
        private readonly IFileService _fileService;

        public CreateModel(DataBaseContext ctx, IFileService fileService)
        {
            _ctx = ctx;
            _fileService = fileService;
        }

        [BindProperty]
        public Data.Person Person { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if (Person == null)
                    return NotFound();
                if (Person.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(Person.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        Person.ProfilePicture = fileResult.Item2;
                    }
                }
                _ctx.Person.Add(Person);
                await _ctx.SaveChangesAsync();
                TempData["success"] = "Saved successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
            }
            return RedirectToPage();
        }
    }
}
