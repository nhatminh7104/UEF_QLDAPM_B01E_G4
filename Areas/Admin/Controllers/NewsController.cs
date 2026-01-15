using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index() => View(await _newsService.GetAllNewsAsync());

        public async Task<IActionResult> Details(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            return news == null ? NotFound() : View(news);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _newsService.CreateNewsAsync(news, imageFile);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(news);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            return news == null ? NotFound() : View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news, IFormFile? imageFile)
        {
            if (id != news.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _newsService.UpdateNewsAsync(news, imageFile);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}