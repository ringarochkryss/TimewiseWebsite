using Salto.Data;
using Salto.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Salto.Pages.Articles
{
    public class TagNamePageModel : PageModel
    {
        public SelectList TagNameSWE { get; set; }

        public void PopulateTagsDropDownList(ApplicationDbContext _context,
            object selectedTag = null)
        {
            var tagQuery = from d in _context.Tags
                                   orderby d.TagNameSWE // Sort by name.
                           select d;

            TagNameSWE = new SelectList(tagQuery.AsNoTracking(),
                nameof(Tag.TagID),
                nameof(Tag.TagNameSWE),
                selectedTag);
        }
    }
}