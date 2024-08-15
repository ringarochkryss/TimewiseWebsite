using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Learn
{
    public class IndexModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public IndexModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LearnArticle> LearnArticle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.LearnArticle != null)
            {
                LearnArticle = await _context.LearnArticle
                    .Include(l => l.LearnTag)
                    .OrderBy(l => l.LearnTag.SortOrder) 
                    .ToListAsync();
            }
        }
    }
}
