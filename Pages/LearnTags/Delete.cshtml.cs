using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.LearnTags
{
    public class DeleteModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DeleteModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LearnTag LearnTag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LearnTag == null)
            {
                return NotFound();
            }

            var learntag = await _context.LearnTag.FirstOrDefaultAsync(m => m.Id == id);

            if (learntag == null)
            {
                return NotFound();
            }
            else 
            {
                LearnTag = learntag;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.LearnTag == null)
            {
                return NotFound();
            }
            var learntag = await _context.LearnTag.FindAsync(id);

            if (learntag != null)
            {
                LearnTag = learntag;
                _context.LearnTag.Remove(LearnTag);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
