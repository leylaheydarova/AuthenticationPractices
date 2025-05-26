using System.Security.Cryptography;

namespace SimpleTokenAuthApp.Handlers
{
    public class TokenManager
    {
        public string GenerateToken(int size = 32)
        {
            byte[] tokenData = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenData);
            }
            return Convert.ToBase64String(tokenData);
        }
    }
}
