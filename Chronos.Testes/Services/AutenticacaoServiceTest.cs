using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using System.Linq.Expressions;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class AutenticacaoServiceTest
    {
        private readonly Mock<IUsuarioRepository> _repository;
        private readonly Mock<ILogService> _mockLogService;
        private readonly Fixture _fixture;
        private readonly AppSettings _appSettings;

        public AutenticacaoServiceTest()
        {
            _fixture = FixtureConfig.Get();
            _repository = new Mock<IUsuarioRepository>();
            _appSettings = new AppSettings()
            {
                SecurityKey = "hWmZq4t6w9z$C&F)J@NcRfUjXn2r5u8x!A%D*G-KaPdSgVkYp3s6v9y$B?E(H+Mb"
            };
            _mockLogService = new Mock<ILogService>();
        }

        [TestMethod]
        public async Task TestLoginValido()
        {
            var usuario = UsuarioFaker.GetUsuarioSenhaEncriptada("Senha123...");
            var request = new LoginRequest() { Email = usuario.Email, Senha = "Senha123...", };
            usuario.Confirmado = true;

            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);

            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await service.Login(request);
            Assert.AreEqual("Token para autenticação na plataforma.", result.Detalhe);
        }

        [TestMethod]
        public async Task TestLLoginUsuarioNaoCadastradoOuEmailInvalido()
        {
            var usuario = UsuarioFaker.GetUsuarioSenhaEncriptada("Senha123...");
            var request = new LoginRequest() { Email = usuario.Email, Senha = "Senha123...", };
            usuario.Confirmado = true;
            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync((Usuario)null);

            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.Login(request)
            );
            Assert.AreEqual("Usuário não cadastrado, ou email incorreto.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestLoginSenhaInvalida()
        {
            var usuario = UsuarioFaker.GetUsuarioSenhaEncriptada("Senha123...");
            var request = new LoginRequest() { Email = usuario.Email, Senha = "outrasenha", };
            usuario.Confirmado = true;
            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);

            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.Login(request)
            );
            Assert.AreEqual("Senha incorreta.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestLoginSemEmailConfirmado()
        {
            var usuario = UsuarioFaker.GetUsuarioSenhaEncriptada("Senha123...");
            var request = new LoginRequest() { Email = usuario.Email, Senha = "Senha123...", };
            usuario.Confirmado = false;
            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.Login(request)
            );
            Assert.AreEqual("E-mail não confirmado.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestConfirmarValido()
        {
            var usuario = UsuarioFaker.GetUsuario();
            usuario.Confirmado = false;
            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await service.Confirmar(usuario.ConfirmacaoToken);
            Assert.AreEqual("Usuário Confirmado com sucesso.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestConfirmarTokenInvalido()
        {
            var usuario = UsuarioFaker.GetUsuario();
            usuario.Confirmado = false;
            _repository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync((Usuario)null);
            var service = new AutenticacaoService(
                _repository.Object,
                _appSettings,
                _mockLogService.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.Confirmar(usuario.ConfirmacaoToken)
            );
            Assert.AreEqual("Token inválido.", result.Mensagens[0]);
        }
    }
}
