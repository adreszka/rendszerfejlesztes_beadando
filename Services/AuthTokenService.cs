using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rendszerfejlesztes_beadando.Services
{
    public class AuthTokenService
    {
        private readonly IConfiguration _configuration;
        public AuthTokenService(IConfiguration config)
        {
            _configuration = config;
        }

        public JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {


#pragma warning disable CS8604 // Possible null reference argument.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
#pragma warning restore CS8604 // Possible null reference argument.
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                authClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);

            return token;
        }
    }
}
