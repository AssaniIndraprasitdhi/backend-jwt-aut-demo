using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using backend_jwt_auth.Models;

namespace backend_jwt_auth.Services
{
    public class JwtService : IJwtService {
        private readonly string _jwtKey;

        public JwtService(IConfiguration config) {
            _jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET")
                    ?? throw new InvalidOperationException("JWT_SECRET is not set in the environment.");
        }

        public string GenerateToken(User user) {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
