using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 30, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4364), new DateTime(2026, 1, 2, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4369), new DateTime(2025, 12, 28, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4348) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 12, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4388));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 28, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4408));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 7, 10, 31, 16, 295, DateTimeKind.Local).AddTicks(4450));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6680), new DateTime(2026, 1, 1, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6686), new DateTime(2025, 12, 27, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6661) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 11, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6712));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 27, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 6, 23, 35, 47, 948, DateTimeKind.Local).AddTicks(6780));
        }
    }
}
