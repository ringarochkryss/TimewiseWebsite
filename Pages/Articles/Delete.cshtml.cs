using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Articles
{
    public class DeleteModel : TagNamePageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            Article = await _context.Articles
                .AsNoTracking()
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(m => m.ArticleID == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                // Delete the associated image file
                if (!string.IsNullOrEmpty(article.ImageFileName))
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", article.ImageFileName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Remove the article from the context and save changes
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
