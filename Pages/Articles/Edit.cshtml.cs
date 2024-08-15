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
    public class EditModel : TagNamePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ArticleRepository _articleRepository;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, ArticleRepository articleRepository)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _articleRepository = articleRepository;
        }

        [BindProperty]
        public Article Article { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.FindAsync(id);

            if (Article == null)
            {
                return NotFound();
            }

            PopulateTagsDropDownList(_context, Article.TagID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTagsDropDownList(_context, Article.TagID);
                return Page();
            }

            var articleToUpdate = await _context.Articles.FindAsync(Article.ArticleID);

            if (articleToUpdate == null)
            {
                return NotFound();
            }

            // Update article properties
            articleToUpdate.ArticleTitleSWE = Article.ArticleTitleSWE;
            articleToUpdate.ArticleTitleENG = Article.ArticleTitleENG;
            articleToUpdate.ArticleContentSWE = Article.ArticleContentSWE;
            articleToUpdate.ArticleContentENG = Article.ArticleContentENG;
            articleToUpdate.Archived = Article.Archived;
            articleToUpdate.Featured = Article.Featured;
            articleToUpdate.Date = Article.Date;
            articleToUpdate.TagID = Article.TagID; // Update TagID here
            articleToUpdate.Archived = Article.Archived;
            // Call the repository method to update Featured status
            _articleRepository.SetFeaturedArticle(Article.ArticleID, Article.Featured);

            if (Image != null && Image.Length > 0)
            {
                // Get the file name from the uploaded file
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", fileName);

                // Copy the uploaded file to the specified location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Set the properties of the Article model related to the uploaded file
                articleToUpdate.Image = System.IO.File.ReadAllBytes(filePath);
                articleToUpdate.ImageMimeType = Image.ContentType;
                articleToUpdate.ImageFileName = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
