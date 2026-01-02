using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _dashboardService.GetDashboardDataAsync();
            return View(viewModel);
        }
    }
}