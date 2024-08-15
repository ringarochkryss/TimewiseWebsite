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
    public class DeleteModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DeleteModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LearnArticle LearnArticle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LearnArticle == null)
            {
                return NotFound();
            }

            var learnarticle = await _context.LearnArticle.FirstOrDefaultAsync(m => m.Id == id);

            if (learnarticle == null)
            {
                return NotFound();
            }
            else 
            {
                LearnArticle = learnarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.LearnArticle == null)
            {
                return NotFound();
            }
            var learnarticle = await _context.LearnArticle.FindAsync(id);

            if (learnarticle != null)
            {
                LearnArticle = learnarticle;
                _context.LearnArticle.Remove(LearnArticle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
