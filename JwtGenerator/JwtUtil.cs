using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtGenerator
{
	public static class JwtUtil
	{
		/// <summary>
		/// Creates a Json Web Token with the given claims, signed using the supplied secret
		/// </summary>
		/// <param name="secret">Secret used for signing. Must be secret in production</param>
		/// <param name="claims">Individual claims to give to the identity</param>
		/// <param name="secondsToExpiry">Duration in seconds token is valid for</param>
		/// <param name="issuer">Issuer, if used</param>
		/// <param name="audience">Audience, if used</param>
		/// <returns>Stringified JWT</returns>
		public static string CreateToken(string secret, Claim[] claims, int secondsToExpiry = 3600, string? issuer = null, string? audience = null)
		{
			DateTime notBefore = DateTime.Now;
			DateTime expires = notBefore.AddSeconds(secondsToExpiry);

			var credentials = new SigningCredentials(new SymmetricSecurityKey(ReadAndValidateSecret(secret)), SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(issuer, audience, claims, notBefore, expires, credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// Reads a JWT string, validates (signing and expiry, optionally issuer, audience), and returns a set of claims
		/// </summary>
		/// <param name="secret">Secret used for signing. Must be secret in production</param>
		/// <param name="token">JWT as a string/param>
		/// <param name="issuer">Issuer, if used</param>
		/// <param name="audience">Audience, if used</param>
		/// <returns>Claims principal [user]</returns>
		public static ClaimsPrincipal? ValidateAndReadToken(string secret, string token, string? issuer = null, string? audience = null)
		{
			// first off validate key
			var key = new SymmetricSecurityKey(ReadAndValidateSecret(secret));

			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = key,
				ValidateIssuer = issuer != null,
				ValidateAudience = audience != null,
				ValidateLifetime = true,
				ValidIssuer = issuer,
				ValidAudience = audience,
				ClockSkew = TimeSpan.FromSeconds(10)
			};

			try
			{
				// then try validation
				return new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var _);
			}
			catch
			{
				// no way of telling if signing, expiry, or just garbage
				return null;
			}
		}

		private static byte[] ReadAndValidateSecret(string secret)
		{
			byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
			if (keyBytes.Length < 16) throw new ArgumentException("secret must be at least 16 bytes long (UTF8)", nameof(secret));
			return keyBytes;
		}
	}
}
