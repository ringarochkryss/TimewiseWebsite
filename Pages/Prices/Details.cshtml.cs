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
    public class DetailsModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DetailsModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }


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
    }
}
