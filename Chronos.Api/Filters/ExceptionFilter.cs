using Chronos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Utils;

namespace Chronos.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new MensagemResponse();

            if (context.Exception is BaseException)
            {
                var informacaoException = (BaseException)context.Exception;

                response.Codigo = informacaoException.Codigo;
                response.Mensagens = informacaoException.Mensagens;
                response.Detalhe =
                    $"{context.Exception.Message} | {context.Exception.InnerException?.Message}";
            }
            else
            {
                response.Codigo = StatusException.Erro;
                response.Mensagens = new List<string> { "Erro inesperado" };
                response.Detalhe =
                    $"{context.Exception?.Message} | {context.Exception?.InnerException?.Message}";
            }

            if (context.Exception is FluentValidation.ValidationException)
            {
                var infoex = (FluentValidation.ValidationException)context.Exception;
                response.Codigo = StatusException.FormatoIncorreto;
                response.Mensagens = infoex.Errors.Select(p => p.ErrorMessage).ToList();
            }
            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Codigo.GetStatusCode()
            };

            OnException(context);
            return Task.CompletedTask;
        }
    }
}
