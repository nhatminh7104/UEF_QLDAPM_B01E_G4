using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContactInfo",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TourBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 16, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(991), new DateTime(2026, 1, 19, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(997), new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(978) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "EventDate", "StartDate" },
                values: new object[] { null, new DateTime(2026, 1, 21, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1022), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "EventDate", "StartDate" },
                values: new object[] { null, new DateTime(2026, 1, 28, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1024), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "EventDate", "StartDate" },
                values: new object[] { null, new DateTime(2026, 2, 13, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1026), null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "EventDate", "StartDate" },
                values: new object[] { null, new DateTime(2026, 1, 24, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1027), null });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(2537));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(2540));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDate", "CustomerPhone" },
                values: new object[] { new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1053), null });

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CustomerEmail", "CustomerPhone", "Note", "TourDate" },
                values: new object[] { new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1095), null, null, null, new DateTime(2026, 1, 24, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1097) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "ContactInfo",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 13, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2874), new DateTime(2026, 1, 16, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2880), new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2859) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 18, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2026, 1, 25, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2026, 2, 10, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2917));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "EventDate",
                value: new DateTime(2026, 1, 21, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2919));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 21, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(3044));
        }
    }
}
