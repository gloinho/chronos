using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class UsuarioServiceTest
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsuarioServiceTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _appSettings = new AppSettings()
            {
                SecurityKey = "hWmZq4t6w9z$C&F)J@NcRfUjXn2r5u8x!A%D*G-KaPdSgVkYp3s6v9y$B?E(H+Mb"
            };
            _mapper = MapperConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var request = UsuarioToContractFaker.GetRequest();
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync((Usuario)null);
            _mockUsuarioRepository
                .Setup(mock => mock.CadastrarAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);
            _mockEmailService.Setup(
                mock => mock.Send(request.Email, It.IsAny<string>(), It.IsAny<string>())
            );

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );

            var result = await service.CadastrarAsync(request);
            Assert.IsNotNull(result);
            Assert.AreEqual(
                "Enviamos um token para seu email. Por favor, faça a confirmação.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCadastrarAsyncEmailJaCadastrado()
        {
            var request = UsuarioToContractFaker.GetRequest();
            var usuario = UsuarioFaker.GetUsuario();
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository
                .Setup(mock => mock.CadastrarAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);
            _mockEmailService.Setup(
                mock => mock.Send(request.Email, It.IsAny<string>(), It.IsAny<string>())
            );

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CadastrarAsync(request)
            );
            Assert.AreEqual("E-mail já cadastrado", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarAsyncAdministrador()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var request = UsuarioToContractFaker.GetRequest();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Administrador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository
                .Setup(mock => mock.AlterarAsync(usuario))
                .ReturnsAsync(It.IsAny<Usuario>());
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.AlterarAsync(usuario.Id, request);
            Assert.AreEqual("Alterado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarAsyncIdInexistente()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var request = UsuarioToContractFaker.GetRequest();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Administrador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync((Usuario)null);
            _mockUsuarioRepository
                .Setup(mock => mock.AlterarAsync(usuario))
                .ReturnsAsync(It.IsAny<Usuario>());
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarAsync(usuario.Id, request)
            );
            Assert.AreEqual($"Usuário com o id {usuario.Id} não cadastrado.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarAsyncColaboradorSemPermissao()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var outroUsuario = UsuarioFaker.GetUsuario();
            var request = UsuarioToContractFaker.GetRequest();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync(outroUsuario);
            _mockUsuarioRepository
                .Setup(mock => mock.AlterarAsync(usuario))
                .ReturnsAsync(It.IsAny<Usuario>());
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarAsync(outroUsuario.Id, request)
            );
            Assert.AreEqual($"Colaborador não pode acessar.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var usuario = UsuarioFaker.GetUsuario();
            _mockUsuarioRepository
                .Setup(mock => mock.DeletarAsync(usuario))
                .Returns(Task.CompletedTask);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync(usuario);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.DeletarAsync(usuario.Id);
            Assert.AreEqual("Deletado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestDeletarAsyncIdInexistente()
        {
            var usuario = UsuarioFaker.GetUsuario();
            _mockUsuarioRepository
                .Setup(mock => mock.DeletarAsync(usuario))
                .Returns(Task.CompletedTask);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync((Usuario)null);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.DeletarAsync(usuario.Id)
            );
            Assert.AreEqual($"Usuário com o id {usuario.Id} não cadastrado.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var response = UsuarioToContractFaker.GetResponse(usuario);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync(usuario);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.ObterPorIdAsync(usuario.Id);
            Assert.AreEqual(usuario.Id, result.Id);
        }

        [TestMethod]
        public async Task TestObterPorIdInexistente()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var response = UsuarioToContractFaker.GetResponse(usuario);
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(usuario.Id))
                .ReturnsAsync((Usuario)null);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.ObterPorIdAsync(usuario.Id)
            );
            Assert.AreEqual($"Usuário com o id {usuario.Id} não cadastrado.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var usuarios = UsuarioFaker.GetUsuarios();
            var responses = UsuarioToContractFaker
                .GetResponses(usuarios)
                .OrderBy(u => u.Nome)
                .ToList();
            _mockUsuarioRepository.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(usuarios);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.ObterTodosAsync();
            Assert.AreEqual(responses.Count, result.Count);
        }

        [TestMethod]
        public async Task TestAlterarSenha()
        {
            var novaSenhaRequest = NovaSenhaRequestFaker.GetRequest("OutraSenha123.");
            var usuario = UsuarioFaker.GetUsuarioCodigoEncriptado(novaSenhaRequest.Codigo);
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(
                usuario.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository.Setup(mock => mock.AlterarAsync(usuario)).ReturnsAsync(usuario);

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.AlterarSenha(novaSenhaRequest);
            Assert.AreEqual("Senha alterada com sucesso.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarSenhaCodigoInvalido()
        {
            var novaSenhaRequest = NovaSenhaRequestFaker.GetRequest("OutraSenha123.");
            var usuario = UsuarioFaker.GetUsuarioCodigoEncriptado("outrocodigo");
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(
                usuario.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository.Setup(mock => mock.AlterarAsync(usuario)).ReturnsAsync(usuario);

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarSenha(novaSenhaRequest)
            );
            Assert.AreEqual("Código incorreto.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarSenhaConfirmacaoDiferente()
        {
            var novaSenhaRequest = NovaSenhaRequestFaker.GetRequest("OutraSenha123");
            novaSenhaRequest.ConfirmacaoSenha = "SenhaDiferente123";
            var usuario = UsuarioFaker.GetUsuarioCodigoEncriptado(novaSenhaRequest.Codigo);
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(
                usuario.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository.Setup(mock => mock.AlterarAsync(usuario)).ReturnsAsync(usuario);

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarSenha(novaSenhaRequest)
            );
            Assert.AreEqual($"Senhas digitadas são diferentes.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarSenhaIgualASenhaAntiga()
        {
            var novaSenhaRequest = NovaSenhaRequestFaker.GetRequest("OutraSenha123");
            var usuario = UsuarioFaker.GetUsuarioCodigoEncriptado(novaSenhaRequest.Codigo);
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(
                novaSenhaRequest.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository.Setup(mock => mock.AlterarAsync(usuario)).ReturnsAsync(usuario);

            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarSenha(novaSenhaRequest)
            );
            Assert.AreEqual("Nova senha deve ser diferente da senha atual", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestEnviarCodigoResetSenha()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var request = new ResetSenhaRequest() { Email = usuario.Email };
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);
            _mockUsuarioRepository
                .Setup(mock => mock.AlterarAsync(usuario))
                .ReturnsAsync(It.IsAny<Usuario>());
            _mockEmailService
                .Setup(mock => mock.Send(request.Email, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await service.EnviarCodigoResetSenha(request);
            Assert.IsNotNull(result);
            Assert.AreEqual(
                "Enviamos o código de alteração de senha para seu e-mail. Este código será válido por apenas 2 horas.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestEnviarCodigoResetSenhaEmailInexistente()
        {
            var usuario = UsuarioFaker.GetUsuario();
            var request = new ResetSenhaRequest() { Email = "outroemail@email.com" };
            _mockUsuarioRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync((Usuario)null);
            _mockUsuarioRepository
                .Setup(mock => mock.AlterarAsync(usuario))
                .ReturnsAsync(It.IsAny<Usuario>());
            _mockEmailService
                .Setup(mock => mock.Send(request.Email, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var service = new UsuarioService(
                _mockUsuarioRepository.Object,
                _mockEmailService.Object,
                _appSettings,
                _mapper,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.EnviarCodigoResetSenha(request)
            );
            Assert.AreEqual(
                $"Usuário com o email {request.Email} não cadastrado.",
                result.Mensagens[0]
            );
        }
    }
}
