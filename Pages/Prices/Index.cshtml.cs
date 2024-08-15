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
    public class IndexModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public IndexModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Salto.Models.Prices> Prices { get; set; } = default!;

        // Add new properties to store cleaned points and descriptions
        public string[] CleanedPointsSWE { get; set; } = Array.Empty<string>();
        public string[] CleanedPointsENG { get; set; } = Array.Empty<string>();
        public string[] CleanedDescriptionsSWE { get; set; } = Array.Empty<string>();
        public string[] CleanedDescriptionsENG { get; set; } = Array.Empty<string>();


        public async Task OnGetAsync()
        {
            if (_context.Prices != null)
            {
                Prices = await _context.Prices.ToListAsync();

                CleanedPointsSWE = Prices
                    .Select(price => price.PointsSWE?.Split(',')
                    .Select(p => p.Trim()))
                    .FirstOrDefault()?.ToArray();

                CleanedPointsENG = Prices
                    .Select(price => price.PointsENG?.Split(',')
                    .Select(p => p.Trim()))
                    .FirstOrDefault()?.ToArray();

                CleanedDescriptionsSWE = Prices
                    .Select(price => price.DescriptionsSWE?.Split(',')
                    .Select(p => p.Trim()))
                    .FirstOrDefault()?.ToArray();

                CleanedDescriptionsENG = Prices
                    .Select(price => price.DescriptionsENG?.Split(',')
                    .Select(p => p.Trim()))
                    .FirstOrDefault()?.ToArray();
            }
        }
       
    }
    }
