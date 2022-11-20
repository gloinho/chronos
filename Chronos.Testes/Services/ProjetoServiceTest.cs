using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;
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
        private readonly Fixture _fixture;

        public ProjetoServiceTest()
        {
            _mapper = MapperConfig.Get();
            _mockProjetoRepository = new Mock<IProjetoRepository>();
            _mockUsuarioProjetoService = new Mock<IUsuario_ProjetoService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
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

        [TestMethod]
        public async Task TestObterPorId()
        {
            var projeto = ProjetoFaker.GetProjeto();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await service.ObterPorIdAsync(projeto.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(projeto.Id, result.Id);
        }

        [TestMethod]
        public async Task TestObterPorIdInexistente()
        {
            var projeto = ProjetoFaker.GetProjeto();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Projeto)null);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.ObterPorIdAsync(projeto.Id)
            );
            Assert.IsNotNull(result);
            Assert.AreEqual($"Id {projeto.Id} não cadastrado.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioId()
        {
            var usuario = UsuarioFaker.GetUsuario(Permissao.Administrador); // pode resgatar todos os projetos.
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, usuario.Permissao);
            var projetos = ProjetoFaker.GetProjetos();
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSeUsuarioExiste(It.IsAny<int>()))
                .Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorUsuarioIdAsync(usuario.Id))
                .ReturnsAsync(projetos);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await service.ObterPorUsuarioId(usuario.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(projetos.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioIdSendoColaborador()
        {
            var usuario = UsuarioFaker.GetUsuario(Permissao.Colaborador); // só pode resgatar seus projetos.
            var outroUsuario = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, usuario.Permissao);

            var projetos = ProjetoFaker.GetProjetos();
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSeUsuarioExiste(It.IsAny<int>()))
                .Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorUsuarioIdAsync(usuario.Id))
                .ReturnsAsync(projetos);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.ObterPorUsuarioId(outroUsuario.Id)
            );
            Assert.AreEqual(
                "Colaborador não pode interagir com projetos de outros colaboradores.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var projetos = ProjetoFaker.GetProjetos();
            _mockProjetoRepository.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(projetos);
            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );
            var result = await service.ObterTodosAsync();
            Assert.AreEqual(projetos.Count, result.Count);
        }

        [TestMethod]
        public async Task TestAdicionarColaboradores()
        {
            var request = _fixture.Create<AdicionarColaboradoresRequest>();
            var projeto = _fixture.Create<Projeto>();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CadastrarAsync(It.IsAny<Usuario_Projeto>()))
                .Returns(Task.CompletedTask);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );

            var result = await service.AdicionarColaboradores(projeto.Id, request);
            Assert.AreEqual(
                $"O(s) usuario(s) foram adicionados ao projeto de ID {projeto.Id} com sucesso",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var projeto = ProjetoFaker.GetProjeto();
            var request = ProjetoToContractFaker.GetRequest();
            var projetoAlterado = ProjetoFaker.GetProjeto();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);

            _mockProjetoRepository
                .Setup(mock => mock.AlterarAsync(projeto))
                .ReturnsAsync(projetoAlterado);

            var service = new ProjetoService(
                _mockHttpContextAccessor.Object,
                _mockProjetoRepository.Object,
                _mapper,
                _mockUsuarioProjetoService.Object
            );
            var result = await service.AlterarAsync(projeto.Id, request);
            Assert.AreEqual("Alterado com Sucesso", result.Mensagens[0]);
        }
    }
}
