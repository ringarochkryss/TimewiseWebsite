using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Salto.Pages.ViewComponents
{
    public class LanguageSelectorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userLanguages = HttpContext.Request.Headers["Accept-Language"].ToString().Split(',');
            var defaultLanguage = "English"; // Default language if no preferred language is detected

            foreach (var lang in userLanguages)
            {
                if (lang.StartsWith("sv", StringComparison.OrdinalIgnoreCase))
                {
                    defaultLanguage = "Swedish";
                    break;
                }
                // Add more language checks as needed
            }

            ViewData["DefaultLanguage"] = defaultLanguage;
            return View("Default");
        }
    }
}

