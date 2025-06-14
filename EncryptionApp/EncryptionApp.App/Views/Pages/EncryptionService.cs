using System.Security.Cryptography;
using System.Text;

namespace EncryptionApp.App.Views.Pages
{
    public class EncryptionService
    {
        readonly byte[] _key;
        readonly byte[] _iv;

        public EncryptionService(IConfiguration configuration)
        {
            _key = Encoding.UTF8.GetBytes(configuration["AesEncryption:Key"]);
            _iv = Encoding.UTF8.GetBytes(configuration["AesEncryption:IV"]);
        }

        string EncryptData(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor(_key, _iv);
            using var memoryStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(plainText);
                writer.Flush();
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        string DecryptData(string cipherText)
        {
            var buffer = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var decryptor = aes.CreateDecryptor(_key, _iv);
            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }

        public void EncryptFile(string inputFilePath, string outputFilePath)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
            using var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);

            using var encryptor = aes.CreateEncryptor();
            using var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write);
            inputFileStream.CopyTo(cryptoStream);
        }

        public void DecryptFile(string inputFilePath, string outputFilePath)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
            using var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);

            using var decryptor = aes.CreateDecryptor();
            using var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read);

            cryptoStream.CopyTo(outputFileStream);
        }
    }
}
