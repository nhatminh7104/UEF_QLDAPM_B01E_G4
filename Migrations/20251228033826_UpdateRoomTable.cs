using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
