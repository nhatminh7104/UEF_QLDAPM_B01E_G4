using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IRoomsService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsWithStatusAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<bool> CreateRoomAsync(Room room, List<IFormFile> imageFiles);
        Task<bool> UpdateRoomAsync(Room room, List<IFormFile> imageFiles);
        Task<bool> DeleteRoomAsync(int id);
        Task<bool> RoomExistsAsync(int id);
    }
}