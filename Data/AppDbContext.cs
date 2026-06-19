using Microsoft.EntityFrameworkCore;
using PortalOgloszeniowy.Models;

namespace PortalOgloszeniowy.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ogloszenie> Ogloszenia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ogloszenie>(entity =>
        {
            entity.Property(o => o.Tytul).HasMaxLength(200);
            entity.Property(o => o.Opis).HasMaxLength(2000);
        });
    }
}
