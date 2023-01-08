using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FudeeTestApi.Models
{
    public class Opinion
    {
        [Key]
        [Display(Name = "Identyfikator:")]
        public int OpinionId { get; set; }

        [Required(ErrorMessage = "Proszę podać treść komentarza.")]
        [Display(Name = "Treść komentarza:")]
        [DataType(DataType.MultilineText)]
        public string? Comment { get; set; }

        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public System.DateTime AddedDate { get; set; }

        [Display(Name = "Ocena restauracji:")]
        public TypeOfGrade? Rating { get; set; }

        [Required]
        [Display(Name = "Komentowana restauracja:")]
        public int RestaurantId { get; set; }
        //Komentowana restauracja
        [ForeignKey("RestaurantId")]
        public virtual Restaurant? Restaurant { get; set; }

        [Display(Name = "Autor komentarza:")]
        public string? Id { get; set; }
        //Autor komentarza
        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }
    }

    public enum TypeOfGrade
    {
        pyszna = 5,
        dobra = 4,
        przeciętna = 3,
        słaba = 2,
        okropna = 1,
    }
}
