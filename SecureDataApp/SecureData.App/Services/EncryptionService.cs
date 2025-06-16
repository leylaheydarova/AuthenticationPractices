using System.Security.Cryptography;

namespace SecureData.App.Services
{
    public class EncryptionService
    {
        readonly byte[] _key;
        readonly byte[] _iv;

        public EncryptionService()
        {
            _key = new byte[32];
            _iv = new byte[16];
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using(var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
                writer.Flush();
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText) 
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            var buffer = Convert.FromBase64String(cipherText);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }
    }
}
