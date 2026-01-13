using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class EventsRepository : GenericRepository<Event>, IEventsRepository
    {
        public EventsRepository(VillaDbContext context) : base(context)
        {
        }
    }
}

