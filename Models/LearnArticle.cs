using System.ComponentModel.DataAnnotations;

namespace Salto.Models
{
    public class LearnArticle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image1 { get; set; }
        public string Heading1 { get; set; }
        public string Text1 { get; set; }
        public byte[] Image2 { get; set; }
        public string Heading2 { get; set; }
        public string Text2 { get; set; }
        public byte[] Image3 { get; set; }
        public string Heading3 { get; set; }
        public string Text3 { get; set; }
        public byte[] Image4 { get; set; }
        public string Heading4 { get; set; }
        public string Text4 { get; set; }
        public byte[] Image5 { get; set; }
        public string Heading5 { get; set; }
        public string Text5 { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Desktop { get; set; }
        public bool Web { get; set; }

        // Foreign Key Property
        public int LearnTagId { get; set; }

        // Navigation Property
        public LearnTag LearnTag { get; set; }
    }

}
