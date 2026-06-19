namespace PortalOgloszeniowy.Models;

public static class PomocnikKategorii
{
    public static string WyswietlNazwe(Kategoria kategoria)
    {
        return kategoria switch
        {
            Kategoria.Motoryzacja => "Motoryzacja",
            Kategoria.Nieruchomosci => "Nieruchomości",
            Kategoria.Elektronika => "Elektronika",
            Kategoria.Praca => "Praca",
            Kategoria.Inne => "Inne",
            _ => kategoria.ToString()
        };
    }
}
