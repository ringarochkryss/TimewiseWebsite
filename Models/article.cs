using System.ComponentModel.DataAnnotations;

namespace Salto.Models;

public class Article
{
    [Display (Name = "Number")]
    public int ArticleID { get; set; }
    [StringLength (160)]
    [Display(Name = "Title SWE")]
    public string ArticleTitleSWE { get; set; }
    [Display(Name = "Title ENG")]
    public string ArticleTitleENG { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }
    [Display(Name = "Content SWE")]
    public string ArticleContentSWE { get; set; }
    [Display(Name = "Content ENG")]
    public string ArticleContentENG { get; set; }
    public byte[] Image { get; set; }

    public string ImagePath { get; set; } // add this property

    public string ImageMimeType { get; set; }

    public string ImageFileName { get; set; }

    public string ImageCaption { get; set; }

    public string ImageAltText { get; set; }

    public string ImageDescription { get; set; }

    public string ImageCredit { get; set; }
    public bool Archived { get; set; }
    public bool Featured { get; set; }
    public int FileID { get; set; }
    public int TagID { get; set; }
    public Tag Tag { get; set; }
    
}
