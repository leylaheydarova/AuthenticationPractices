using JwtAuthDemo.App.Models;
using JwtAuthDemo.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly List<AppUser> _users = new()
        {
            new AppUser { UserName = "admin", Password = "1234" },
            new AppUser { UserName = "lilo", Password = "4567" }
        };

        public UsersController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AppUser loginUser)
        {

            var user = _users.FirstOrDefault(u =>
             u.UserName == loginUser.UserName && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user.UserName);
            return Ok(new { token });
        }

        [HttpGet("secure-data")]
        [Authorize]
        public IActionResult GetData()
        {
            return Ok("I am secure data");
        }

    }
}
