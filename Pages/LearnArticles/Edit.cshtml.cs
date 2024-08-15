using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Learn

{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<EditModel> logger)
        {
            _context = context;
            //_webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [BindProperty]
        public CreateLearnArticleViewModel LearnArticleViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _logger.LogInformation("OnGetAsync called with id: {Id}", id);

            if (id == null)
            {
                _logger.LogWarning("OnGetAsync called with null id");
                return NotFound();
            }

            var learnArticle = await _context.LearnArticle
                .Include(la => la.LearnTag)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (learnArticle == null)
            {
                _logger.LogWarning("LearnArticle with id: {Id} not found", id);
                return NotFound();
            }

            LearnArticleViewModel = new CreateLearnArticleViewModel
            {
                LearnArticle = learnArticle,
                TagList = await _context.LearnTag.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                }).ToListAsync()
            };

            _logger.LogInformation("LearnArticleViewModel prepared with LearnArticle ID: {Id}", learnArticle.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is not valid. Errors: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                LearnArticleViewModel.TagList = await _context.LearnTag.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                }).ToListAsync();
                return Page();
            }

            var learnArticle = await _context.LearnArticle.FindAsync(LearnArticleViewModel.LearnArticle.Id);

            if (learnArticle == null)
            {
                _logger.LogWarning("LearnArticle with ID: {Id} not found during update", LearnArticleViewModel.LearnArticle.Id);
                return NotFound();
            }

            // Log before updating
            _logger.LogInformation("Updating LearnArticle ID: {Id} - Before Update: {@LearnArticle}", learnArticle.Id, learnArticle);

            // Updating fields
            learnArticle.Name = LearnArticleViewModel.LearnArticle.Name;
            learnArticle.Heading1 = LearnArticleViewModel.LearnArticle.Heading1;
            learnArticle.Text1 = LearnArticleViewModel.LearnArticle.Text1;
            learnArticle.Heading2 = LearnArticleViewModel.LearnArticle.Heading2;
            learnArticle.Text2 = LearnArticleViewModel.LearnArticle.Text2;
            learnArticle.Heading3 = LearnArticleViewModel.LearnArticle.Heading3;
            learnArticle.Text3 = LearnArticleViewModel.LearnArticle.Text3;
            learnArticle.Heading4 = LearnArticleViewModel.LearnArticle.Heading4;
            learnArticle.Text4 = LearnArticleViewModel.LearnArticle.Text4;
            learnArticle.Heading5 = LearnArticleViewModel.LearnArticle.Heading5;
            learnArticle.Text5 = LearnArticleViewModel.LearnArticle.Text5;
            learnArticle.Url = LearnArticleViewModel.LearnArticle.Url;
            learnArticle.Icon = LearnArticleViewModel.LearnArticle.Icon;
            learnArticle.Desktop = LearnArticleViewModel.LearnArticle.Desktop;
            learnArticle.Web = LearnArticleViewModel.LearnArticle.Web;
            learnArticle.LearnTagId = LearnArticleViewModel.LearnArticle.LearnTagId;

            // Handle Image uploads
            await HandleImageUpload(LearnArticleViewModel.Image1, learnArticle, 1);
            await HandleImageUpload(LearnArticleViewModel.Image2, learnArticle, 2);
            await HandleImageUpload(LearnArticleViewModel.Image3, learnArticle, 3);
            await HandleImageUpload(LearnArticleViewModel.Image4, learnArticle, 4);
            await HandleImageUpload(LearnArticleViewModel.Image5, learnArticle, 5);

            // Log after updating
            _logger.LogInformation("Updating LearnArticle ID: {Id} - After Update: {@LearnArticle}", learnArticle.Id, learnArticle);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("LearnArticle ID: {Id} successfully saved", learnArticle.Id);
                return RedirectToPage("/LearnArticles/Index");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnArticleExists(LearnArticleViewModel.LearnArticle.Id))
                {
                    _logger.LogError("Concurrency error: LearnArticle with ID: {Id} does not exist", LearnArticleViewModel.LearnArticle.Id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency error occurred while saving LearnArticle ID: {Id}", LearnArticleViewModel.LearnArticle.Id);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving LearnArticle ID: {Id}", LearnArticleViewModel.LearnArticle.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        private static async Task HandleImageUpload(IFormFile image, LearnArticle learnArticle, int imageNumber)
        {
            if (image != null)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                switch (imageNumber)
                {
                    case 1:
                        learnArticle.Image1 = memoryStream.ToArray();
                        break;
                    case 2:
                        learnArticle.Image2 = memoryStream.ToArray();
                        break;
                    case 3:
                        learnArticle.Image3 = memoryStream.ToArray();
                        break;
                    case 4:
                        learnArticle.Image4 = memoryStream.ToArray();
                        break;
                    case 5:
                        learnArticle.Image5 = memoryStream.ToArray();
                        break;
                }
            }
        }

        private bool LearnArticleExists(int id)
        {
            return _context.LearnArticle.Any(e => e.Id == id);
        }
    }
}
