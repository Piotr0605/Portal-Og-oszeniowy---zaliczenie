using System.ComponentModel.DataAnnotations;

namespace PortalOgloszeniowy.Models;

public class Ogloszenie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [StringLength(200, ErrorMessage = "Tytuł może mieć maksymalnie 200 znaków")]
    [Display(Name = "Tytuł")]
    public string Tytul { get; set; } = string.Empty;

    [Required(ErrorMessage = "Opis jest wymagany")]
    [StringLength(2000, ErrorMessage = "Opis może mieć maksymalnie 2000 znaków")]
    [Display(Name = "Opis")]
    public string Opis { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cena jest wymagana")]
    [Range(0, 9999999.99, ErrorMessage = "Cena musi być większa lub równa 0")]
    [Display(Name = "Cena (PLN)")]
    public decimal Cena { get; set; }

    [Required(ErrorMessage = "Kategoria jest wymagana")]
    [Display(Name = "Kategoria")]
    public Kategoria Kategoria { get; set; }

    [Display(Name = "Data dodania")]
    public DateTime DataUtworzenia { get; set; }
}
