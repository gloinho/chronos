using Chronos.Domain.Contracts.Request;
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

        public async Task<string> Login(LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetPorEmail(request.Email);
            if (usuario == null)
            {
                throw new Exception("Usuário não cadastrado, ou email incorreto.");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                throw new Exception("Senha incorreta.");
            }
            ;
            if (!usuario.Confirmado)
            {
                throw new Exception("E-mail não confirmado.");
            }
            return Token.GenerateToken(usuario, _appSettings.SecurityKey);
        }

        public async Task Confirmar(string token)
        {
            var usuario = await _usuarioRepository.GetPorToken(token);
            if (usuario == null)
            {
                throw new Exception("Algum erro ocorreu.");
            }
            await _usuarioRepository.Confirmar(usuario);
        }
    }
}
