using SimpleTokenAuthApp.Handlers;

//Register
AuthManager _authManager = new AuthManager();
Console.WriteLine("Welcome! Register, please");
Console.Write("Username:");
var username = Console.ReadLine();
Console.Write("Password:");
var password = Console.ReadLine();

var result = _authManager.Register(username, password);
Console.WriteLine(result);

//Login
Console.WriteLine("Welcome! Login, please");
Console.Write("Username:");
username = Console.ReadLine();
Console.Write("Password:");
password = Console.ReadLine();

var token = _authManager.Login(username, password);
Console.WriteLine($"Login was successfully! Your token is: {token}");

//Verify Token
SecureContentManager _secureContentManager = new SecureContentManager();
var access = _secureContentManager.ValidateToken(token, username);
Console.WriteLine(access);

