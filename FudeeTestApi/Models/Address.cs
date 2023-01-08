using System.ComponentModel.DataAnnotations;

namespace FudeeTestApi.Models
{
    public class Address
    {
        [Key]
        [Display(Name = "Identyfikator ")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę miejscowości.")]
        [Display(Name = "Miasto: ")]
        [MaxLength(30, ErrorMessage = "Nazwa miejscowości nie może być dłuższa niż 30 znaków.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę ulicy.")]
        [Display(Name = "Ulica: ")]
        [MaxLength(75, ErrorMessage = "Nazwa ulicy nie może być dłuższa niż 75 znaków.")]
        public string? StreetName { get; set; }

        [Required(ErrorMessage = "Proszę podać numer ulicy.")]
        [Display(Name = "Numer ulicy: ")]
        [MaxLength(30)]
        public string? StreetNr { get; set; }

        [Required(ErrorMessage = "Proszę podać numer lokalu.")]
        [Display(Name = "Numer lokalu: ")]
        [MaxLength(30)]
        public string? LocalNr { get; set; }

        [Required(ErrorMessage = "Proszę podać kod pocztowy.")]
        [Display(Name = "Kod pocztowy: ")]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Proszę podać adres e-mail.")]
        [Display(Name = "E-mail: ")]
        [MaxLength(30)]
        public string? ContactEmail { get; set; }

        [Required(ErrorMessage = "Proszę podać numer kontaktowy.")]
        [Display(Name = "Numer kontaktowy: ")]
        [MaxLength(30)]
        public string? ContactPhone { get; set;}

        //restauracja
        public virtual Restaurant? Restaurant { get; set; }
    }
    
}
