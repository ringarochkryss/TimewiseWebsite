using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Tags
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
        public Tag Tag { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
