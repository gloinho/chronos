using Chronos.Testes.Settings;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chronos.Testes.Repositories
{
    [TestClass]
    public class LogRepositoryTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Fixture _fixture;

        public LogRepositoryTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _fixture = FixtureConfig.Get();
        }

        [TestMethod]
        public async Task TestCadastrarAsync()
        {
            var log = _fixture.Create<Log>();
            _mockContext.Setup(mock => mock.Set<Log>())
                .ReturnsDbSet(new List<Log> { log });

            var repository = new LogRepository(_mockContext.Object);

            var result = repository.CadastrarAsync(log);
            Assert.AreEqual(Task.CompletedTask, result);
        }

     
    }
}
