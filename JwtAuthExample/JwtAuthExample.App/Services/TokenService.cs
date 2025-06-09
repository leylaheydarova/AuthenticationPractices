using JwtAuthExample.App.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthExample.App.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private static Dictionary<string, Token> _refreshTokens = new();
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AppUser user)
        {
            var secretKey = _configuration["Jwt:secretKey"];
            var issuer = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:audience"];
            var expiresIn = int.Parse(_configuration["Jwt:expiresIn"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4).AddDays(expiresIn),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string RefreshAccessToken(string refreshToken)
        {
            if (!_refreshTokens.TryGetValue(refreshToken, out var savedToken))
                return null;

            if (savedToken.ExpiryDate < DateTime.UtcNow.AddHours(4))
            {
                _refreshTokens.Remove(refreshToken);
                return null;
            }

            // Istifadəçini təkrar tapmaq üçün sadə demo
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(savedToken.RefreshToken);
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (username == null || role == null) return null;

            return GenerateToken(new AppUser
            {
                UserName = username,
                Role = role
            });
        }

    }
}
