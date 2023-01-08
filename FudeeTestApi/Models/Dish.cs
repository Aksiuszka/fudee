using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FudeeTestApi.Models
{
    public class Dish
    {
        [Key]
        [Display(Name = "Identyfikator ")]
        public int DishId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę dania.")]
        [Display(Name = "Nazwa dania: ")]
        [MaxLength(50, ErrorMessage = "Nazwa miejscowości nie może być dłuższa niż 50 znaków.")]
        public string? NameDish { get; set; }

        [Display(Name = "Zdjęcie dania:")]
        [MaxLength(128)]
        [FileExtensions(Extensions = ". jpg,. png,. gif", ErrorMessage = "Niepoprawne rozszerzenie pliku.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Proszę podać opis dania.")]
        [Display(Name = "Opis dania: ")]
        [MaxLength(125, ErrorMessage = "Opis dania nie może być dłuższa niż 125 znaków.")]
        public string? DescriptionDish { get; set; }

        [Required(ErrorMessage = "Proszę podać cenę dania.")]
        [Display(Name = "Cena: ")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Danie restauracji:")]
        public int RestaurantId { get; set; }
        //Danie restauracji
        [ForeignKey("RestaurantId")]
        public virtual Restaurant? Restaurant { get; set; }


    }
}
