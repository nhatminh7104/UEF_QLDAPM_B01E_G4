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
    public class BookingsController : Controller
    {
        private readonly VillaDbContext _context;

        public BookingsController(VillaDbContext context)
        {
            _context = context;
        }


        // GET: Bookings/Details/5
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


        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
