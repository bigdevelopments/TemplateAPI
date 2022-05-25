using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TemplateAPI.Extension
{
	public static class AuthenticationExtensions
	{
		/// <summary>
		/// Adds JWT Bearer authentication token validation
		/// </summary>
		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
		{
			// this is the secret used to sign JWTs. It must be kept secret!
			string tokenKey = config["TokenKey"];
	
			// validate it, we need to fail fast if this is missing or invalid
			// would like a better exception here but not sure which....
			if (string.IsNullOrWhiteSpace(tokenKey)) throw new Exception("TokenKey configuration setting missing");
			byte[] keyBytes = Encoding.UTF8.GetBytes(tokenKey);
			if (keyBytes.Length < 16) throw new Exception("TokenKey configuration setting myst be at least 16 bytes long (UTF8)");

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			return services;
		}
	}
}