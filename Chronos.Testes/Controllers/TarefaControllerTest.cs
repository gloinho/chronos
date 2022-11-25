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
    public class TarefaControllerTest
    {
        private readonly Mock<ITarefaService> _mockTarefaService;
        private readonly Fixture _fixture;

        public TarefaControllerTest()
        {
            _mockTarefaService = new Mock<ITarefaService>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<TarefaRequest>();
            _mockTarefaService.Setup(mock => mock.CadastrarAsync(request)).ReturnsAsync(message);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.CadastrarAsync(request);

            Assert.IsNotNull(result);

            var createdResult = result as CreatedResult;
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<TarefaRequest>();
            _mockTarefaService
                .Setup(mock => mock.AlterarAsync(It.IsAny<int>(), request))
                .ReturnsAsync(message);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.AlterarAsync(It.IsAny<int>(), request);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var message = _fixture.Create<MensagemResponse>();
            var request = _fixture.Create<TarefaRequest>();
            _mockTarefaService
                .Setup(mock => mock.DeletarAsync(It.IsAny<int>()))
                .ReturnsAsync(message);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.DeletarAsync(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterTodosAsync();

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var response = _fixture.Create<TarefaResponse>();
            _mockTarefaService
                .Setup(mock => mock.ObterPorIdAsync(response.Id))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterPorIdAsync(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as TarefaResponse;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Id, value.Id);
        }

        [TestMethod]
        public async Task TestStartTarefa()
        {
            var response = _fixture.Create<TarefaResponse>();
            _mockTarefaService.Setup(mock => mock.StartTarefa(response.Id)).ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.StartTarefa(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as TarefaResponse;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Id, value.Id);
        }

        [TestMethod]
        public async Task TestStopTarefa()
        {
            var response = _fixture.Create<TarefaResponse>();
            _mockTarefaService.Setup(mock => mock.StopTarefa(response.Id)).ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.StopTarefa(response.Id);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as TarefaResponse;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Id, value.Id);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioIdAsync()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService
                .Setup(mock => mock.ObterPorUsuarioId(It.IsAny<int>()))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterPorUsuarioIdAsync(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoDia()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService
                .Setup(mock => mock.ObterTarefasDoDia(It.IsAny<int>()))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterTarefasDoDia(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoMes()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService
                .Setup(mock => mock.ObterTarefasDoMes(It.IsAny<int>()))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterTarefasDoMes(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDaSemana()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService
                .Setup(mock => mock.ObterTarefasDaSemana(It.IsAny<int>()))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterTarefasDaSemana(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoProjeto()
        {
            var response = _fixture.Create<List<TarefaResponse>>();
            _mockTarefaService
                .Setup(mock => mock.ObterTarefasDoProjeto(It.IsAny<int>()))
                .ReturnsAsync(response);

            var controller = new TarefaController(_mockTarefaService.Object);

            var result = await controller.ObterTarefasDoProjeto(It.IsAny<int>());

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value as List<TarefaResponse>;
            Assert.IsNotNull(value);
            Assert.AreEqual(response.Count, value.Count);
        }
    }
}
