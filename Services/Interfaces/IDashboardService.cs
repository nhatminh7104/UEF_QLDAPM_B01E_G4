using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}