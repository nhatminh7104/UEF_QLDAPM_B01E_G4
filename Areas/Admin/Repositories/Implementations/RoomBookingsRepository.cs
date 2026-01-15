using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;

namespace VillaManagementWeb.Admin.Repositories.Implementations
{
    public class RoomBookingsRepository : IRoomBookingsRepository
    {
        private readonly VillaDbContext _context;
        public RoomBookingsRepository(VillaDbContext context) => _context = context;

        public async Task<IEnumerable<RoomBooking>> GetAllWithRoomsAsync() =>
            await _context.RoomBookings.Include(b => b.Room).OrderByDescending(b => b.CreatedAt).ToListAsync();

        public async Task<RoomBooking?> GetByIdWithRoomAsync(int id) =>
            await _context.RoomBookings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == id);

        public async Task AddAsync(RoomBooking RoomBooking) => await _context.RoomBookings.AddAsync(RoomBooking);

        public void Update(RoomBooking RoomBooking) => _context.RoomBookings.Update(RoomBooking);

        public void Delete(RoomBooking RoomBooking) => _context.RoomBookings.Remove(RoomBooking);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeRoomBookingId = null)
        {
            var query = _context.RoomBookings.Where(b => b.RoomId == roomId && b.Status != "Cancelled");

            if (excludeRoomBookingId.HasValue)
                query = query.Where(b => b.Id != excludeRoomBookingId.Value);

            // Logic: Một phòng không khả dụng nếu (Ngày nhận mới < Ngày trả cũ) VÀ (Ngày trả mới > Ngày nhận cũ)
            return !await query.AnyAsync(b => checkIn < b.CheckOut && checkOut > b.CheckIn);
        }
    }
}