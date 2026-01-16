namespace VillaManagementWeb.ViewModels
{
    public class UserWithRoleVM
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; }
    }
}