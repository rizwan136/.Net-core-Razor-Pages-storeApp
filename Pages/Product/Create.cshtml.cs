using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Product
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
        public Data.Product Product { get; set; }
        public IActionResult OnGetAysnc()
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
                if (Product == null)
                    return NotFound();
                //if (Product.ImageFile != null)
                //{
                //    var fileResult = _fileService.SaveImage(Product.ImageFile);
                //    if (fileResult.Item1 == 1)
                //    {
                //        Product.ProfilePicture = fileResult.Item2;
                //    }
                ////}
                _ctx.Products.Add(Product);
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
