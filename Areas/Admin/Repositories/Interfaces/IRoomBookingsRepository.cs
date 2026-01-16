using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface IRoomBookingsRepository
    {
        Task<IEnumerable<RoomBooking>> GetAllWithRoomsAsync();
        Task<RoomBooking?> GetByIdWithRoomAsync(int id);
        Task AddAsync(RoomBooking RoomBooking);
        void Update(RoomBooking RoomBooking);
        void Delete(RoomBooking RoomBooking);
        Task<bool> SaveChangesAsync();
        // Kiểm tra xem phòng có bị trùng lịch (overlap) không
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeRoomBookingId = null);
    }
}