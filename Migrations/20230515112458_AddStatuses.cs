using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rendszerfejlesztes_beadando.Migrations
{
    /// <inheritdoc />
    public partial class AddStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Statuses(Name) VALUES ('New'),('Draft'),('Wait')," +
                $"('Scheduled'),('InProgress'),('Completed'),('Failed')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
