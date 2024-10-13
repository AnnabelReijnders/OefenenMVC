using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OefenenMVC.Migrations
{
    /// <inheritdoc />
    public partial class Ordernumbertoticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 12, 23, 22, 25, 36, DateTimeKind.Local).AddTicks(2309));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Date", "PostalCode" },
                values: new object[] { new DateTime(2024, 10, 12, 23, 2, 19, 86, DateTimeKind.Local).AddTicks(3684), null });
        }
    }
}
