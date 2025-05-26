using SimpleTokenAuthApp.Models;

namespace SimpleTokenAuthApp.Handlers
{
    public class AuthManager
    {
        readonly TokenManager _tokenManager;
        readonly List<User> _users;
        public AuthManager()
        {
            _tokenManager = new TokenManager();
            _users = new List<User>();
        }
        public string Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
            {
                return "User already exists!";
            }

            var user = new User
            {
                Username = username,
                Password = password
            };
            _users.Add(user);
            return "User registered successfully!";
        }

        public string Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return "Username or password is not correct!";
            }
            var token = _tokenManager.GenerateToken();
            user.Token = token;
            return token;
        }

        public User GetUserByUserName(string usernName)
        {
            var user = _users.FirstOrDefault(u => u.Username == usernName);
            return user;
        }
    }
}
