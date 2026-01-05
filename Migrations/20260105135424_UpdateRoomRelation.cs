using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomCategoryId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BannerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCategories", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "RoomCategories",
                columns: new[] { "Id", "Amenities", "BannerUrl", "Description", "Name", "ShortDescription" },
                values: new object[,]
                {
                    { 1, null, "...", null, "Wooden House", null },
                    { 2, null, "...", null, "Khu Villa", null },
                    { 3, null, "...", null, "Rose House", null }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoomCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoomCategoryId",
                value: 1);

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

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomCategoryId",
                table: "Rooms",
                column: "RoomCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomCategories_RoomCategoryId",
                table: "Rooms",
                column: "RoomCategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomCategories_RoomCategoryId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomCategories");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomCategoryId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomCategoryId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                column: "Category",
                value: "Villa");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Category",
                value: "Wooden House");

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
    }
}
