using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtDemo
{
    public class JwtCreator
    {
        private const string secretKey = "MySuperSecretKeyForThisDemoApp123456789"; //this is demo, in real this key must be maintained securely.
        public string CreateJwt()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, "HS256");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user123"),
                new Claim("role", "admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "JwtDemoApp",
                audience: "JwtDemoApp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4).AddMinutes(5),
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
