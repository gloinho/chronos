using Chronos.Api.Controllers;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Domain.Shared;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Testes.Controllers
{
    [TestClass]
    public class AutenticacaoControllerTest
    {
        private readonly Mock<IAutenticacaoService> _mockAutenticacaoService;
        private readonly Fixture _fixture;

        public AutenticacaoControllerTest()
        {
            _mockAutenticacaoService = new Mock<IAutenticacaoService>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestConfirmarUsuario()
        {
            var usuario = _fixture.Create<Usuario>();
            var appSettings = new AppSettings()
            {
                SecurityKey = "hWmZq4t6w9z$C&F)J@NcRfUjXn2r5u8x!A%D*G-KaPdSgVkYp3s6v9y$B?E(H+Mb"
            };
            var token = Token.GenerateToken(usuario, appSettings.SecurityKey);
            var mensagem = _fixture.Create<MensagemResponse>();
            _mockAutenticacaoService.Setup(mock => mock.Confirmar(token)).ReturnsAsync(mensagem);

            var controller = new AutenticacaoController(_mockAutenticacaoService.Object);

            var result = await controller.ConfirmarUsuario(token);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task TestLogin()
        {
            var request = _fixture.Create<LoginRequest>();
            var mensagem = _fixture.Create<MensagemResponse>();
            _mockAutenticacaoService.Setup(mock => mock.Login(request)).ReturnsAsync(mensagem);

            var controller = new AutenticacaoController(_mockAutenticacaoService.Object);

            var result = await controller.Login(request);

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }
    }
}
