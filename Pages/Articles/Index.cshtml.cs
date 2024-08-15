using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Salto.Helpers;
using Salto.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Salto.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public IndexModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string SearchString { get; set; }
        public PaginatedList<Article> Articles { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string ArticleTitleSWESort { get; set; }
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ArticleTitleSWE_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<Article> articlesIQ = from s in _context.Articles
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                articlesIQ = articlesIQ.Where(s => s.ArticleTitleSWE.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ArticleTitleSWE_desc":
                    articlesIQ = articlesIQ.OrderByDescending(s => s.ArticleTitleSWE);
                    break;
                case "Date":
                    articlesIQ = articlesIQ.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    articlesIQ = articlesIQ.OrderByDescending(s => s.Date);
                    break;
                default:
                    articlesIQ = articlesIQ.OrderByDescending(s => s.ArticleTitleSWE)
                                           .ThenBy(s => s.Date);
                    break;
            }

            int pageSize = 10;
            Articles = await PaginatedList<Article>.CreateAsync(
                articlesIQ.Include(c => c.Tag).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}