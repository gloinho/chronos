using Chronos.Api.Controllers;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace Chronos.Testes.Controllers
{
    [TestClass]
    public class ProjetoControllerTest
    {
        private readonly Mock<IProjetoService> _mockProjetoService;
        private readonly Fixture _fixture;

        public ProjetoControllerTest()
        {
            _mockProjetoService = new Mock<IProjetoService>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestObterPorUsuarioIdAsync()
        {
            var response = _fixture.Create<UsuarioResponse>();
            var projetosResponse = _fixture.Create<List<ProjetoResponse>>();
            _mockProjetoService.Setup(mock => mock.ObterPorUsuarioId(response.Id)).ReturnsAsync(projetosResponse);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.ObterPorUsuarioIdAsync(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<ProjetoResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(projetosResponse.Count, value.Count);
        }

        [TestMethod]
        public async Task TestInativarColaboradoresAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var projetoResponse = _fixture.Create<ProjetoResponse>();
            var request = _fixture.Create<ColaboradoresRequest>();
            _mockProjetoService.Setup(mock => mock.InativarColaboradores(projetoResponse.Id, request)).ReturnsAsync(message);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.InativarColaboradoresAsync(projetoResponse.Id, request);

            Assert.IsNotNull(result);

            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestAdicionarColaboradoresAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var projetoResponse = _fixture.Create<ProjetoResponse>();
            var request = _fixture.Create<ColaboradoresRequest>();
            _mockProjetoService.Setup(mock => mock.AdicionarColaboradores(projetoResponse.Id, request)).ReturnsAsync(message);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.AdicionarColaboradoresAsync(projetoResponse.Id, request);

            Assert.IsNotNull(result);

            var createdResult = result as CreatedResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<ProjetoRequest>();
            _mockProjetoService.Setup(mock => mock.CadastrarAsync(request)).ReturnsAsync(message);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.CadastrarAsync(request);

            Assert.IsNotNull(result);

            var createdResult = result as CreatedResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<ProjetoRequest>();
            _mockProjetoService.Setup(mock => mock.AlterarAsync(It.IsAny<int>(), request)).ReturnsAsync(message);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.AlterarAsync(It.IsAny<int>(), request);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<ProjetoRequest>();
            _mockProjetoService
                .Setup(mock => mock.DeletarAsync(It.IsAny<int>()))
                .ReturnsAsync(message);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.DeletarAsync(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var response = _fixture.Create<List<ProjetoResponse>>();
            _mockProjetoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(response);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.ObterTodosAsync();

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<ProjetoResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var response = _fixture.Create<ProjetoResponse>();
            _mockProjetoService
                .Setup(mock => mock.ObterPorIdAsync(response.Id))
                .ReturnsAsync(response);

            var controller = new ProjetoController(_mockProjetoService.Object);

            var result = await controller.ObterPorIdAsync(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as ProjetoResponse;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Id, value.Id);
        }

    }
}
