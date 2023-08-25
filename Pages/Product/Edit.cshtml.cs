using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Data;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Product
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
        public Data.Product ProductDetail { get; set; }
        public IActionResult OnGet(int id)
        {
            var person = _ctx.Products.Find(id);
            if (person == null)
                return NotFound();
            ProductDetail = person;
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
                if (ProductDetail == null)
                    return NotFound();
                //var OldProfile = ProductDetail.ProfilePicture;

                //if (ProductDetail.ImageFile != null)
                //{

                //    var fileResult = _fileService.SaveImage(ProductDetail.ImageFile);
                //    if (fileResult.Item1 == 1)
                //    {
                //        ProductDetail.ProfilePicture = fileResult.Item2;
                //    }
                //}
                _ctx.Products.Update(ProductDetail);
                await _ctx.SaveChangesAsync();
                //if (!string.IsNullOrEmpty(OldProfile) && OldProfile != ProductDetail.ProfilePicture)
                //{
                //    _fileService.DeleteImage(OldProfile);
                //}
                TempData["success"] = "Saved successfully";
                return RedirectToPage("ListOfProduct");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
                return Page();
            }
        }
    }
}
