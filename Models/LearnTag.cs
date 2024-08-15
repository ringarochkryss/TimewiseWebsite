using System.ComponentModel.DataAnnotations;

namespace Salto.Models
{
    public class LearnTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        [Display(Name = "Icon Css Class Name  (example: fas fa-car) ")]
        public string Icon { get; set; }
    }
}
