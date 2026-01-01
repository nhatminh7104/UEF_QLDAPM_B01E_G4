using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class TourRepository : GenericRepository<Tour>, ITourRepository
    {
        public TourRepository(VillaDbContext context) : base(context)
        {
        }
    }
}

