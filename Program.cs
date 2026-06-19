using Microsoft.EntityFrameworkCore;
using PortalOgloszeniowy.Data;
using PortalOgloszeniowy.Models;
using PortalOgloszeniowy.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<WyszukiwarkaOgloszen>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        if (!db.Ogloszenia.Any())
        {
            db.Ogloszenia.AddRange(
                new Ogloszenie
                {
                    Tytul = "Sprzedam laptop Dell",
                    Opis = "Laptop w bardzo dobrym stanie, 16 GB RAM, dysk SSD 512 GB. Idealny do nauki i pracy.",
                    Cena = 2200,
                    Kategoria = Kategoria.Elektronika,
                    DataUtworzenia = DateTime.Now.AddDays(-3)
                },
                new Ogloszenie
                {
                    Tytul = "Mieszkanie 2 pokoje - centrum",
                    Opis = "Przestronne mieszkanie po remoncie, blisko uczelni i komunikacji miejskiej.",
                    Cena = 2500,
                    Kategoria = Kategoria.Nieruchomosci,
                    DataUtworzenia = DateTime.Now.AddDays(-1)
                },
                new Ogloszenie
                {
                    Tytul = "Szukam pracy na wakacje",
                    Opis = "Student 3 roku informatyki, dostępny od czerwca. Chętnie podejmę się pracy w IT lub biurze.",
                    Cena = 0,
                    Kategoria = Kategoria.Praca,
                    DataUtworzenia = DateTime.Now
                }
            );
            db.SaveChanges();
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ogloszenia}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
