using VillaManagementWeb.Models;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface ITourBookingService
    {
        Task<IEnumerable<TourBooking>> GetAllTourBookingsAsync();
        Task<TourBooking?> GetTourBookingByIdAsync(int id);
        Task<IEnumerable<TourBooking>> GetTourBookingsWithToursAsync();
        Task<IEnumerable<TourBooking>> GetTourBookingsFilteredAsync(DateTime? tourDate, string? status);
        Task<TourBooking> CreateTourBookingAsync(TourBooking tourBooking);
        Task<TourBooking> UpdateTourBookingAsync(TourBooking tourBooking);
        Task<bool> DeleteTourBookingAsync(int id);
    }
}

