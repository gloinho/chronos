using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chronos.Services
{
    public abstract class BaseService
    {
        public readonly int? UsuarioId;
        public readonly string UsuarioPermissao;
        public readonly string UsuarioCodigo;
        public readonly string UsuarioEmail;

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            UsuarioId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
            UsuarioPermissao = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
            UsuarioCodigo = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier);
            UsuarioEmail = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Email);
        }
    }
}
