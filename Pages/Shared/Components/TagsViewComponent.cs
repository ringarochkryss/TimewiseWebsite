using Microsoft.AspNetCore.Mvc;
using Salto.Data;
using Salto.Models;
using System.Collections.Generic;
using System.Linq;

namespace Salto.Shared.Components
{
    public class TagsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TagsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }
    }
}
