using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.CORE.Infrastructure.Shared
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string role)
        {
            // 1. Extraer los parámetros del JWT desde el appsettings.json
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey")
                ?? "ClaveSecretaSuperSeguraDeRespaldoParaElExamen123456!";
            var issuer = jwtSettings.GetValue<string>("Issuer") ?? "http://localhost:5072";
            var audience = jwtSettings.GetValue<string>("Audience") ?? "http://localhost:5072";
            var durationInMinutes = jwtSettings.GetValue<int>("DurationInMinutes");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 2. Definir los Claims (las propiedades que viajan encriptadas en el token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 3. Crear el Token con su tiempo de expiración
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes == 0 ? 60 : durationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}