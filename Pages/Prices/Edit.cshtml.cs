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

namespace Salto.Pages.Prices
{
    public class EditModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public EditModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Salto.Models.Prices Prices { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var prices =  await _context.Prices.FirstOrDefaultAsync(m => m.PricesID == id);
            if (prices == null)
            {
                return NotFound();
            }
            Prices = prices;
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

            _context.Attach(Prices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricesExists(Prices.PricesID))
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

        private bool PricesExists(int id)
        {
          return _context.Prices.Any(e => e.PricesID == id);
        }
    }
}
