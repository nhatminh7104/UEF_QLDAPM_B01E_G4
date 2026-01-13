using Microsoft.AspNetCore.Identity;
using VillaManagementWeb.Models;
using VillaManagementWeb.ViewModels;

namespace VillaManagementWeb.Admin.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserWithRoleVM>> GetAllUsersWithRolesAsync();
        Task<IEnumerable<UserWithRoleVM>> SearchUsersAsync(string keyword);
        Task<User?> GetUserByIdAsync(string id);
        Task<IdentityResult> CreateUserAsync(User user, string password, string role);
        Task<IdentityResult> UpdateUserAsync(User user, string newRole);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<IEnumerable<string>> GetRolesAsync();
        Task<IdentityResult> ToggleUserLockoutAsync(string userId);
    }
}