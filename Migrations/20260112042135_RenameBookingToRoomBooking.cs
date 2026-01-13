using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class RenameBookingToRoomBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.CreateTable(
                name: "RoomBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdultsCount = table.Column<int>(type: "int", nullable: false),
                    ChildrenCount = table.Column<int>(type: "int", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomBookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 27, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 12, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 12, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.InsertData(
                table: "RoomBookings",
                columns: new[] { "Id", "AdultsCount", "CheckIn", "CheckOut", "ChildrenCount", "CreatedAt", "CustomerEmail", "CustomerName", "CustomerPhone", "Notes", "PaymentMethod", "RoomId", "Status", "TotalAmount" },
                values: new object[] { 1, 2, new DateTime(2026, 1, 14, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2610), new DateTime(2026, 1, 17, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2620), 1, new DateTime(2026, 1, 12, 11, 21, 35, 476, DateTimeKind.Local).AddTicks(2620), "a@gmail.com", "Nguyễn Văn A", "0901234567", null, "Credit Card", 1, "Confirmed", 15000000m });

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RoomId",
                table: "RoomBookings",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomBookings");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    AdultsCount = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildrenCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AdultsCount", "CheckIn", "CheckOut", "ChildrenCount", "CreatedAt", "CustomerEmail", "CustomerName", "CustomerPhone", "Notes", "PaymentMethod", "RoomId", "Status", "TotalAmount" },
                values: new object[] { 1, 2, new DateTime(2026, 1, 13, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9190), new DateTime(2026, 1, 16, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9200), 1, new DateTime(2026, 1, 11, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9200), "a@gmail.com", "Nguyễn Văn A", "0901234567", null, "Credit Card", 1, "Confirmed", 15000000m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 26, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9220));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 13, 1, 41, 578, DateTimeKind.Local).AddTicks(9270));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");
        }
    }
}
