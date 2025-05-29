using JwtDemo;

JwtCreator creator = new JwtCreator();
var token = creator.CreateJwt();
Console.WriteLine("Get Token\n");
Console.WriteLine($"Token var created: {token}");
Console.WriteLine("\n\n");

Console.WriteLine("Decode Token\n");
Console.Write($"Enter token for decode: ");
var jwtToken = Console.ReadLine();
JwtDecoder decoder = new JwtDecoder();
decoder.DecodeJwt(jwtToken);