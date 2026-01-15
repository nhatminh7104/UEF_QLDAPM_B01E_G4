using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryRoomImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryRoomImages_RoomCategories_RoomCategoryId",
                        column: x => x.RoomCategoryId,
                        principalTable: "RoomCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1449), new DateTime(2026, 1, 19, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1457), new DateTime(2026, 1, 14, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1436) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 21, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1479));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2026, 1, 28, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1482));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2026, 2, 13, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1484));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "EventDate",
                value: new DateTime(2026, 1, 24, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1485));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(2961));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2026, 1, 14, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TourDate" },
                values: new object[] { new DateTime(2026, 1, 14, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1549), new DateTime(2026, 1, 24, 15, 41, 46, 839, DateTimeKind.Local).AddTicks(1550) });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRoomImages_RoomCategoryId",
                table: "CategoryRoomImages",
                column: "RoomCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRoomImages");

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
                column: "EventDate",
                value: new DateTime(2026, 1, 21, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2026, 1, 28, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2026, 2, 13, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "EventDate",
                value: new DateTime(2026, 1, 24, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1027));

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
                column: "BookingDate",
                value: new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1053));

            migrationBuilder.UpdateData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TourDate" },
                values: new object[] { new DateTime(2026, 1, 14, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1095), new DateTime(2026, 1, 24, 14, 42, 11, 113, DateTimeKind.Local).AddTicks(1097) });
        }
    }
}
