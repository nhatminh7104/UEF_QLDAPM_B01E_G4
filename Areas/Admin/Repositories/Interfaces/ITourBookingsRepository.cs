using VillaManagementWeb.Models;

namespace VillaManagementWeb.Admin.Repositories.Interfaces
{
    public interface ITourBookingsRepository
    {
        Task<IEnumerable<TourBooking>> GetAllWithToursAsync();
        Task<TourBooking?> GetByIdWithTourAsync(int id);
        Task AddAsync(TourBooking tourBooking);
        void Update(TourBooking tourBooking);
        void Delete(TourBooking tourBooking);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}