using Chronos.Testes.Settings;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class UsuarioRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Fixture _fixture;

        public UsuarioRepositoryTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var usuario = _fixture.Create<Usuario>();
            _mockContext
                .Setup(mock => mock.Set<Usuario>())
                .ReturnsDbSet(new List<Usuario> { usuario });

            var repository = new UsuarioRepository(_mockContext.Object);

            var result = repository.CadastrarAsync(usuario);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestAlterarAsync()
        {
            var usuario = _fixture.Create<Usuario>();

            _mockContext
                .Setup(mock => mock.Set<Usuario>())
                .ReturnsDbSet(new List<Usuario> { usuario });
            var repository = new UsuarioRepository(_mockContext.Object);

            var result = await repository.AlterarAsync(usuario);
            Assert.AreEqual(usuario.Id, result.Id);
        }

        [TestMethod]
        public async Task TestDeletarAsync()
        {
            var usuario = _fixture.Create<Usuario>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(new List<Usuario>());
            var repository = new UsuarioRepository(_mockContext.Object);

            var result = repository.DeletarAsync(usuario);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestObterPorIdAsync()
        {
            var usuario = _fixture.Create<Usuario>();

            _mockContext
                .Setup(mock => mock.Set<Usuario>().FindAsync(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            var repository = new UsuarioRepository(_mockContext.Object);

            var result = await repository.ObterPorIdAsync(usuario.Id);
            Assert.AreEqual(usuario.Id, result.Id);
        }

        [TestMethod]
        public async Task TestObterTodosAsync()
        {
            var usuarios = _fixture.Create<List<Usuario>>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(usuarios);

            var repository = new UsuarioRepository(_mockContext.Object);

            var result = await repository.ObterTodosAsync();
            Assert.AreEqual(usuarios.Count, result.Count);
        }

        [TestMethod]
        public async Task TestObterAsync()
        {
            var usuario = _fixture.Create<Usuario>();

            _mockContext
                .Setup(mock => mock.Set<Usuario>())
                .ReturnsDbSet(new List<Usuario>() { usuario });

            var repository = new UsuarioRepository(_mockContext.Object);

            var result = await repository.ObterAsync(u => u.Email == usuario.Email);
            Assert.AreEqual(usuario, result);
        }

        [TestMethod]
        public async Task TestConfirmar()
        {
            var usuario = _fixture.Create<Usuario>();
            usuario.Confirmado = false;

            _mockContext
                .Setup(mock => mock.Set<Usuario>())
                .ReturnsDbSet(new List<Usuario> { usuario });

            var repository = new UsuarioRepository(_mockContext.Object);

            var result = repository.Confirmar(usuario);
            Assert.AreEqual(Task.CompletedTask, result);
            Assert.AreEqual(usuario.Confirmado, true);
        }
    }
}
