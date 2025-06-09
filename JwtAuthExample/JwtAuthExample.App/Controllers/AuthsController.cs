using JwtAuthExample.App.Models;
using JwtAuthExample.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthExample.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        readonly TokenService _tokenService;

        public AuthsController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        List<AppUser> _users = new List<AppUser>()
        {
            new AppUser
            {
                UserName = "test@example.com",
                Role = "Admin",
                Password = "Test123@"
            }
        };
        
        [HttpGet]
        public IActionResult Login(string userName, string password)
        {
            if(_users.Any(u => u.UserName != userName) || _users.Any(u => u.Password != password))
            {
                BadRequest("UserName Or Password is not correct!");
            }

            var token = _tokenService.GenerateToken(_users.FirstOrDefault(u => u.UserName == userName));
            return Ok(token);
        }
    }
}
