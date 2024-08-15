using Microsoft.AspNetCore.Mvc.Rendering;
using Salto.Models;

public class CreateLearnArticleViewModel
{
    public LearnArticle LearnArticle { get; set; }
    public List<SelectListItem> TagList { get; set; }
    public IFormFile Image1 { get; set; }
    public IFormFile Image2 { get; set; }
    public IFormFile Image3 { get; set; }
    public IFormFile Image4 { get; set; }
    public IFormFile Image5 { get; set; }
}
