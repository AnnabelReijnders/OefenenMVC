using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    public partial class CreateThemesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Maak de Themes tabel aan
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeID);
                });

            // Voeg hier voorbeelddata toe als dat nodig is
            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "ThemeID", "Name" },
                values: new object[,]
                {
                    { 1, "Foodtruckfestivals" },
                    { 2, "Theatervoorstellingen" },
                    { 3, "Dating Events" },
                    { 4, "Muziekoptredens" },
                    { 5, "Festivals" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
