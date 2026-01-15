using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface ITourBookingsService
    {
        Task<IEnumerable<TourBooking>> GetAllBookingsAsync();
        Task<TourBooking?> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(TourBooking tourBooking);
        Task UpdateBookingAsync(TourBooking tourBooking);
        Task DeleteBookingAsync(int id);
    }
}