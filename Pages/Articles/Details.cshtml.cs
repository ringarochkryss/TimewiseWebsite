using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DetailsModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
