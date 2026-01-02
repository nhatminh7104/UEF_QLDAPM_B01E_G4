using VillaManagementWeb.Models;

namespace VillaManagementWeb.Repositories.Interfaces
{
    public interface ITourBookingRepository : IGenericRepository<TourBooking>
    {
        Task<IEnumerable<TourBooking>> GetTourBookingsWithToursAsync();
    }
}

