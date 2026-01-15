using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels; 
namespace VillaManagementWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly VillaDbContext _context;

    public HomeController(ILogger<HomeController> logger, VillaDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            Rooms = await _context.Rooms.Where(r => r.IsActive).ToListAsync(),
            Categories = await _context.RoomCategories.ToListAsync(),
            Tours = await _context.Tours.ToListAsync(),
            Events = await _context.Events.ToListAsync(),
            NewsItems = await _context.News.ToListAsync()
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
