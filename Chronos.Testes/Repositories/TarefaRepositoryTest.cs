using Chronos.Testes.Fakers;
using Chronos.Testes.Settings;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class TarefaRepositoryTest
    {
        private readonly ApplicationDbContext _context;

        public TarefaRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MockDB")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            // Arrange -> Popular o banco de dados com uma entidade de cada tipo.
            var usuario = UsuarioFaker.GetUsuario();
            var projeto = ProjetoFaker.GetProjeto();
            _context.Usuarios.Add(usuario);
            _context.Projetos.Add(projeto);
            _context.SaveChanges();
            var usuario_projeto = Usuario_ProjetoFaker.GetRelacao(
                _context.Projetos.First(),
                _context.Usuarios.First()
            );
            _context.Usuarios_Projetos.Add(usuario_projeto);
            _context.SaveChanges();
            var tarefas = new List<Tarefa>()
            {
                TarefaFaker.GetTarefa(usuario_projeto.Id), // dia + semana
                TarefaFaker.GetTarefaAddTresDias(usuario_projeto.Id), // semana
                TarefaFaker.GetTarefaAntiga(usuario_projeto.Id) // nenhum dos dois
            };
            _context.Tarefas.AddRange(tarefas);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var repository = new TarefaRepository(_context);
            var result = await repository.ObterPorIdAsync(1);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, result.Usuario_Projeto.Id);
            Assert.AreEqual(1, result.Usuario_Projeto.Projeto.Id);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var repository = new TarefaRepository(_context);
            var result = await repository.ObterTodosAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task TestObterPorUsuarioIdAsync()
        {
            var repository = new TarefaRepository(_context);
            var result = await repository.ObterPorUsuarioIdAsync(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        // EF DateDiff não é suportado pelo In Memory Database :
        // https://github.com/dotnet/efcore/issues/22566
        // https://github.com/dotnet/efcore/issues/22566#issuecomment-693552960

        //[TestMethod]
        //public async Task TestGetTarefasDia()
        //{
        //    var repository = new TarefaRepository(_context);
        //    var result = await repository.GetTarefasDia(1);
        //    Assert.AreEqual(1, result.Count);
        //}

        //[TestMethod]
        //public async Task TestGetTarefasSemana()
        //{
        //    var repository = new TarefaRepository(_context);
        //    var result = await repository.GetTarefasSemana(1);
        //    Assert.AreEqual(2, result.Count);
        //}

        //[TestMethod]
        //public async Task TestGetTarefasMes()
        //{
        //    var repository = new TarefaRepository(_context);
        //    var result = await repository.GetTarefasMes(1);
        //    Assert.AreEqual(2, result.Count);
        //}

        [TestMethod]
        public async Task TestGetTarefasPorProjeto()
        {
            var repository = new TarefaRepository(_context);
            var result = await repository.GetTarefasProjeto(1);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var tarefa = TarefaFaker.GetTarefa(1);
            var repository = new TarefaRepository(_context);

            var result = repository.CadastrarAsync(tarefa);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var tarefa = _context.Tarefas.Last();
            var repository = new TarefaRepository(_context);

            var result = await repository.AlterarAsync(tarefa);
            Assert.AreEqual(tarefa.Id, result.Id);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var tarefa = _context.Tarefas.Last();
            var repository = new TarefaRepository(_context);

            var result = repository.DeletarAsync(tarefa);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestObterAsync()
        {
            var tarefa = _context.Tarefas.Last();
            var repository = new TarefaRepository(_context);

            var result = await repository.ObterAsync(t => t.Id == tarefa.Id);
            Assert.AreEqual(tarefa, result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
