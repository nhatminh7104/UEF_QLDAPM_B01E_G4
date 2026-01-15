using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class RoomBookingsService : IRoomBookingsService
    {
        private readonly IRoomBookingsRepository _RoomBookingRepo;
        private readonly IRoomsRepository _roomRepo;

        public RoomBookingsService(IRoomBookingsRepository RoomBookingRepo, IRoomsRepository roomRepo)
        {
            _RoomBookingRepo = RoomBookingRepo;
            _roomRepo = roomRepo;
        }

        public async Task<IEnumerable<RoomBooking>> GetRoomBookingsWithRoomsAsync() => await _RoomBookingRepo.GetAllWithRoomsAsync();

        public async Task<RoomBooking?> GetRoomBookingByIdAsync(int id) => await _RoomBookingRepo.GetByIdWithRoomAsync(id);

        public async Task CreateRoomBookingAsync(RoomBooking RoomBooking)
        {
            if (RoomBooking.CheckIn >= RoomBooking.CheckOut)
                throw new ArgumentException("Ngày nhận phòng phải trước ngày trả phòng.");

            bool isAvailable = await _RoomBookingRepo.IsRoomAvailableAsync(RoomBooking.RoomId, RoomBooking.CheckIn, RoomBooking.CheckOut);
            if (!isAvailable)
                throw new InvalidOperationException("Phòng đã có khách đặt trong khoảng thời gian này.");

            RoomBooking.CreatedAt = DateTime.Now;
            await _RoomBookingRepo.AddAsync(RoomBooking);
            await _RoomBookingRepo.SaveChangesAsync();
        }

        public async Task UpdateRoomBookingAsync(RoomBooking RoomBooking)
        {
            bool isAvailable = await _RoomBookingRepo.IsRoomAvailableAsync(RoomBooking.RoomId, RoomBooking.CheckIn, RoomBooking.CheckOut, RoomBooking.Id);
            if (!isAvailable)
                throw new InvalidOperationException("Không thể cập nhật: Phòng bị trùng lịch với đơn đặt khác.");

            _RoomBookingRepo.Update(RoomBooking);
            await _RoomBookingRepo.SaveChangesAsync();
        }

        public async Task DeleteRoomBookingAsync(int id)
        {
            var RoomBooking = await _RoomBookingRepo.GetByIdWithRoomAsync(id);
            if (RoomBooking != null)
            {
                _RoomBookingRepo.Delete(RoomBooking);
                await _RoomBookingRepo.SaveChangesAsync();
            }
        }
    }
}