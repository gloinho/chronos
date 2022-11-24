using Chronos.Api.Controllers;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Chronos.Testes.Controllers
{
    [TestClass]
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Fixture _fixture;

        public UsuarioControllerTest()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestAlterarSenha()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<NovaSenhaRequest>();
            _mockUsuarioService.Setup(mock => mock.AlterarSenha(request)).ReturnsAsync(message);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.AlterarSenha(request);

            Assert.IsNotNull(result);

            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestEnviarCodigoResetSenha()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<ResetSenhaRequest>();
            _mockUsuarioService.Setup(mock => mock.EnviarCodigoResetSenha(request)).ReturnsAsync(message);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.EnviarCodigoResetSenha(request);

            Assert.IsNotNull(result);

            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<UsuarioRequest>();
            _mockUsuarioService.Setup(mock => mock.CadastrarAsync(request)).ReturnsAsync(message);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.CadastrarAsync(request);

            Assert.IsNotNull(result);

            var createdResult = result as CreatedResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<UsuarioRequest>();
            _mockUsuarioService.Setup(mock => mock.AlterarAsync(It.IsAny<int>(), request)).ReturnsAsync(message);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.AlterarAsync(It.IsAny<int>(), request);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<UsuarioRequest>();
            _mockUsuarioService
                .Setup(mock => mock.DeletarAsync(It.IsAny<int>()))
                .ReturnsAsync(message);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.DeletarAsync(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var response = _fixture.Create<List<UsuarioResponse>>();
            _mockUsuarioService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(response);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.ObterTodosAsync();

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<UsuarioResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var response = _fixture.Create<UsuarioResponse>();
            _mockUsuarioService
                .Setup(mock => mock.ObterPorIdAsync(response.Id))
                .ReturnsAsync(response);

            var controller = new UsuarioController(_mockUsuarioService.Object);

            var result = await controller.ObterPorIdAsync(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as UsuarioResponse;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Id, value.Id);
        }

    }
}
