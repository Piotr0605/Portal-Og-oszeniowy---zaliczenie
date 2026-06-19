using System.ComponentModel.DataAnnotations;

namespace PortalOgloszeniowy.Models;

public enum Kategoria
{
    Motoryzacja,
    [Display(Name = "Nieruchomości")]
    Nieruchomosci,
    Elektronika,
    Praca,
    Inne
}
