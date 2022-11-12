using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Settings;
using Chronos.Domain.Shared;

namespace Chronos.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, AppSettings appSettings)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
        }

        public async Task<string> Login(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetPorEmail(email);
            if (usuario == null)
            {
                throw new Exception("Usuário não cadastrado, ou email incorreto.");
            }
            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
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
            var usuario = _usuarioRepository.GetPorToken(token);
            if (usuario == null)
            {
                throw new Exception("Algum erro ocorreu.");
            }
            await _usuarioRepository.Confirmar(token);
        }
    }
}
