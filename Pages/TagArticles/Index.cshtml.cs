using Azure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Salto.Pages.TagArticles
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tag> Tags { get; set; } // Property to store tags data
        public List<Article> Articles { get; set; }

        public async Task OnGetAsync(int? tagId)
        {
            Tags = await _context.Tags.ToListAsync(); // Fetch all tags

            if (tagId.HasValue)
            {
                // Filter articles by the specified tag ID
                Articles = await _context.Articles
                    .Include(a => a.Tag)
                    .Where(a => a.Tag.TagID == tagId.Value)
                    .ToListAsync();
            }
            else
            {
                // Fetch all articles when no specific tag ID is provided
                Articles = await _context.Articles.ToListAsync();
            }
        }
    }
}
