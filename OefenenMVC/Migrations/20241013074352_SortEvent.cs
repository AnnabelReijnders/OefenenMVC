using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    /// <inheritdoc />
    public partial class SortEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Date", "EventType" },
                values: new object[] { new DateTime(2024, 10, 13, 9, 43, 51, 873, DateTimeKind.Local).AddTicks(9969), "Overig" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 23, 22, 25, 36, DateTimeKind.Local).AddTicks(2309));
        }
    }
}
