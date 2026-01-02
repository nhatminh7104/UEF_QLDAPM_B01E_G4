using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomImagesController : Controller
    {
        private readonly VillaDbContext _context;

        public RoomImagesController(VillaDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RoomImages
        public async Task<IActionResult> Index()
        {
            var villaDbContext = _context.RoomImage.Include(r => r.Room);
            return View(await villaDbContext.ToListAsync());
        }

        // GET: Admin/RoomImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomImage = await _context.RoomImage
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomImage == null)
            {
                return NotFound();
            }

            return View(roomImage);
        }

        // GET: Admin/RoomImages/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Admin/RoomImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,RoomId")] RoomImage roomImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomImage = await _context.RoomImage.FindAsync(id);
            if (roomImage == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomImage.RoomId);
            return View(roomImage);
        }

        // POST: Admin/RoomImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,RoomId")] RoomImage roomImage)
        {
            if (id != roomImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomImageExists(roomImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomImage.RoomId);
            return View(roomImage);
        }

        // GET: Admin/RoomImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomImage = await _context.RoomImage
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomImage == null)
            {
                return NotFound();
            }

            return View(roomImage);
        }

        // POST: Admin/RoomImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomImage = await _context.RoomImage.FindAsync(id);
            if (roomImage != null)
            {
                _context.RoomImage.Remove(roomImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomImageExists(int id)
        {
            return _context.RoomImage.Any(e => e.Id == id);
        }
    }
}
