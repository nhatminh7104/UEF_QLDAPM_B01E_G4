using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenitiesToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBreakfast",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPool",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTowel",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWifi",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 30, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1107), new DateTime(2026, 1, 2, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1112), new DateTime(2025, 12, 28, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1094) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 12, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1133));

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "Sự kiện âm nhạc", null, new DateTime(2025, 12, 28, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(2502), "/images/news/news1.jpg", "Music Concert Night" },
                    { 2, "Tin tức", null, new DateTime(2025, 12, 28, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(2507), "/images/news/news2.jpg", "New Villa Opening" }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasBreakfast", "HasPool", "HasTowel", "HasWifi" },
                values: new object[] { true, true, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasBreakfast", "HasPool", "HasTowel", "HasWifi" },
                values: new object[] { true, true, true, true });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 28, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 7, 10, 47, 49, 793, DateTimeKind.Local).AddTicks(1193));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropColumn(
                name: "HasBreakfast",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HasPool",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HasTowel",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HasWifi",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 30, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(2930), new DateTime(2026, 1, 2, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(2935), new DateTime(2025, 12, 28, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 12, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 28, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 7, 10, 38, 25, 931, DateTimeKind.Local).AddTicks(3013));
        }
    }
}
