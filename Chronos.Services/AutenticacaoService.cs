using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Domain.Shared;

namespace Chronos.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, AppSettings appSettings)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
        }

        public async Task<MensagemResponse> Login(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterAsync(u => u.Email == request.Email);
            if (usuario == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    "Usuário não cadastrado, ou email incorreto."
                );
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                throw new BaseException(StatusException.Erro, "Senha incorreta.");
            }
            ;
            if (!usuario.Confirmado)
            {
                throw new BaseException(StatusException.NaoProcessado, "E-mail não confirmado.");
            }
            var token = Token.GenerateToken(usuario, _appSettings.SecurityKey);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { token },
                Detalhe = "Token para autenticação na plataforma."
            };
        }

        public async Task<MensagemResponse> Confirmar(string token)
        {
            var usuario = await _usuarioRepository.ObterAsync(u => u.ConfirmacaoToken == token);
            if (usuario == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, "Token inválido.");
            }
            await _usuarioRepository.Confirmar(usuario);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Usuário Confirmado com sucesso." }
            };
        }
    }
}
