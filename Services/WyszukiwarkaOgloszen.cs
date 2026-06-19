using System.Text;
using PortalOgloszeniowy.Models;

namespace PortalOgloszeniowy.Services;

public class WyszukiwarkaOgloszen
{
    public IQueryable<Ogloszenie> Filtruj(IQueryable<Ogloszenie> zapytanie, string? fraza)
    {
        if (string.IsNullOrWhiteSpace(fraza))
        {
            return zapytanie;
        }

        foreach (var slowo in RozbijNaSlowa(fraza))
        {
            var szukane = slowo;
            zapytanie = zapytanie.Where(o =>
                o.Tytul.ToLower().Contains(szukane) ||
                o.Opis.ToLower().Contains(szukane));
        }

        return zapytanie;
    }

    public List<Ogloszenie> SortujWedlugTrafnosci(IEnumerable<Ogloszenie> ogloszenia, string? fraza)
    {
        if (string.IsNullOrWhiteSpace(fraza))
        {
            return ogloszenia
                .OrderByDescending(o => o.DataUtworzenia)
                .ToList();
        }

        var slowa = RozbijNaSlowa(fraza);
        var frazaZnormalizowana = NormalizujTekst(fraza);

        return ogloszenia
            .Select(o => new
            {
                Ogloszenie = o,
                Wynik = ObliczTrafnosc(o, slowa, frazaZnormalizowana)
            })
            .Where(x => x.Wynik > 0)
            .OrderByDescending(x => x.Wynik)
            .ThenByDescending(x => x.Ogloszenie.DataUtworzenia)
            .Select(x => x.Ogloszenie)
            .ToList();
    }

    public static string NormalizujTekst(string tekst)
    {
        var builder = new StringBuilder(tekst.Length);

        foreach (var znak in tekst.Trim().ToLowerInvariant())
        {
            builder.Append(ZamienPolskiZnak(znak));
        }

        return builder.ToString();
    }

    private static char ZamienPolskiZnak(char znak)
    {
        return znak switch
        {
            'ą' => 'a',
            'ć' => 'c',
            'ę' => 'e',
            'ł' => 'l',
            'ń' => 'n',
            'ó' => 'o',
            'ś' => 's',
            'ź' => 'z',
            'ż' => 'z',
            _ => znak
        };
    }

    public static string[] RozbijNaSlowa(string fraza)
    {
        return NormalizujTekst(fraza)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    public static int ObliczTrafnosc(Ogloszenie ogloszenie, string[] slowa, string frazaZnormalizowana)
    {
        var tytul = NormalizujTekst(ogloszenie.Tytul);
        var opis = NormalizujTekst(ogloszenie.Opis);
        var wynik = 0;

        if (tytul == frazaZnormalizowana)
        {
            wynik += 100;
        }
        else if (tytul.StartsWith(frazaZnormalizowana, StringComparison.Ordinal))
        {
            wynik += 80;
        }
        else if (tytul.Contains(frazaZnormalizowana, StringComparison.Ordinal))
        {
            wynik += 60;
        }

        foreach (var slowo in slowa)
        {
            if (tytul.Contains(slowo, StringComparison.Ordinal))
            {
                wynik += 20;
            }

            if (opis.Contains(slowo, StringComparison.Ordinal))
            {
                wynik += 10;
            }
        }

        return wynik;
    }
}
