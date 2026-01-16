using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IRoomsRepository
    {
        // --- Truy vấn Room ---
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);

        // Lấy Room kèm theo bảng RoomImages (Album ảnh)
        Task<Room?> GetRoomWithImagesAsync(int id);

        // --- Truy vấn liên quan ---
        // Lấy danh sách Category để dùng cho Dropdown trong Service/Controller
        Task<IEnumerable<RoomCategory>> GetRoomCategoriesAsync();

        // Tìm một ảnh cụ thể trong album để xóa
        Task<RoomImage?> GetRoomImageByIdAsync(int imageId);

        // --- Thao tác dữ liệu ---
        Task AddAsync(Room room);
        void Update(Room room);
        void Delete(Room room);

        // Xóa một ảnh lẻ trong bảng RoomImages
        void DeleteImage(RoomImage image);

        // --- Hệ thống ---
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}