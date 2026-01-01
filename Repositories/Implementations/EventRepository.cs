using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(VillaDbContext context) : base(context)
        {
        }
    }
}

