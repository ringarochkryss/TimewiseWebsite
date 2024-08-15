using System.ComponentModel.DataAnnotations;

namespace Salto.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagNameSWE { get; set; }
        public string TagNameENG { get; set; }
        [Display(Name = "Icon Css Class Name  (t.ex: fas fa-car) ")]
        public string IconClass { get; set; }
        public bool Archived { get; set; }
        public ICollection<Article> ArticleFile { get; set; }

    }
}
