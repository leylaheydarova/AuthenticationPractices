using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleBasedManagementSystem.App.Models;

namespace RoleBasedManagementSystem.App.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
