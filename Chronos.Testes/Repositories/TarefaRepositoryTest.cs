using Chronos.Testes.Settings;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class TarefaRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Fixture _fixture;

        public TarefaRepositoryTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var tarefa = _fixture.Create<Tarefa>();
            _mockContext
                .Setup(mock => mock.Set<Tarefa>())
                .ReturnsDbSet(new List<Tarefa> { tarefa });

            var repository = new TarefaRepository(_mockContext.Object);

            var result = repository.CadastrarAsync(tarefa);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var tarefa = _fixture.Create<Tarefa>();

            _mockContext
                .Setup(mock => mock.Set<Tarefa>())
                .ReturnsDbSet(new List<Tarefa> { tarefa });
            var repository = new TarefaRepository(_mockContext.Object);

            var result = await repository.AlterarAsync(tarefa);
            Assert.AreEqual(tarefa.Id, result.Id);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var tarefa = _fixture.Create<Tarefa>();

            _mockContext.Setup(mock => mock.Set<Tarefa>()).ReturnsDbSet(new List<Tarefa>());
            var repository = new TarefaRepository(_mockContext.Object);

            var result = repository.DeletarAsync(tarefa);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        //[TestMethod]
        //public async Task TestObterPorIdAsync()
        //{
        //    var tarefa = _fixture.Create<Tarefa>();
        //    var usuario_projeto = _fixture.Create<Usuario_Projeto>();
        //    tarefa.Usuario_Projeto = usuario_projeto;
        //    usuario_projeto.Tarefas.Add(tarefa);

        //    var tarefas = new List<Tarefa>() { tarefa }.AsQueryable();
        //    var usuarios_projetos = new List<Usuario_Projeto>() { usuario_projeto }.AsQueryable();

        //    var mockTarefas = new Mock<DbSet<Tarefa>>();

        //    mockTarefas.As<IQueryable<Tarefa>>().Setup(m => m.Provider).Returns(tarefas.Provider);
        //    mockTarefas
        //        .As<IQueryable<Tarefa>>()
        //        .Setup(m => m.Expression)
        //        .Returns(tarefas.Expression);
        //    mockTarefas
        //        .As<IQueryable<Tarefa>>()
        //        .Setup(m => m.ElementType)
        //        .Returns(tarefas.ElementType);
        //    mockTarefas
        //        .As<IQueryable<Tarefa>>()
        //        .Setup(m => m.GetEnumerator())
        //        .Returns(tarefas.GetEnumerator());

        //    var mockusuarios_projetos = new Mock<DbSet<Usuario_Projeto>>();

        //    mockusuarios_projetos
        //        .As<IQueryable<Usuario_Projeto>>()
        //        .Setup(m => m.Provider)
        //        .Returns(usuarios_projetos.Provider);
        //    mockusuarios_projetos
        //        .As<IQueryable<Usuario_Projeto>>()
        //        .Setup(m => m.Expression)
        //        .Returns(usuarios_projetos.Expression);
        //    mockusuarios_projetos
        //        .As<IQueryable<Usuario_Projeto>>()
        //        .Setup(m => m.ElementType)
        //        .Returns(usuarios_projetos.ElementType);
        //    mockusuarios_projetos
        //        .As<IQueryable<Usuario_Projeto>>()
        //        .Setup(m => m.GetEnumerator())
        //        .Returns(usuarios_projetos.GetEnumerator());

        //    var mockContext = new Mock<ApplicationDbContext>(MockBehavior.Loose);
        //    mockTarefas.Setup(p => p.Include(It.IsAny<string>())).Returns(mockTarefas.Object);
        //    mockusuarios_projetos
        //        .Setup(p => p.Include(It.IsAny<string>()))
        //        .Returns(mockusuarios_projetos.Object);

        //    mockContext.Setup(p => p.Set<Tarefa>()).Returns(mockTarefas.Object);

        //    var list = mockContext.Object.Tarefas
        //        .Include(p => p.Usuario_Projeto)
        //        .Select(p => p)
        //        .ToList();
        //}
    }
}
