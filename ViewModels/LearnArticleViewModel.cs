using Salto.Models;

namespace Salto.ViewModels
{
    public class LearnArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image1 { get; set; }
        public string Heading1 { get; set; }
        public string Text1 { get; set; }
        public IFormFile Image2 { get; set; }
        public string Heading2 { get; set; }
        public string Text2 { get; set; }
        public IFormFile Image3 { get; set; }
        public string Heading3 { get; set; }
        public string Text3 { get; set; }
        public IFormFile Image4 { get; set; }
        public string Heading4 { get; set; }
        public string Text4 { get; set; }
        public IFormFile Image5 { get; set; }
        public string Heading5 { get; set; }
        public string Text5 { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Desktop { get; set; }
        public bool Web { get; set; }
        public int LearnTagId { get; set; }
        public List<LearnTag> LearnTags { get; set; }

    }
}
