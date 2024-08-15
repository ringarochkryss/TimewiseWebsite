using Microsoft.CodeAnalysis.Emit;
using System.ComponentModel.DataAnnotations;

namespace Salto.Models
{
    public class Prices
    {
        public int PricesID { get; set; }
        [Display(Name = "Prisplan Namn SWE")]
        public string PricesNameSWE { get; set; }
        [Display(Name = "Price Plan Name ENG")]
        public string PricesNameENG { get; set; }
        [Display(Name = "ca 3 punkter som beskriver features (kommaseparerad input) SWE")]
        public string PointsSWE { get; set; }
        [Display(Name = "Short Bullet list describing features (comma separated input) ENG")]
        public string PointsENG { get; set; }
        [Display(Name = "Price in SEK (write just a number)")]
        public int Price { get; set; }
        [Display(Name = "Villkor för tjänsten, t.ex 1.400kr / månad 20 licenser SWE")]
        public string QuitRuleSWE { get; set; }
        [Display(Name = "Terms in ENG")]
        public string QuitRuleENG { get; set; }
        [Display(Name = "Rubrik ovanför punkter som beskriver innehåll (exempel: Allt i Starter plus:) SWE")]
        public string DescriptionTitleSWE { get; set; }
        [Display(Name = "Header above description of content ENG")]
        public string DescriptionTitleENG { get; set; }
        [Display(Name = "Beskrivande checklista över innehåll, ord eller meningar (kommaseparerad input) SWE")]
        public string DescriptionsSWE { get; set; }
        [Display(Name = "Check List description of content ENG")]
        public string DescriptionsENG { get; set; }
        [Display(Name = "Show Button Buy now!")]
        public bool ButtonBuyNow { get; set; }
        [Display(Name = "Show Button Test for Free!")]
        public bool ButtonTestForFree { get; set; }
        [Display(Name = "Show Button Contact Us!")]
        public bool ButtonContactUs { get; set; }


    }
}
