using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Salto.Data;
using Salto.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Salto.Pages.Learn
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateLearnArticleViewModel CreateLearnArticleViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CreateLearnArticleViewModel = new CreateLearnArticleViewModel
            {
                LearnArticle = new LearnArticle(),
                TagList = await _context.LearnTag.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                }).ToListAsync()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CreateLearnArticleViewModel.TagList = await _context.LearnTag.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                }).ToListAsync();

                return Page();
            }

            var learnArticle = CreateLearnArticleViewModel.LearnArticle;

            if (CreateLearnArticleViewModel.Image1 != null)
            {
                learnArticle.Image1 = await SaveImageAsByteArrayAsync(CreateLearnArticleViewModel.Image1);
            }
            if (CreateLearnArticleViewModel.Image2 != null)
            {
                learnArticle.Image2 = await SaveImageAsByteArrayAsync(CreateLearnArticleViewModel.Image2);
            }
            if (CreateLearnArticleViewModel.Image3 != null)
            {
                learnArticle.Image3 = await SaveImageAsByteArrayAsync(CreateLearnArticleViewModel.Image3);
            }
            if (CreateLearnArticleViewModel.Image4 != null)
            {
                learnArticle.Image4 = await SaveImageAsByteArrayAsync(CreateLearnArticleViewModel.Image4);
            }
            if (CreateLearnArticleViewModel.Image5 != null)
            {
                learnArticle.Image5 = await SaveImageAsByteArrayAsync(CreateLearnArticleViewModel.Image5);
            }

            _context.LearnArticle.Add(learnArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<byte[]> SaveImageAsByteArrayAsync(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
