using AutoMapper;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Http;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class ProjetoServiceTest
    {
        private readonly Mock<IProjetoRepository> _mockProjetoRepository;
        private readonly Mock<IUsuario_ProjetoService> _mockUsuarioProjetoService;
        private readonly IMapper _mapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public ProjetoServiceTest()
        {
            _mapper = MapperConfig.Get();
            _mockProjetoRepository = new Mock<IProjetoRepository>();
            _mockUsuarioProjetoService = new Mock<IUsuario_ProjetoService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var request = ProjetoToContractFaker.GetRequest();
            _mockProjetoRepository
                .Setup(mock => mock.CadastrarAsync(It.IsAny<Projeto>()))
                .Returns(Task.CompletedTask);
            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );
            var result = await service.CadastrarAsync(request);
            Assert.IsNotNull(result);
            Assert.AreEqual("Cadastrado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var projeto = ProjetoFaker.GetProjeto();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);
            _mockProjetoRepository
                .Setup(mock => mock.DeletarAsync(It.IsAny<Projeto>()))
                .Returns(Task.CompletedTask);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await service.DeletarAsync(projeto.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Deletado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestDeletarAsyncIdInexistente()
        {
            var projeto = ProjetoFaker.GetProjeto();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(projeto.Id))
                .ReturnsAsync((Projeto)null);
            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.DeletarAsync(projeto.Id)
            );
            Assert.IsNotNull(result);
            Assert.AreEqual($"Id {projeto.Id} não cadastrado.", result.Mensagens[0]);
        }
    }
}
