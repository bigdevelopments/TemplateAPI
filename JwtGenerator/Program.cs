using JwtGenerator;
using System.Security.Claims;

// secret for signing the token
string secret = "0123456789ABCDEF";

// the claims to embed in the token
var claims = new [] {
	new Claim("name", "Mr. Testcase"),
	new Claim("email", "colin@bucket.org"),
	new Claim("style", "as cool as"),
	new Claim("roles", "useradmin,dev,readonly")
};

// create the token
string token = JwtUtil.CreateToken(secret, claims);

// display it
Console.WriteLine($"Bearer {token}");
Console.WriteLine();

// validate it and read it back to a ClaimsPrincipal
var claimsPrincipal = JwtUtil.ValidateAndReadToken(secret, token);

// write the claims back to the screen
Console.WriteLine("Unpacking token...");
Console.WriteLine(string.Join("> ", claimsPrincipal!.Claims.Select(x=>$"{x.Type}:{x.Value}\r\n").ToArray()));
Console.WriteLine("Done - Enter to exit");
Console.ReadLine();


