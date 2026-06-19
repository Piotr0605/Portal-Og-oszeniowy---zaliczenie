using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalOgloszeniowy.Data;

#nullable disable

namespace PortalOgloszeniowy.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("PortalOgloszeniowy.Models.Ogloszenie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cena")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUtworzenia")
                        .HasColumnType("TEXT");

                    b.Property<int>("Kategoria")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ogloszenia");
                });
#pragma warning restore 612, 618
        }
    }
}
