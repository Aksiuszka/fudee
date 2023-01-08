using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FudeeTestApi.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Identyfikator kategorii:")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kategorii.")]
        [Display(Name = "Nazwa kategorii: ")]
        [MaxLength(50, ErrorMessage = "Nazwa kategorii  nie może być dłuższa niż 50 znaków.")]
        public string? NameCategory { get; set; }

        [Required(ErrorMessage = "Proszę podać opis kategorii.")]
        [Display(Name = "Opis kategorii: ")]
        [MaxLength(255, ErrorMessage = "Opis kategorii  nie może być dłuższy niż 255 znaków.")]
        public string? DescriptionCategory { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę ikony.")]
        [Display(Name = "Ikona kategorii: ")]
        [MaxLength(30, ErrorMessage = "Nazwa ikony nie może być dłuższa niż 30 znaków.")]
        public string? Icon { get; set; }

        [Required]
        [Display(Name = "Czy aktywna?")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Czy wyświetlać?")]
        public bool Display { get; set; }

        //lista wszystkich restauracji należących do kategorii
        public virtual List<Restaurant>? Restaurants { get; set;}

    }
}
