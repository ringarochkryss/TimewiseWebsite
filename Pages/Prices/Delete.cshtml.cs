using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Prices
{
    public class DeleteModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DeleteModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Salto.Models.Prices Prices { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var prices = await _context.Prices.FirstOrDefaultAsync(m => m.PricesID == id);

            if (prices == null)
            {
                return NotFound();
            }
            else 
            {
                Prices = prices;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }
            var prices = await _context.Prices.FindAsync(id);

            if (prices != null)
            {
                Prices = prices;
                _context.Prices.Remove(Prices);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
