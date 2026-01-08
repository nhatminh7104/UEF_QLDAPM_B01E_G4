using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bedrooms",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Beds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CapacityChildren",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 7, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2350), new DateTime(2026, 1, 10, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2354), new DateTime(2026, 1, 5, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2335) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 20, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(3872));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bedrooms", "Beds", "CapacityChildren" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bedrooms", "Beds", "CapacityChildren" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 5, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 15, 17, 58, 24, 619, DateTimeKind.Local).AddTicks(2443));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bedrooms",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CapacityChildren",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 7, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(133), new DateTime(2026, 1, 10, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(138), new DateTime(2026, 1, 5, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(118) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 20, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(161));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(1875));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(1877));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 5, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(183));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 15, 17, 53, 51, 541, DateTimeKind.Local).AddTicks(222));
        }
    }
}
