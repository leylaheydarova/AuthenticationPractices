using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtDemo
{
    public class JwtDecoder
    {
        private const string secretKey = "MySuperSecretKeyForThisDemoApp123456789"; //this is demo, in real this key must be maintained securely.

        public void DecodeJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "JwtDemoApp",
                ValidAudience = "JwtDemoApp",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => (object)c.Value);

            foreach(var claim in claims)
            {
                Console.WriteLine($"{claim.Key} : {claim.Value}");
            }
        }
    }
}
