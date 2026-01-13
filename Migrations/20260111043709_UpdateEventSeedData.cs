using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Description", "EventDate", "ImageUrl", "Location", "Title", "TotalTickets" },
                values: new object[] { "Chúng tôi cung cấp không gian tổ chức sinh nhật ấm cúng, trang trí theo yêu cầu với phong cách hiện đại hoặc cổ điển. Dịch vụ trọn gói bao gồm tiệc trà, bánh ngọt.", new DateTime(2026, 1, 18, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3558), "/images/events/birthday.png", "Sân vườn Villa", "Tổ chức sinh nhật", 50 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EventDate", "ImageUrl", "Location", "Title", "TotalTickets" },
                values: new object[,]
                {
                    { 2, "Không gian sân vườn rộng rãi thích hợp cho các hoạt động ngoài trời, gắn kết tinh thần đồng đội. Hỗ trợ setup các trò chơi vận động, âm thanh, ánh sáng.", new DateTime(2026, 1, 25, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3562), "/images/events/teambuilding.png", "Bãi cỏ trung tâm", "Teambuilding", 200 },
                    { 3, "Một lễ cưới thân mật, lãng mạn bên những người thân yêu nhất. Không gian được trang hoàng lộng lẫy với hoa tươi, nến và phong cách phục vụ chuẩn 5 sao.", new DateTime(2026, 2, 10, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3564), "/images/events/wedding.png", "Sảnh tiệc chính", "Tiệc cưới nhỏ", 100 },
                    { 4, "Kỷ niệm ngày cưới, gặp mặt bạn cũ hay những cột mốc quan trọng. Chúng tôi mang đến không gian riêng tư, thực đơn phong phú và sự phục vụ tận tâm.", new DateTime(2026, 1, 21, 11, 37, 6, 333, DateTimeKind.Local).AddTicks(3566), "/images/events/celebrate.png", "Nhà hàng ven hồ", "Lễ kỷ niệm", 30 }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "CreatedAt" },
                values: new object[] { new DateTime(2026, 1, 8, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6245), new DateTime(2026, 1, 11, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6249), new DateTime(2026, 1, 6, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6232) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EventDate", "ImageUrl", "Location", "Title", "TotalTickets" },
                values: new object[] { "Show ca nhạc acoustic cực chill tại Rose Villa", new DateTime(2026, 1, 21, 11, 52, 4, 964, DateTimeKind.Local).AddTicks(6273), "/images/events/event1.jpg", "Sân khấu ngoài trời", "Đêm Nhạc Dưới Trăng", 100 });

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
    }
}
