using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.LearnTags
{
    public class EditModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public EditModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LearnTag LearnTag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LearnTag == null)
            {
                return NotFound();
            }

            var learntag =  await _context.LearnTag.FirstOrDefaultAsync(m => m.Id == id);
            if (learntag == null)
            {
                return NotFound();
            }
            LearnTag = learntag;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LearnTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnTagExists(LearnTag.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LearnTagExists(int id)
        {
          return _context.LearnTag.Any(e => e.Id == id);
        }
    }
}
