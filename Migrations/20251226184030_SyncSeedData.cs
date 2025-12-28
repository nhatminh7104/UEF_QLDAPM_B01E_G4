using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class SyncSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TourBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingStars",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SquareFootage",
                table: "Rooms",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdultsCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RoomImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImage_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AdultsCount", "CheckIn", "CheckOut", "ChildrenCount", "CreatedAt", "CustomerEmail", "CustomerName", "CustomerPhone", "Notes", "PaymentMethod", "RoomId", "Status", "TotalAmount" },
                values: new object[] { 1, 2, new DateTime(2025, 12, 29, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4871), new DateTime(2026, 1, 1, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4876), 1, new DateTime(2025, 12, 27, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4857), "a@gmail.com", "Nguyễn Văn A", "0901234567", null, "Credit Card", 1, "Confirmed", 15000000m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventDate", "ImageUrl" },
                values: new object[] { new DateTime(2026, 1, 11, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4894), "/images/events/event1.jpg" });

            migrationBuilder.InsertData(
                table: "RoomImage",
                columns: new[] { "Id", "ImageUrl", "RoomId" },
                values: new object[] { 1, "/images/rooms/villa1-1.jpg", 1 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "RatingStars", "SquareFootage" },
                values: new object[] { "Villa view rừng cực đẹp", null, 5, 150.0 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "RatingStars", "SquareFootage" },
                values: new object[] { "Không gian lãng mạn", null, 4, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDate", "CustomerEmail", "CustomerName", "EventId", "IsUsed", "Price", "QRCode", "Quantity", "TicketType" },
                values: new object[] { 1, new DateTime(2025, 12, 27, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4913), "b@gmail.com", "Trần Thị B", 1, false, 500000m, "QR_EVT_001", 2, "VIP" });

            migrationBuilder.InsertData(
                table: "TourBookings",
                columns: new[] { "Id", "ContactInfo", "CustomerName", "NumberOfPeople", "Status", "TotalPrice", "TourDate", "TourId" },
                values: new object[] { 1, "0988777666", "Lê Văn C", 4, "Pending", 2000000m, new DateTime(2026, 1, 6, 1, 40, 29, 484, DateTimeKind.Local).AddTicks(4985), 1 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Khám phá vẻ đẹp hoang sơ của núi rừng Ba Vì cùng hướng dẫn viên bản địa.", "/images/tours/trekking-bavi.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomImage_RoomId",
                table: "RoomImage",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomImage");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TourBookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RatingStars",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "SquareFootage",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdultsCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 1, 10, 23, 16, 20, 164, DateTimeKind.Local).AddTicks(1609));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Description", "IsActive", "PricePerNight", "RoomNumber", "Type" },
                values: new object[] { 3, 8, null, true, 4500000m, "V02", "Villa" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Khám phá vẻ đẹp thiên nhiên");
        }
    }
}
