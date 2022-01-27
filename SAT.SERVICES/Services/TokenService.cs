using Microsoft.IdentityModel.Tokens;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SAT.SERVICES.Services
{
    public class TokenService : ITokenService
    {
        private const double TEMPO_VIDA_MINUTOS = 10 * 60;

        public string GerarToken(string key, string issuer, Usuario usuario)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, usuario.CodUsuario),
                new Claim(ClaimTypes.Role, usuario.Perfil.CodPerfil.ToString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(TEMPO_VIDA_MINUTOS), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
