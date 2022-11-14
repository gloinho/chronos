using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class Usuario_ProjetoService : BaseService, IUsuario_ProjetoService
    {
        private readonly IUsuario_ProjetoRepository _usuario_ProjetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;

        public Usuario_ProjetoService(
            IUsuario_ProjetoRepository usuario_ProjetoRepository,
            IUsuarioRepository usuarioRepository,
            IProjetoRepository projetoRepository,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor)
        {
            _usuario_ProjetoRepository = usuario_ProjetoRepository;
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
        }

        public async Task CadastrarAsync(Usuario_Projeto relacao)
        {
            await CheckSeIdsExistem(relacao);
            await CheckSeRelacaoExiste(relacao);
            await _usuario_ProjetoRepository.CadastrarAsync(relacao);
        }

        public async Task<Usuario_Projeto> CheckSeUsuarioFazParteDoProjeto(int projetoId)
        {
            await CheckSeProjetoExiste(projetoId);
            var relacao = await _usuario_ProjetoRepository.ObterPorUsuarioIdProjetoId(
                projetoId,
                UsuarioId
            );
            if (relacao == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    "Usuário não faz parte do projeto."
                );
            }
            return relacao;
        }

        public async Task CheckPermissao(int usuario_projetoId)
        {
            var relacao = await _usuario_ProjetoRepository.ObterPorIdAsync(usuario_projetoId);
            if (
                relacao.UsuarioId != UsuarioId
                && PermissaoUtil.PermissaoColaborador == UsuarioPermissao
            )
            {
                throw new BaseException(
                    StatusException.NaoAutorizado,
                    "Colaborador não pode deletar tarefas de outros colaboradores."
                );
            }
        }

        private async Task CheckSeRelacaoExiste(Usuario_Projeto relacao)
        {
            var find = await _usuario_ProjetoRepository.ObterPorUsuarioIdProjetoId(
                relacao.ProjetoId,
                relacao.UsuarioId
            );
            if (find != null)
            {
                throw new BaseException(
                    StatusException.Erro,
                    $"O usuario de id {relacao.UsuarioId} já faz parte do projeto {relacao.ProjetoId}"
                );
            }
        }

        private async Task CheckSeProjetoExiste(int id)
        {
            var projeto = await _projetoRepository.ObterPorIdAsync(id);
            if (projeto == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"Projeto de id {id} não foi encontrado."
                );
            }
        }

        private async Task CheckSeIdsExistem(Usuario_Projeto relacao)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(relacao.UsuarioId);
            if (usuario == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"O usuario de ID {relacao.UsuarioId} não foi encontrado."
                );
            }
            ;
        }
    }
}
