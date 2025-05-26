namespace SimpleTokenAuthApp.Handlers
{
    public class SecureContentManager
    {
        readonly AuthManager _authManager;
        public SecureContentManager()
        {
            _authManager = new AuthManager();
        }
        public string ValidateToken(string token, string userName)
        {
            var user = _authManager.GetUserByUserName(userName);
            if (user.Token == token)
            {
                return "Access Denied!";
            }
            return "Access is allowed!";
        }
    }
}
