using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Repositories.Interfaces;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class TourBookingsService : ITourBookingsService
    {
        private readonly ITourBookingsRepository _bookingRepo;
        private readonly IToursRepository _tourRepo;

        public TourBookingsService(ITourBookingsRepository bookingRepo, IToursRepository tourRepo)
        {
            _bookingRepo = bookingRepo;
            _tourRepo = tourRepo;
        }

        public async Task<IEnumerable<TourBooking>> GetAllBookingsAsync() => await _bookingRepo.GetAllWithToursAsync();

        public async Task<TourBooking?> GetBookingByIdAsync(int id) => await _bookingRepo.GetByIdWithTourAsync(id);

        public async Task CreateBookingAsync(TourBooking tourBooking)
        {
            var tour = await _tourRepo.GetByIdAsync(tourBooking.TourId);
            if (tour == null) throw new ArgumentException("Tour không tồn tại.");

            // Bạn có thể tính TotalPrice tự động ở đây nếu Model Tour có trường Price
            // tourBooking.TotalPrice = tour.Price * tourBooking.NumberOfPeople;

            await _bookingRepo.AddAsync(tourBooking);
            await _bookingRepo.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(TourBooking tourBooking)
        {
            if (!await _bookingRepo.ExistsAsync(tourBooking.Id))
                throw new ArgumentException("Đơn đặt tour không tồn tại.");

            _bookingRepo.Update(tourBooking);
            await _bookingRepo.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdWithTourAsync(id);
            if (booking != null)
            {
                _bookingRepo.Delete(booking);
                await _bookingRepo.SaveChangesAsync();
            }
        }
    }
}