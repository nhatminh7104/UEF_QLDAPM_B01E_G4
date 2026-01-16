using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;
using VillaManagementWeb.Admin.Services.Interfaces;

namespace VillaManagementWeb.Admin.Services.Implementations
{
    public class UserService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IEnumerable<UserWithRoleVM>> SearchUsersAsync(string keyword)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u => u.Email.Contains(keyword) || u.FullName.Contains(keyword));
            }

            var users = await query.ToListAsync();
            return await MapUsersToVM(users);
        }

        public async Task<IEnumerable<UserWithRoleVM>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserWithRoleVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserWithRoleVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email ?? "",
                    Role = roles.FirstOrDefault() ?? "No Role",
                    IsLockedOut = await _userManager.IsLockedOutAsync(user)
                });
            }
            return userList;
        }

        public async Task<User?> GetUserByIdAsync(string id) => await _userManager.FindByIdAsync(id);

        public async Task<IEnumerable<string>> GetRolesAsync() =>
            await _roleManager.Roles.Select(r => r.Name!).ToListAsync();

        public async Task<IdentityResult> UpdateUserAsync(User user, string newRole)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            // Cập nhật thông tin FullName nếu cần
            existingUser.FullName = user.FullName;
            await _userManager.UpdateAsync(existingUser);

            // Cập nhật Role
            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            return await _userManager.AddToRoleAsync(existingUser, newRole);
        }

        public async Task<IdentityResult> ToggleUserLockoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed();

            var isLocked = await _userManager.IsLockedOutAsync(user);
            return await _userManager.SetLockoutEndDateAsync(user, isLocked ? null : DateTimeOffset.MaxValue);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return await _userManager.AddToRoleAsync(user, role);
            }
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User không tồn tại" });

            return await _userManager.DeleteAsync(user);
        }

        // Hàm phụ trợ để tái sử dụng logic map dữ liệu
        private async Task<IEnumerable<UserWithRoleVM>> MapUsersToVM(List<User> users)
        {
            var userList = new List<UserWithRoleVM>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserWithRoleVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email ?? "",
                    Role = roles.FirstOrDefault() ?? "No Role",
                    IsLockedOut = await _userManager.IsLockedOutAsync(user)
                });
            }
            return userList;
        }
    }
}