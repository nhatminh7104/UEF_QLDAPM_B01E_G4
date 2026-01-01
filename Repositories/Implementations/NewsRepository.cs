using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;

namespace VillaManagementWeb.Repositories.Implementations
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(VillaDbContext context) : base(context)
        {
        }
    }
}

