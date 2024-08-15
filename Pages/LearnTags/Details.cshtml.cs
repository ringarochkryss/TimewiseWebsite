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
    public class DetailsModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public DetailsModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
