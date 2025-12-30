using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VillaManagementWeb.Data;

namespace VillaManagementWeb.ViewComponents;
public class NewsEventsViewComponent : ViewComponent
{
    private readonly VillaDbContext _context;
    public NewsEventsViewComponent(VillaDbContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Lấy 3 tin tức mới nhất để hiển thị lên Slider
        var news = await _context.News.OrderByDescending(n => n.CreatedAt).Take(3).ToListAsync();
        return View(news);
    }
}