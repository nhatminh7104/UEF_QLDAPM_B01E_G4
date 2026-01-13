using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;

namespace VillaManagementWeb.Controllers
{
    public class RoomBookingsController : Controller
    {
        private readonly VillaDbContext _context;

        public RoomBookingsController(VillaDbContext context)
        {
            _context = context;
        }


        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // tìm thông tin PHÒNG
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            // Gửi đối tượng room sang View
            return View(room);
        }


        private bool RoomBookingExists(int id)
        {
            return _context.RoomBookings.Any(e => e.Id == id);
        }
    }
}
