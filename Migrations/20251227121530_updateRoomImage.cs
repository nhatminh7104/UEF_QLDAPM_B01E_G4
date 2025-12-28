using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateRoomImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2739), new DateTime(2026, 1, 1, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2747), new DateTime(2025, 12, 27, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2725) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 11, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2765));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 27, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 6, 19, 15, 29, 968, DateTimeKind.Local).AddTicks(2857));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4871), new DateTime(2026, 1, 1, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4876), new DateTime(2025, 12, 27, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4857) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 11, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4894));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2025, 12, 27, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 6, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4985));
        }
    }
}
