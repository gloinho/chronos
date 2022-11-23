using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;

namespace Chronos.Domain.Shared
{
    public class Verificador<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUsuario_ProjetoService _usuarioProjetoService;
        private readonly string UsuarioPermissao;
        private readonly int? UsuarioId;

        public Verificador(
            IBaseRepository<T> repository,
            IUsuario_ProjetoService usuarioprojetoService,
            string usuarioPermissao,
            int? usuarioId
        )
        {
            _repository = repository;
            _usuarioProjetoService = usuarioprojetoService;
            UsuarioId = usuarioId;
            UsuarioPermissao = usuarioPermissao;
        }

        public async Task<T> Id(int id)
        {
            var entity = await _repository.ObterPorIdAsync(id);
            if (entity == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, $"Id {id} não cadastrado.");
            }
            return entity;
        }

        public async Task Permissao(int usuarioId)
        {
            await _usuarioProjetoService.CheckSeUsuarioExiste(usuarioId);
            if (UsuarioPermissao == PermissaoUtil.PermissaoColaborador && UsuarioId != usuarioId)
            {
                throw new BaseException(StatusException.NaoAutorizado, "Acesso não permitido.");
            }
        }
    }
}
