using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "TourBookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt", "CustomerId" },
                values: new object[] { new DateTime(2026, 1, 13, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2874), new DateTime(2026, 1, 16, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2880), new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2859), null });

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
                columns: new[] { "BookingDate", "CustomerId" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(2946), null });

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerId", "TourDate" },
                values: new object[] { null, new DateTime(2026, 1, 21, 12, 41, 35, 561, DateTimeKind.Local).AddTicks(3044) });

            migrationBuilder.CreateIndex(
                name: "IX_TourBookings_CustomerId",
                table: "TourBookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Customers_CustomerId",
                table: "Tickets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Customers_CustomerId",
                table: "TourBookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Customers_CustomerId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Customers_CustomerId",
                table: "TourBookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_TourBookings_CustomerId",
                table: "TourBookings");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 13, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3390), new DateTime(2026, 1, 16, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3529), new DateTime(2026, 1, 11, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3375) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 18, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2026, 1, 25, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3562));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2026, 2, 10, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "EventDate",
                value: new DateTime(2026, 1, 21, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3566));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(5114));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 11, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3591));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TourDate",
                value: new DateTime(2026, 1, 21, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3634));
        }
    }
}
