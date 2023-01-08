using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FudeeTestApi.Models
{
    public class Restaurant
    {
        [Key]
        [Display(Name = "Identyfikator:")]
        public int RestaurantId { get; set; }

        [Required]
        [Display(Name = "Nazwa restauracji:")]
        [MaxLength(75, ErrorMessage = "Nazwa restauracji nie może przekraczać 75 znaków")]
        public string? RestaurantName { get; set; }

        [Display(Name ="Adres: ")]
        public int AddressId { get; set; }
        //adres restauracji
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }

        [Display(Name = "Logo restauracji:")]
        [MaxLength(128)]
        [FileExtensions(Extensions = ". jpg,. png,. gif", ErrorMessage = "Niepoprawne rozszerzenie pliku.")]
        public string? Logo { get; set; }

        [Required(ErrorMessage = "Proszę podać opis restauracji.")]
        [Display(Name = "Opis restauracji: ")]
        [MaxLength(300, ErrorMessage = "Opis restauracji nie może być dłuższy niż 300 znaków.")]
        public string? RestaurantDescription { get; set; }

        [Required]
        [Display(Name = "Godzina otwarcia:")]
        [DataType(DataType.Time, ErrorMessage = "Niepoprawny format godziny")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public System.DateTime OpenHours { get; set; }

        [Required]
        [Display(Name = "Godzina zamknięcia:")]
        [DataType(DataType.Time, ErrorMessage = "Niepoprawny format godziny")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public System.DateTime CloseHours { get; set; }

        [Required]
        [Display(Name = "Dostawy:")]
        public bool HasDelivery { get; set; }

        [Required]
        [Display(Name = "Catering:")]
        public bool HasCatering { get; set; }

        [Required]
        [Display(Name = "Imprezy okolicznościowe:")]
        public bool Events { get; set; }

        [Display(Name = "Właściciel/Manager:")]
        public string? Id { get; set; }
        //Właściciel restauracji
        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }

        [Display(Name = "Rodzaj kuchni:")]
        public int CategoryId { get; set; }
        //Kategoria restauracji
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Display(Name = "SocialMedia:")]
        [MaxLength(128)]
        public string? SocialMedia { get; set; }

        [Required]
        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public System.DateTime AddedDate { get; set;}

        //lista wszystkich opinii
        public virtual List<Opinion>? Opinions { get; set; }

        //lista wszystkich dań
        public virtual List<Dish>? Dishes { get; set; }

    }
}
