using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IRoomImagesRepository
    {
        Task<IEnumerable<RoomImage>> GetAllWithRoomAsync();
        Task<RoomImage?> GetByIdAsync(int id);
        Task AddAsync(RoomImage roomImage);
        void Update(RoomImage roomImage);
        void Delete(RoomImage roomImage);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}