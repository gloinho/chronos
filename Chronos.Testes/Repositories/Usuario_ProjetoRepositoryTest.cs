using Chronos.Testes.Settings;
using Moq.EntityFrameworkCore;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class Usuario_ProjetoRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Fixture _fixture;

        public Usuario_ProjetoRepositoryTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            _mockContext
                .Setup(mock => mock.Set<Usuario_Projeto>())
                .ReturnsDbSet(new List<Usuario_Projeto> { usuario_projeto });

            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = repository.CadastrarAsync(usuario_projeto);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Usuario_Projeto>())
                .ReturnsDbSet(new List<Usuario_Projeto> { usuario_projeto });
            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = await repository.AlterarAsync(usuario_projeto);
            Assert.AreEqual(usuario_projeto.Id, result.Id);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Usuario_Projeto>())
                .ReturnsDbSet(new List<Usuario_Projeto>());
            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = repository.DeletarAsync(usuario_projeto);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();

            _mockContext
                .Setup(mock => mock.Set<Usuario_Projeto>().FindAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario_projeto);

            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterPorIdAsync(usuario_projeto.Id);
            Assert.AreEqual(usuario_projeto.Id, result.Id);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var usuario_projetos = _fixture.Create<List<Usuario_Projeto>>();

            _mockContext.Setup(mock => mock.Set<Usuario_Projeto>()).ReturnsDbSet(usuario_projetos);

            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterTodosAsync();
            Assert.AreEqual(usuario_projetos.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterAsync()
        {
            var usuario_projeto = _fixture.Create<Usuario_Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            usuario_projeto.UsuarioId = usuario.Id;
            usuario_projeto.ProjetoId = projeto.Id;

            _mockContext
                .Setup(mock => mock.Set<Usuario_Projeto>())
                .ReturnsDbSet(new List<Usuario_Projeto>() { usuario_projeto });

            var repository = new Usuario_ProjetoRepository(_mockContext.Object);

            var result = await repository.ObterAsync(
                u => u.ProjetoId == projeto.Id && u.UsuarioId == usuario.Id
            );
            Assert.AreEqual(usuario_projeto, result);
        }
    }
}
