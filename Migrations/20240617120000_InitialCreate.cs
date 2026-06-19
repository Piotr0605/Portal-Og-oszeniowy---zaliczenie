using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalOgloszeniowy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ogloszenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tytul = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Opis = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false),
                    Kategoria = table.Column<int>(type: "INTEGER", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ogloszenia");
        }
    }
}
