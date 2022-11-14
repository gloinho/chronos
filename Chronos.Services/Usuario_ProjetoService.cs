using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;

namespace Chronos.Services
{
    public class Usuario_ProjetoService : IUsuario_ProjetoService
    {
        private readonly IUsuario_ProjetoRepository _usuario_ProjetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public Usuario_ProjetoService(
            IUsuario_ProjetoRepository usuario_ProjetoRepository,
            IUsuarioRepository usuarioRepository
        )
        {
            _usuario_ProjetoRepository = usuario_ProjetoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task CadastrarAsync(Usuario_Projeto relacao)
        {
            await CheckSeIdsExistem(relacao);
            await CheckSeRelacaoExiste(relacao);
            await _usuario_ProjetoRepository.CadastrarAsync(relacao);
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
