using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class LogServiceTest
    {
        private readonly Mock<ILogRepository> _mockLogRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;

        public LogServiceTest()
        {
            _mockLogRepository = new Mock<ILogRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task LogAsync()
        {
            _mockLogRepository.Setup(mock => mock.CadastrarAsync(It.IsAny<Log>())).Returns(Task.CompletedTask);

            var tarefa = _fixture.Create<TarefaResponse>();

            var service = new LogService(_mockLogRepository.Object, _mockHttpContextAccessor.Object);      
            
            var result =  await service.LogAsync("TarefaService", "DeletarAsync", tarefa.Id);

            Assert.AreEqual("Sucesso", result.Mensagens[0]);

        }

    }
}
