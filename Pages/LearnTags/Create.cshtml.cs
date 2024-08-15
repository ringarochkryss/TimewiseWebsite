using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.LearnTags
{
    public class CreateModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public CreateModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LearnTag LearnTag { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LearnTag.Add(LearnTag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
