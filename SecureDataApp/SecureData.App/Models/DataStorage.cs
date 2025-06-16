using SecureData.App.Services;

namespace SecureData.App.Models
{
    public class DataStorage
    {
        public string Data { get; set; }
    }

    public class SecureStorage
    {
        readonly EncryptionService _service;

        public SecureStorage()
        {
            _service = new EncryptionService();
        }

        public string StoreData(string text)
        {
            var data = _service.Encrypt(text);
            new DataStorage() { Data = data };
            return data;
        }

        public string RetrieveAccess(string ecryptedData)
        {
            var data = _service.Decrypt(ecryptedData);
            if (data.ToLower() != "admin") return "You are not allowed!";
            return "You have an access!";
        }

        public string RetrieveData(string ecryptedData)
        {
            return _service.Decrypt(ecryptedData);
        }
    }
}
