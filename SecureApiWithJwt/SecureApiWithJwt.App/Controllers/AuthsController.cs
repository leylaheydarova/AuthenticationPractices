using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.App.Services;

namespace SecureApiWithJwt.App.Controllers
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

        [HttpPost]
        public IActionResult LoginUser(string username, string password)
        {
            if (username != "test@example.com" || password != "Test123@")
            {
                return Unauthorized();
            }
            var token = _tokenService.GenerateToken(username);
            return Ok(token);
        }
    }
}
