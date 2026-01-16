using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaManagementWeb.Models;
using VillaManagementWeb.Admin.Services.Interfaces;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService) => _userService = userService;

        public async Task<IActionResult> Index() => View(await _userService.GetAllUsersWithRolesAsync());

        public async Task<IActionResult> Index(string keyword)
        {
            ViewBag.Keyword = keyword;
            var users = await _userService.SearchUsersAsync(keyword);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _userService.GetRolesAsync());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM model, string role)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, FullName = model.FullName };
                var result = await _userService.CreateUserAsync(user, model.Password, role);

                if (result.Succeeded) return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }
            ViewBag.Roles = new SelectList(await _userService.GetRolesAsync());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Succeeded) return RedirectToAction(nameof(Index));

            return BadRequest("Lỗi khi xóa người dùng.");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            // Sửa lỗi: GetAllRolesAsync -> GetRolesAsync
            var userRoles = await _userService.GetRolesAsync();
            ViewBag.Roles = new SelectList(userRoles);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string userId, string fullName, string newRole)
        {
            // Tạo đối tượng user giả định để truyền vào hàm UpdateUserAsync
            var user = new User { Id = userId, FullName = fullName };

            var result = await _userService.UpdateUserAsync(user, newRole);

            if (result.Succeeded) return RedirectToAction(nameof(Index));

            return BadRequest("Không thể cập nhật quyền.");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLock(string id)
        {
            await _userService.ToggleUserLockoutAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}