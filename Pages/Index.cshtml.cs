using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Salto.Data; // Import your DbContext namespace
using Salto.Models; // Import your Article model 
using System.Collections.Generic; // Import for List<T>

namespace Salto.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context) // Add your DbContext as a parameter here
        {
            _logger = logger;
            _context = context;
        }
        //public List<Tag> Tags { get; set; } // Property to store tags data
        public Article FeaturedArticle { get; private set; }
        public List<Tag> Tags { get; private set; } // Add property for tags
        public void OnGet()
        {
            // Query the database to get the featured article including the image data
            FeaturedArticle = _context.Articles
                .Where(a => a.Featured)
                .Select(a => new Article
                {
                    ArticleID = a.ArticleID,
                    ArticleTitleSWE = a.ArticleTitleSWE,
                    ArticleTitleENG = a.ArticleTitleENG,
                    ArticleContentSWE = a.ArticleContentSWE,
                    ArticleContentENG = a.ArticleContentENG,
                    Image = a.Image, // Include the image data
                    ImageAltText = a.ImageAltText,
                    ImagePath = a.ImagePath
                })
                .FirstOrDefault();

            Tags = _context.Tags.ToList();
        }
    }
}