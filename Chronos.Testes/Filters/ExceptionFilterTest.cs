using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Chronos.Testes.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Chronos.Api.Filters;
using Chronos.Domain.Exceptions;

namespace Chronos.Testes.Filters
{
    [TestClass]
    public class ExceptionFilterTest
    {
        private readonly Fixture _fixture;
        private readonly ActionContext _actionContext;
        private readonly List<IFilterMetadata> _filterMetadata;

        public ExceptionFilterTest()
        {
            _fixture = FixtureConfig.Get();
            _actionContext = new ActionContext
            {
                ActionDescriptor = new ActionDescriptor(),
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData()
            };
            _filterMetadata = new List<IFilterMetadata>();
        }

        [TestMethod]
        public async Task TestOnExceptionInformacaoException()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new BaseException(
                    StatusException.NaoEncontrado,
                    "Nenhum dado encontrado."
                )
            };

            var exceptionFilter = new ExceptionFilter();

            var result = exceptionFilter.OnExceptionAsync(exceptionContext);

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionIsException()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new Exception("Erro inesperado.")
            };

            var exceptionFilter = new ExceptionFilter();

            var result = exceptionFilter.OnExceptionAsync(exceptionContext);

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterInformacaoExceptionInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new BaseException(
                    StatusException.NaoEncontrado,
                    "Nenhum dado encontrado",
                    new Exception("Erro Inner Exception")
                )
            };
            var exception = new ExceptionFilter();

            var result = exception.OnExceptionAsync(exceptionContext);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception("Erro genérico", new Exception("Erro Inner Exception"))
            };
            var exception = new ExceptionFilter();

            var result = exception.OnExceptionAsync(exceptionContext);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterInformacaoExceptionNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new BaseException(StatusException.NaoEncontrado, new List<string>())
            };
            var exception = new ExceptionFilter();

            var result = exception.OnExceptionAsync(exceptionContext);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = null
            };
            var exception = new ExceptionFilter();

            var result = exception.OnExceptionAsync(exceptionContext);
            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterValidationException()
        {
            var errors = _fixture.Create<List<FluentValidation.Results.ValidationFailure>>();

            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new FluentValidation.ValidationException("error", errors)
            };

            var exceptionFilter = new ExceptionFilter();

            var result = exceptionFilter.OnExceptionAsync(exceptionContext);

            Assert.AreEqual(Task.CompletedTask, result);
        }

        [TestMethod]
        public async Task TestOnExceptionFilterValidationExceptionInnerException()
        {
            var errors = _fixture.Create<List<FluentValidation.Results.ValidationFailure>>();

            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new FluentValidation.ValidationException("error", errors)
            };

            var exceptionFilter = new ExceptionFilter();

            var result = exceptionFilter.OnExceptionAsync(exceptionContext);

            Assert.AreEqual(Task.CompletedTask, result);
        }
    }
}
