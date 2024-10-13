using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    /// <inheritdoc />
    public partial class Ordernumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 23, 2, 19, 86, DateTimeKind.Local).AddTicks(3684));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 22, 51, 39, 13, DateTimeKind.Local).AddTicks(4994));
        }
    }
}
