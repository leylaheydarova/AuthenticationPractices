using Microsoft.AspNetCore.Identity;

namespace RoleBasedManagementSystem.App.Models
{
    public class Role : IdentityRole
    {
        public Permission Permission { get; set; }
    }
}
