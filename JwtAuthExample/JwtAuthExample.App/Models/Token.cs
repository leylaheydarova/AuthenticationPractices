namespace JwtAuthExample.App.Models
{
    public class Token
    {
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
