using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 10, 23, 16, 20, 164, DateTimeKind.Local).AddTicks(1609));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 10, 19, 54, 38, 77, DateTimeKind.Local).AddTicks(8673));
        }
    }
}
