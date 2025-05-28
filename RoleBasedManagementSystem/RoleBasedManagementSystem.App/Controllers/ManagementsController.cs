using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedManagementSystem.App.Contexts;
using RoleBasedManagementSystem.App.Dtos;
using RoleBasedManagementSystem.App.Models;

namespace RoleBasedManagementSystem.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementsController : ControllerBase
    {
        readonly RoleManager<Role> _roleManager;
        readonly AppDbContext _context;

        public ManagementsController(RoleManager<Role> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.Include(r => r.Permission);
            var dtos = new List<RoleGetDto>();
            dtos = await roles.Select(role => new RoleGetDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                PermissionId = role.Permission.Id,
                Permission = role.Permission.Description
            }).ToListAsync();
            return Ok(dtos);
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRoleAsync(string roleName, string permissionDescription)
        {
            var role = new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName
            };

            var permission = new Permission()
            {
                Id = Guid.NewGuid().ToString(),
                Description = permissionDescription,
                RoleId = role.Id
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return BadRequest("Create Role Failed");

            _context.Permissions.Add(permission);
            var count = await _context.SaveChangesAsync();
            if (count == 0) return BadRequest("Save Permission Failed");

            return Ok("Success");
        }
    }
}
