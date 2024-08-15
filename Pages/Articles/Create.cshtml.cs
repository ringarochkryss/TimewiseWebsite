using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Articles
{
    public class CreateModel : TagNamePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ArticleRepository _articleRepository;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, ArticleRepository articleRepository)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _articleRepository = articleRepository;
        }

        [BindProperty]
        public Article Article { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public IActionResult OnGet()
        {
            PopulateTagsDropDownList(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTagsDropDownList(_context);
                return Page();
            }

            var emptyArticle = new Article();

            if (await TryUpdateModelAsync<Article>(
                emptyArticle,
                "article",
                c => c.ArticleID,
                c => c.ArticleTitleSWE,
                c => c.ArticleTitleENG,
                c => c.ArticleContentSWE,
                c => c.ArticleContentENG,
                c => c.Date,
                c => c.Archived,
                c => c.Featured,
                c => c.Tag,
                c => c.TagID,
                c => c.ImageMimeType,
                c => c.ImageFileName,
                c => c.ImageCaption,
                c => c.ImageAltText,
                c => c.ImageDescription,
                c => c.ImageCredit))
            {
                if (Image != null && Image.Length > 0)
                {
                    // Get the file name from the uploaded file
                    var fileName = Path.GetFileName(Image.FileName);

                    // Construct the file path where you want to save the file within wwwroot/Images
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", fileName);

                    // Copy the uploaded file to the specified location
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // Set the properties of the Article model related to the uploaded file 
                    emptyArticle.Image = System.IO.File.ReadAllBytes(filePath);
                    emptyArticle.ImageMimeType = Image.ContentType;
                    emptyArticle.ImageFileName = fileName;
                }

                _context.Articles.Add(emptyArticle);
                await _context.SaveChangesAsync();

                // Call the repository method to update Featured status
                _articleRepository.SetFeaturedArticle(emptyArticle.ArticleID, emptyArticle.Featured);

                return RedirectToPage("./Index");
            }



            // Select TagID if TryUpdateModelAsync fails.
            PopulateTagsDropDownList(_context, emptyArticle?.TagID);
            return Page();
        }
    }
}
