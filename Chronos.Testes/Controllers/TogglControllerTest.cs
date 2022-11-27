using Chronos.Api.Controllers;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Testes.Settings;

namespace Chronos.Testes.Controllers
{
    [TestClass]
    public class TogglControllerTest
    {
        private readonly Mock<ITogglService> _mockTogglService;
        private readonly Fixture _fixture;

        public TogglControllerTest()
        {
            _mockTogglService = new Mock<ITogglService>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestObterHorasToggl()
        {
            var request = _fixture.Create<TogglDetailedRequest>();
            var mensagem = _fixture.Create<TogglDetailedResponse>();
            _mockTogglService.Setup(mock => mock.ObterHorasToggl(request)).ReturnsAsync(mensagem);

            var controller = new TogglController(_mockTogglService.Object);

            var result = await controller.ObterHorasToggl(request);

            Assert.IsNotNull(result);
        }
    }
}
