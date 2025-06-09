using JwtAuthExample.App.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthExample.App.Contexts
{
    public class AppDbContext:DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
