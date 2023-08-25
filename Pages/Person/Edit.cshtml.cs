using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Person
{
    public class EditModel : PageModel
    {
        private readonly DataBaseContext _ctx;
        private readonly IFileService _fileService;

        public EditModel(DataBaseContext ctx, IFileService fileService)
        {
            _ctx = ctx;
            _fileService = fileService;
        }

        [BindProperty]
        public Data.Person Person { get; set; }
        public IActionResult OnGet(int id)
        {
            var person = _ctx.Person.Find(id);
            if (person == null)
                return NotFound();
            Person = person;
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
                var OldProfile = Person.ProfilePicture;

                if (Person.ImageFile != null)
                {

                    var fileResult = _fileService.SaveImage(Person.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        Person.ProfilePicture = fileResult.Item2;
                    }
                }
                _ctx.Person.Update(Person);
                await _ctx.SaveChangesAsync();
                if (!string.IsNullOrEmpty(OldProfile) && OldProfile != Person.ProfilePicture)
                {
                    _fileService.DeleteImage(OldProfile);
                }
                TempData["success"] = "Saved successfully";
                return RedirectToPage("DisplayAll");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
                return Page();
            }
        }

    }
}
