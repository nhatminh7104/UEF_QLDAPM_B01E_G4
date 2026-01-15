using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IRoomsRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task AddAsync(Room room);
        void Update(Room room);
        void Delete(Room room);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}