using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    /// <inheritdoc />
    public partial class RemoveThemes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 22, 51, 39, 13, DateTimeKind.Local).AddTicks(4994));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 22, 50, 47, 287, DateTimeKind.Local).AddTicks(7165));
        }
    }
}
