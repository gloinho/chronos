using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Http;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class TarefaServiceTest
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;
        private readonly Mock<IUsuario_ProjetoService> _mockUsuarioProjetoService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ILogService> _mockLogService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public TarefaServiceTest()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();
            _mockUsuarioProjetoService = new Mock<IUsuario_ProjetoService>();
            _mapper = MapperConfig.Get();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
            _mockLogService = new Mock<ILogService>();
        }

        [TestMethod]
        public async Task TestAlterarAsyncSendoAdmin()
        {
            var usuario = _fixture.Create<Usuario>();
            var request = TarefaToContractFaker.GetRequest();
            var tarefa = TarefaFaker.GetTarefa();
            var tarefaAlterada = TarefaFaker.GetTarefa();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            tarefaAlterada.Id = tarefa.Id;
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Administrador);

            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSePodeAlterarTarefa(It.IsAny<int>(), tarefa))
                .ReturnsAsync(usuario_projeto);
            _mockTarefaRepository
                .Setup(mock => mock.AlterarAsync(tarefa))
                .ReturnsAsync(tarefaAlterada);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.AlterarAsync(tarefa.Id, request);

            Assert.IsNotNull(result);
            Assert.AreEqual("Alterado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestAlterarAsyncTarefaInclusaAMaisDeDoisDiasSendoColaborador()
        {
            var request = TarefaToContractFaker.GetRequest();
            var tarefa = TarefaFaker.GetTarefaAntiga();
            var user = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(user.Id, user.Email, Permissao.Colaborador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.AlterarAsync(tarefa.Id, request)
            );

            Assert.AreEqual(
                $"O tempo de editar a tarefa expirou em {tarefa.DataInclusao.AddDays(2)}",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestAlterarAsyncTarefaSendoColaborador()
        {
            var usuario = _fixture.Create<Usuario>();
            var request = TarefaToContractFaker.GetRequest();
            var tarefa = TarefaFaker.GetTarefa();
            var tarefaAlterada = TarefaFaker.GetTarefa();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            tarefaAlterada.Id = tarefa.Id;
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);

            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);

            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSePodeAlterarTarefa(It.IsAny<int>(), tarefa))
                .ReturnsAsync(usuario_projeto);
            _mockTarefaRepository
                .Setup(mock => mock.AlterarAsync(tarefa))
                .ReturnsAsync(tarefaAlterada);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await service.AlterarAsync(tarefa.Id, request);

            Assert.IsNotNull(result);
            Assert.AreEqual("Alterado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task CadastrarAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            var request = TarefaToContractFaker.GetRequest();
            var usuario = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);

            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSeUsuarioFazParteDoProjeto(request.ProjetoId, usuario.Id))
                .ReturnsAsync(usuario_projeto);
            _mockTarefaRepository
                .Setup(mock => mock.CadastrarAsync(It.IsAny<Tarefa>()))
                .Returns(Task.CompletedTask);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await service.CadastrarAsync(request);

            Assert.IsNotNull(result);
            Assert.AreEqual("Tarefa cadastrada com sucesso.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var tarefa = TarefaFaker.GetTarefa();
            var request = TarefaToContractFaker.GetRequest();
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);
            _mockTarefaRepository
                .Setup(mock => mock.DeletarAsync(tarefa))
                .Returns(Task.CompletedTask);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.DeletarAsync(It.IsAny<int>());

            Assert.AreEqual("Deletado com Sucesso", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestObterPorId()
        {
            var tarefa = TarefaFaker.GetTarefa();
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var horas = tarefa.DataFinal.Value.TimeOfDay - tarefa.DataInicial.Value.TimeOfDay;
            var result = await service.ObterPorIdAsync(tarefa.Id);
            Assert.AreEqual(tarefa.Id, result.Id);
            Assert.AreEqual(horas, result.TotalHoras);

            tarefa.DataFinal = null;
            tarefa.DataInicial = null;
            var result2 = await service.ObterPorIdAsync(tarefa.Id);
            Assert.AreEqual(TimeSpan.Zero, result2.TotalHoras);
        }

        [TestMethod]
        public async Task TestObterPorIdDataFinalNulaEDataInicialNaoNula()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataFinal = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);

            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterPorIdAsync(tarefa.Id);
            Assert.AreEqual(tarefa.Id, result.Id);
            Assert.AreNotEqual(TimeSpan.Zero, result.TotalHoras);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioId()
        {
            var tarefas = TarefaFaker.GetTarefas();

            _mockTarefaRepository
                .Setup(mock => mock.ObterPorUsuarioIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterPorUsuarioId(It.IsAny<int>());
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var tarefas = TarefaFaker.GetTarefas();
            _mockTarefaRepository.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTodosAsync();
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoDia()
        {
            var tarefas = TarefaFaker.GetTarefas();
            var usuario = _fixture.Create<Usuario>();
            _mockTarefaRepository
                .Setup(mock => mock.GetTarefasDia(usuario.Id))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTarefasPorFiltro(usuario.Id, FiltroRequest.Dia);
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoMes()
        {
            var tarefas = TarefaFaker.GetTarefas();
            var usuario = _fixture.Create<Usuario>();
            _mockTarefaRepository
                .Setup(mock => mock.GetTarefasMes(usuario.Id))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTarefasPorFiltro(usuario.Id, FiltroRequest.Mes);
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDaSemana()
        {
            var tarefas = TarefaFaker.GetTarefas();
            var usuario = _fixture.Create<Usuario>();
            _mockTarefaRepository
                .Setup(mock => mock.GetTarefasSemana(usuario.Id))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTarefasPorFiltro(usuario.Id, FiltroRequest.Semana);
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoProjetoSendoColaborador()
        {
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var tarefas = TarefaFaker.GetTarefas();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSeUsuarioFazParteDoProjeto(usuario.Id, projeto.Id))
                .ReturnsAsync(It.IsAny<Usuario_Projeto>);
            _mockTarefaRepository
                .Setup(mock => mock.GetTarefasProjeto(projeto.Id))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTarefasDoProjeto(projeto.Id);
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterTarefasDoProjetoSendoAdmin()
        {
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var tarefas = TarefaFaker.GetTarefas();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Administrador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckSeUsuarioFazParteDoProjeto(usuario.Id, projeto.Id))
                .ReturnsAsync(It.IsAny<Usuario_Projeto>);
            _mockTarefaRepository
                .Setup(mock => mock.GetTarefasProjeto(projeto.Id))
                .ReturnsAsync(tarefas);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );

            var result = await service.ObterTarefasDoProjeto(projeto.Id);
            Assert.AreEqual(tarefas.Count, result.Count);
        }

        [TestMethod]
        public async Task TestStartTarefa()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataFinal = null;
            tarefa.DataInicial = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await service.StartTarefa(tarefa.Id);
            Assert.IsNotNull(result.DataInicial);
            Assert.AreEqual(DateTime.Now.Minute, result.DataInicial.Value.Minute);
        }

        [TestMethod]
        public async Task TestStartTarefaQueJaIniciou()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataInicial = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.StartTarefa(tarefa.Id)
            );
            Assert.AreEqual(
                $"Tarefa de id {tarefa.Id} já foi iniciada ou finalizada.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestStartTarefaQueJaFinalizou()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataFinal = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.StartTarefa(tarefa.Id)
            );
            Assert.AreEqual(
                $"Tarefa de id {tarefa.Id} já foi iniciada ou finalizada.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestStopTarefa()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataFinal = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await service.StopTarefa(tarefa.Id);
            Assert.IsNotNull(result.DataFinal);
            Assert.AreEqual(DateTime.Now.Minute, result.DataFinal.Value.Minute);
        }

        [TestMethod]
        public async Task TestStopTarefaQueNaoIniciou()
        {
            var tarefa = TarefaFaker.GetTarefa();
            tarefa.DataFinal = null;
            tarefa.DataInicial = null;
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.StopTarefa(tarefa.Id)
            );
            Assert.AreEqual(
                $"Tarefa de id {tarefa.Id} ainda não foi iniciada",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestStopTarefaQueJaFinalizou()
        {
            var tarefa = TarefaFaker.GetTarefa();
            _mockTarefaRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);
            _mockUsuarioProjetoService
                .Setup(mock => mock.CheckPermissaoRelacao(tarefa.Usuario_ProjetoId))
                .Returns(Task.CompletedTask);
            _mockTarefaRepository.Setup(mock => mock.AlterarAsync(tarefa)).ReturnsAsync(tarefa);
            var service = new TarefaService(
                _mockHttpContextAccessor.Object,
                _mockUsuarioProjetoService.Object,
                _mockTarefaRepository.Object,
                _mockLogService.Object,
                _mapper
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.StopTarefa(tarefa.Id)
            );
            Assert.AreEqual($"Tarefa de id {tarefa.Id} já foi finalizada.", result.Mensagens[0]);
        }
    }
}
