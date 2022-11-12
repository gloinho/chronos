using System.Security.Claims;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public abstract class BaseService
    {
        public readonly int? UsuarioId;
        public readonly string UsuarioPermissao;

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            UsuarioId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
            UsuarioPermissao = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
        }
    }
}
