using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoziMe.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mailAdresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odgovornaOsoba = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    spol = table.Column<int>(type: "int", nullable: false),
                    datumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mailAdresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaxiStajaliste",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<int>(type: "int", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brojMjesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiStajaliste", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proizvodjac = table.Column<int>(type: "int", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    godinaProizvodnje = table.Column<int>(type: "int", nullable: false),
                    registarskaOznaka = table.Column<int>(type: "int", nullable: false),
                    boja = table.Column<int>(type: "int", nullable: false),
                    brojSjedista = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.id);
                    table.ForeignKey(
                        name: "FK_Admin_Osoba_id",
                        column: x => x.id,
                        principalTable: "Osoba",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ocjena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.id);
                    table.ForeignKey(
                        name: "FK_Klijent_Osoba_id",
                        column: x => x.id,
                        principalTable: "Osoba",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vozac",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    brojVozackeDozvole = table.Column<int>(type: "int", nullable: false),
                    ocjena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozac", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vozac_Osoba_id",
                        column: x => x.id,
                        principalTable: "Osoba",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voznje",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vozacId = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<int>(type: "int", nullable: false),
                    firmaId = table.Column<int>(type: "int", nullable: false),
                    voziloId = table.Column<int>(type: "int", nullable: false),
                    vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ocjena = table.Column<int>(type: "int", nullable: false),
                    cijena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    adresaPolazista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresaDolazista = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voznje", x => x.id);
                    table.ForeignKey(
                        name: "FK_Voznje_Firma_firmaId",
                        column: x => x.firmaId,
                        principalTable: "Firma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voznje_Klijent_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Klijent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voznje_Vozac_vozacId",
                        column: x => x.vozacId,
                        principalTable: "Vozac",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voznje_Vozilo_voziloId",
                        column: x => x.voziloId,
                        principalTable: "Vozilo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_firmaId",
                table: "Voznje",
                column: "firmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_korisnikId",
                table: "Voznje",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_vozacId",
                table: "Voznje",
                column: "vozacId");

            migrationBuilder.CreateIndex(
                name: "IX_Voznje_voziloId",
                table: "Voznje",
                column: "voziloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "TaxiStajaliste");

            migrationBuilder.DropTable(
                name: "Voznje");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropTable(
                name: "Vozac");

            migrationBuilder.DropTable(
                name: "Vozilo");

            migrationBuilder.DropTable(
                name: "Osoba");
        }
    }
}
