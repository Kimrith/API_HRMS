using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HRMS.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.API.Services
{
    public interface ITokenService 
    { 
        string CreateToken(User user); 
    }

    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            var keyString = config["TokenKey"] ?? throw new InvalidOperationException("TokenKey is not configured.");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, user.Username),
                // Add this line to include the role in the JWT payload
                new Claim(ClaimTypes.Role, user.Role.ToString()) 
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), 
                SigningCredentials = creds
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}