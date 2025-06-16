using SecureData.App.Models;

SecureStorage storage = new SecureStorage();
var text = "Hello I am new text";
//var encrypted = storage.StoreData(text);
//var decrypted = storage.RetrieveData(encrypted);
//Console.WriteLine($"I am encrypted text: {encrypted}");
//Console.WriteLine($"I am decrypted text: {decrypted}");

var user = new AppUser() { Role = "User", Username = "user@example.com" };
var admin = new AppUser() { Role = "Admin", Username = "admin@example.com" };

var encryptedAdminUsername = storage.StoreData(admin.Username);
var encryptedAdminRole = storage.StoreData(admin.Role);
var encryptedUserUsername = storage.StoreData(user.Username);
var encryptedUserRole = storage.StoreData(user.Role);

Console.WriteLine($"I am a encrypted user username: {encryptedUserUsername}");
Console.WriteLine($"I am a encrypted user role: {encryptedUserRole}\n");
Console.WriteLine($"I am a encrypted admin username: {encryptedAdminUsername}");
Console.WriteLine($"I am a encrypted admin role: {encryptedAdminRole}\n");

var decryptedAdminUsername = storage.RetrieveData(encryptedAdminUsername);
var adminAccess = storage.RetrieveAccess(encryptedAdminRole);
var decrypteduserUsername = storage.RetrieveData(encryptedUserUsername);
var userAccess = storage.RetrieveAccess(encryptedUserRole);

Console.WriteLine($"{decryptedAdminUsername} Acsess: {adminAccess}\n");
Console.WriteLine($"{decrypteduserUsername} Access: {userAccess}\n");