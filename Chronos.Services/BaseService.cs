using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chronos.Services
{
    public abstract class BaseService<T> where T : BaseEntity
    {
        public readonly int? UsuarioId;
        public readonly string UsuarioPermissao;
        public readonly string UsuarioCodigo;
        public readonly string UsuarioEmail;
        private readonly IBaseRepository<T> _repository;

        public BaseService(IHttpContextAccessor httpContextAccessor, IBaseRepository<T> repository)
        {
            UsuarioId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
            UsuarioPermissao = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
            UsuarioCodigo = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier);
            UsuarioEmail = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Email);
            _repository = repository;
        }

        public async Task<T> CheckSeIdExiste(int id)
        {
            var entity = await _repository.ObterPorIdAsync(id);
            if (entity == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, $"Id {id} não cadastrado.");
            }
            return entity;
        }
    }
}
