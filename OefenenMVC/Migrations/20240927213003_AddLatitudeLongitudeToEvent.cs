using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddLatitudeLongitudeToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Events",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Events",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Date", "Latitude", "Longitude" },
                values: new object[] { new DateTime(2024, 9, 27, 23, 30, 3, 271, DateTimeKind.Local).AddTicks(7780), null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 27, 23, 24, 43, 710, DateTimeKind.Local).AddTicks(5815));
        }
    }
}
