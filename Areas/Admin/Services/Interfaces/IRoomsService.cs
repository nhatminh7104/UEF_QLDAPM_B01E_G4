using VillaManagementWeb.Models;
using Microsoft.AspNetCore.Http; // Cần thiết để sử dụng IFormFile

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IRoomsService
    {
        // --- Truy vấn dữ liệu ---
        Task<IEnumerable<Room>> GetAllRoomsAsync();

        // Lấy danh sách phòng kèm theo trạng thái (thường dùng cho trang Index)
        Task<IEnumerable<Room>> GetRoomsWithStatusAsync();

        Task<Room?> GetRoomByIdAsync(int id);

        // Lấy phòng kèm theo danh sách ảnh trong album (dùng cho trang Edit)
        Task<Room?> GetRoomWithImagesAsync(int id);

        // Lấy danh sách danh mục phòng (để đổ vào SelectList)
        Task<IEnumerable<RoomCategory>> GetRoomCategoriesAsync();


        // --- Thao tác nghiệp vụ ---

        // Tạo phòng kèm xử lý upload nhiều ảnh
        Task<bool> CreateRoomAsync(Room room, List<IFormFile> imageFiles);

        // Cập nhật phòng và bổ sung ảnh mới vào album
        Task<bool> UpdateRoomAsync(Room room, List<IFormFile> imageFiles);

        // Xóa phòng và toàn bộ ảnh liên quan trong thư mục
        Task<bool> DeleteRoomAsync(int id);

        // Xóa một ảnh lẻ trong album (thường dùng cho AJAX trong trang Edit)
        Task<bool> DeleteRoomImageAsync(int imageId);


        // --- Kiểm tra ---
        Task<bool> RoomExistsAsync(int id);
    }
}