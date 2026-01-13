using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ToursController : Controller
    {
        private readonly IToursService _toursService;

        public ToursController(IToursService toursService)
        {
            _toursService = toursService;
        }

        public async Task<IActionResult> Index() => View(await _toursService.GetAllToursAsync());

        public async Task<IActionResult> Details(int id)
        {
            var tour = await _toursService.GetTourByIdAsync(id);
            return tour == null ? NotFound() : View(tour);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tour tour, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                await _toursService.CreateTourAsync(tour, imageFile);
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _toursService.GetTourByIdAsync(id);
            return tour == null ? NotFound() : View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour tour, IFormFile? imageFile)
        {
            if (id != tour.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _toursService.UpdateTourAsync(tour, imageFile);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(tour);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toursService.DeleteTourAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}