using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class ProjetoRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Fixture _fixture;

        public ProjetoRepositoryTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var projeto = _fixture.Create<Projeto>();
            _mockContext
                .Setup(mock => mock.Set<Projeto>())
                .ReturnsDbSet(new List<Projeto> { projeto });

            var repository = new ProjetoRepository(_mockContext.Object);

            var result = repository.CadastrarAsync(projeto);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var projeto = _fixture.Create<Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Projeto>())
                .ReturnsDbSet(new List<Projeto> { projeto });
            var repository = new ProjetoRepository(_mockContext.Object);

            var result = await repository.AlterarAsync(projeto);
            Assert.AreEqual(projeto.Id, result.Id);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var projeto = _fixture.Create<Projeto>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(new List<Projeto>());
            var repository = new ProjetoRepository(_mockContext.Object);

            var result = repository.DeletarAsync(projeto);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var projeto = _fixture.Create<Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Projeto>().FindAsync(It.IsAny<int>()))
                .ReturnsAsync(projeto);

            var repository = new ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterPorIdAsync(projeto.Id);
            Assert.AreEqual(projeto.Id, result.Id);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var projetos = _fixture.Create<List<Projeto>>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(projetos);

            var repository = new ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterTodosAsync();
            Assert.AreEqual(projetos.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterAsync()
        {
            var projeto = _fixture.Create<Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Projeto>())
                .ReturnsDbSet(new List<Projeto>() { projeto });

            var repository = new ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterAsync(u => u.Id == projeto.Id);
            Assert.AreEqual(projeto, result);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioIdAsync()
        {
            var context = new InMemoryContext().GetContext();

            // Arrange
            var usuario = UsuarioFaker.GetUsuario();
            var projeto = ProjetoFaker.GetProjeto();
            context.Usuarios.Add(usuario);
            context.Projetos.Add(projeto);
            context.SaveChanges();
            var usuario_projeto = Usuario_ProjetoFaker.GetRelacao(projeto, usuario);
            context.Usuarios_Projetos.Add(usuario_projeto);
            context.SaveChanges();
            var tarefas = new List<Tarefa>() { TarefaFaker.GetTarefaDeHoje(usuario_projeto.Id), };
            context.Tarefas.AddRange(tarefas);
            context.SaveChanges();

            // Act
            var repository = new ProjetoRepository(context);
            var result = await repository.ObterPorUsuarioIdAsync(usuario.Id);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Contains(projeto));
        }
    }
}
