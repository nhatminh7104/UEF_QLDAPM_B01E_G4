using Microsoft.AspNetCore.Identity;

namespace VillaManagementWeb.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
