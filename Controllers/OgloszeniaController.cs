using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalOgloszeniowy.Data;
using PortalOgloszeniowy.Models;
using PortalOgloszeniowy.Services;

namespace PortalOgloszeniowy.Controllers;

public class OgloszeniaController : Controller
{
    private readonly AppDbContext _context;
    private readonly WyszukiwarkaOgloszen _wyszukiwarka;

    public OgloszeniaController(AppDbContext context, WyszukiwarkaOgloszen wyszukiwarka)
    {
        _context = context;
        _wyszukiwarka = wyszukiwarka;
    }

    public async Task<IActionResult> Index(Kategoria? kategoria, string? fraza)
    {
        var query = _context.Ogloszenia.AsQueryable();

        if (kategoria.HasValue)
        {
            query = query.Where(o => o.Kategoria == kategoria.Value);
        }

        query = _wyszukiwarka.Filtruj(query, fraza);

        var ogloszenia = await query.ToListAsync();
        var wyniki = _wyszukiwarka.SortujWedlugTrafnosci(ogloszenia, fraza);

        ViewBag.WybranaKategoria = kategoria;
        ViewBag.Fraza = fraza;
        return View(wyniki);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ogloszenie = await _context.Ogloszenia.FindAsync(id);
        if (ogloszenie == null)
        {
            return NotFound();
        }

        return View(ogloszenie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ogloszenie ogloszenie)
    {
        if (ModelState.IsValid)
        {
            ogloszenie.DataUtworzenia = DateTime.Now;
            _context.Add(ogloszenie);
            await _context.SaveChangesAsync();

            TempData["Komunikat"] = "Ogłoszenie zostało dodane.";
            return RedirectToAction(nameof(Index));
        }

        return View(ogloszenie);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ogloszenie = await _context.Ogloszenia.FindAsync(id);
        if (ogloszenie == null)
        {
            return NotFound();
        }

        return View(ogloszenie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ogloszenie ogloszenie)
    {
        if (id != ogloszenie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ogloszenie);
                await _context.SaveChangesAsync();

                TempData["Komunikat"] = "Ogłoszenie zostało zaktualizowane.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OgloszenieExists(ogloszenie.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }

        return View(ogloszenie);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ogloszenie = await _context.Ogloszenia.FindAsync(id);
        if (ogloszenie == null)
        {
            return NotFound();
        }

        return View(ogloszenie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ogloszenie = await _context.Ogloszenia.FindAsync(id);
        if (ogloszenie != null)
        {
            _context.Ogloszenia.Remove(ogloszenie);
            await _context.SaveChangesAsync();

            TempData["Komunikat"] = "Ogłoszenie zostało usunięte.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> OgloszenieExists(int id)
    {
        return await _context.Ogloszenia.AnyAsync(o => o.Id == id);
    }
}
