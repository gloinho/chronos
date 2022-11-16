using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Chronos.Domain.Shared
{
    public static class Token
    {
        public static string GenerateToken(Usuario usuario, string securityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.Permissao.ToString()),
                    }
                ),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateTokenRequest(Usuario usuario, string securityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.Email, usuario.Email)}
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateTokenRequest(Usuario usuario, string securityKey, string codigo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.Email, usuario.Email), new Claim(ClaimTypes.NameIdentifier, codigo) }
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )//,
               // Expires = DateTime.Now.AddHours(2),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static string GenerateCodigo()
        {
            var random = new Random();
            var codigo = random.Next(111111, 999999);

            return codigo.ToString();
        }
    }
}
