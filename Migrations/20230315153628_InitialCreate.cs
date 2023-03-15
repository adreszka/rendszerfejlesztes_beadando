using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rendszerfejlesztes_beadando.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alkatresz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alkatresz_megnevezes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ar = table.Column<int>(type: "int", nullable: false),
                    MaxTarolasRekeszenkent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alkatresz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Megrendelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefonszam = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adoszam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Megrendelo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statusz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statusz_megnevezes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raktar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlkatreszId = table.Column<int>(type: "int", nullable: false),
                    Darabszam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raktar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raktar_Alkatresz_AlkatreszId",
                        column: x => x.AlkatreszId,
                        principalTable: "Alkatresz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Helyszin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Leiras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MegrendeloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekt_Megrendelo_MegrendeloId",
                        column: x => x.MegrendeloId,
                        principalTable: "Megrendelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Naplo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjektID = table.Column<int>(type: "int", nullable: false),
                    StatuszID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naplo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Naplo_Projekt_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Naplo_Statusz_StatuszID",
                        column: x => x.StatuszID,
                        principalTable: "Statusz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Naplo_ProjektID",
                table: "Naplo",
                column: "ProjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Naplo_StatuszID",
                table: "Naplo",
                column: "StatuszID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekt_MegrendeloId",
                table: "Projekt",
                column: "MegrendeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Raktar_AlkatreszId",
                table: "Raktar",
                column: "AlkatreszId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Naplo");

            migrationBuilder.DropTable(
                name: "Raktar");

            migrationBuilder.DropTable(
                name: "Projekt");

            migrationBuilder.DropTable(
                name: "Statusz");

            migrationBuilder.DropTable(
                name: "Alkatresz");

            migrationBuilder.DropTable(
                name: "Megrendelo");
        }
    }
}
