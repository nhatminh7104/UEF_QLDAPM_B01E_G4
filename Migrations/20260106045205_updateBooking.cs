using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "RoomCategories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RoomCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdentityCard",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt", "IdentityCard", "Nationality" },
                values: new object[] { new DateTime(2026, 1, 8, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6245), new DateTime(2026, 1, 11, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6249), new DateTime(2026, 1, 6, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6232), null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 21, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 6, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(7745));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 6, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(7747));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 6, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 16, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6333));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityCard",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "RoomCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RoomCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 7, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(99), new DateTime(2026, 1, 10, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(101), new DateTime(2026, 1, 5, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(85) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 20, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(122));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(1573));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 5, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(1577));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 5, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 15, 20, 54, 23, 868, DateTimeKind.Local).AddTicks(180));
        }
    }
}
