using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Services;
using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Chronos.Testes.Services
{
    [TestClass]
    public class Usuario_ProjetoServiceTest
    {
        private readonly Mock<IUsuario_ProjetoRepository> _mockUsuarioProjetoRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly Mock<IProjetoRepository> _mockProjetoRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;

        public Usuario_ProjetoServiceTest()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockProjetoRepository = new Mock<IProjetoRepository>();
            _mockUsuarioProjetoRepository = new Mock<IUsuario_ProjetoRepository>();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCheckSeUsuarioExiste()
        {
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = service.CheckSeUsuarioExiste(usuario.Id);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestCheckSeUsuarioExisteException()
        {
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Usuario)null);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CheckSeUsuarioExiste(usuario.Id)
            );
            Assert.AreEqual(
                $"O usuario de ID {usuario.Id} não foi encontrado.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCheckSeProjetoExiste()
        {
            var projeto = _fixture.Create<Projeto>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = service.CheckSeProjetoExiste(projeto.Id);

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestCheckSeProjetoExisteException()
        {
            var projeto = _fixture.Create<Projeto>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Projeto)null);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CheckSeProjetoExiste(projeto.Id)
            );
            Assert.AreEqual(
                $"O projeto de ID {projeto.Id} não foi encontrado.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCheckPermissaoColaboradorComPermissao()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = true;
            var usuario = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(
                usuario_projeto.UsuarioId,
                usuario.Email,
                Permissao.Colaborador
            );
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario_projeto);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = service.CheckPermissao(usuario_projeto.Id);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestCheckPermissaoColaboradorSemPermissao()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario_projeto);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CheckPermissao(usuario_projeto.Id)
            );
            Assert.AreEqual(
                "Colaborador não pode interagir com tarefas de outros colaboradores.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCheckPermissaoAdministradorSuasTarefas()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = true;
            var usuario = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(
                usuario_projeto.UsuarioId,
                usuario.Email,
                Permissao.Administrador
            );
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario_projeto);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = service.CheckPermissao(usuario_projeto.Id);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task CheckSeUsuarioFazParteDoProjeto()
        {
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = Usuario_ProjetoFaker.GetRelacao(projeto, usuario);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(projeto.Id))
                .ReturnsAsync(projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = await service.CheckSeUsuarioFazParteDoProjeto(projeto.Id, usuario.Id);
            Assert.AreEqual(usuario_projeto, result);
        }

        [TestMethod]
        public async Task CheckSeUsuarioFazParteDoProjetoException()
        {
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = Usuario_ProjetoFaker.GetRelacao(projeto, usuario);
            usuario_projeto.Ativo = true;
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(projeto.Id))
                .ReturnsAsync(projeto);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync((Usuario_Projeto)null);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );

            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CheckSeUsuarioFazParteDoProjeto(projeto.Id, usuario.Id)
            );
            Assert.AreEqual("Usuário não faz parte do projeto.", result.Mensagens[0]);
        }

        [TestMethod]
        public async Task TestCheckSePodeAlterarTarefaAdministrador()
        {
            var projetoTarget = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = true;
            var tarefa = _fixture.Create<Tarefa>();
            var claims = ClaimConfig.Get(
                usuario_projeto.UsuarioId,
                usuario.Email,
                Permissao.Administrador
            );
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projetoTarget);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Usuario_ProjetoId))
                .ReturnsAsync(usuario_projeto);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = await service.CheckSePodeAlterarTarefa(projetoTarget.Id, tarefa);
            Assert.AreEqual(usuario_projeto, result);
        }

        [TestMethod]
        public async Task TestCheckSePodeAlterarTarefaColaborador()
        {
            var projetoTarget = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = true;
            var tarefa = _fixture.Create<Tarefa>();
            var claims = ClaimConfig.Get(
                usuario_projeto.UsuarioId,
                usuario.Email,
                Permissao.Colaborador
            );
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projetoTarget);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Usuario_ProjetoId))
                .ReturnsAsync(usuario_projeto);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = await service.CheckSePodeAlterarTarefa(projetoTarget.Id, tarefa);
            Assert.AreEqual(usuario_projeto, result);
        }

        [TestMethod]
        public async Task TestCheckSePodeAlterarTarefaColaboradorSemPermissao()
        {
            var projetoTarget = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            var tarefa = _fixture.Create<Tarefa>();
            var claims = ClaimConfig.Get(usuario.Id, usuario.Email, Permissao.Colaborador);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projetoTarget);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(tarefa.Usuario_ProjetoId))
                .ReturnsAsync(usuario_projeto);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CheckSePodeAlterarTarefa(projetoTarget.Id, tarefa)
            );
            Assert.AreEqual(
                "Não é possivel alterar tarefas de outros usuários.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync((Usuario_Projeto)null);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = service.CadastrarAsync(usuario.Id, projeto.Id);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestCadastrarAsyncRelacaoQueJaExisteEJaEstaAtiva()
        {
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = true;
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.CadastrarAsync(usuario_projeto.UsuarioId, usuario_projeto.ProjetoId)
            );
            Assert.AreEqual(
                $"O usuario de id {usuario_projeto.UsuarioId} já faz parte do projeto {usuario_projeto.ProjetoId} e está ativo.",
                result.Mensagens[0]
            );
        }

        [TestMethod]
        public async Task TestCadastrarASyncRelacaoQueJaExisteENaoEstaAtiva()
        {
            var usuario = _fixture.Create<Usuario>();
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            usuario_projeto.Ativo = false;
            _mockUsuarioRepository
                .Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(usuario_projeto);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = service.CadastrarAsync(
                usuario_projeto.UsuarioId,
                usuario_projeto.ProjetoId
            );
            Assert.AreEqual(Task.CompletedTask, result);
            Assert.AreEqual(true, usuario_projeto.Ativo);
        }

        [TestMethod]
        public async Task TestInativarColaborador()
        {
            var relacao = _fixture.Create<Usuario_Projeto>();
            var projeto = _fixture.Create<Projeto>();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(relacao.ProjetoId))
                .ReturnsAsync(projeto);
            relacao.Ativo = true;
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(relacao);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = service.InativarColaborador(relacao.ProjetoId, relacao.UsuarioId);
            Assert.AreEqual(Task.CompletedTask, result);
            Assert.AreEqual(false, relacao.Ativo);
        }

        [TestMethod]
        public async Task TestInativarColaboradorQueJaEstaInativo()
        {
            var relacao = _fixture.Create<Usuario_Projeto>();
            var projeto = _fixture.Create<Projeto>();
            _mockProjetoRepository
                .Setup(mock => mock.ObterPorIdAsync(relacao.ProjetoId))
                .ReturnsAsync(projeto);
            relacao.Ativo = false;
            _mockUsuarioProjetoRepository
                .Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario_Projeto, bool>>>()))
                .ReturnsAsync(relacao);
            var service = new Usuario_ProjetoService(
                _mockUsuarioProjetoRepository.Object,
                _mockUsuarioRepository.Object,
                _mockProjetoRepository.Object,
                _mockHttpContextAccessor.Object
            );
            var result = await Assert.ThrowsExceptionAsync<BaseException>(
                () => service.InativarColaborador(relacao.ProjetoId, relacao.UsuarioId)
            );
            Assert.AreEqual(
                "Colaborador não está mais ativo no projeto. Falar com administrador do sistema.",
                result.Mensagens[0]
            );
        }
    }
}
