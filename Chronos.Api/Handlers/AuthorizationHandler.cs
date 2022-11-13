using Chronos.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Chronos.Domain.Contracts.Response;

namespace Chronos.Api.Handlers
{
    public class AuthorizationHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _handler;

        public AuthorizationHandler()
        {
            _handler = new AuthorizationMiddlewareResultHandler();
        }

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            var informacaoResponse = new MensagemResponse();

            if (!policyAuthorizationResult.Succeeded)
            {
                if (policyAuthorizationResult.Forbidden)
                {
                    httpContext.Response.StatusCode = 403;
                    informacaoResponse = new MensagemResponse
                    {
                        Codigo = StatusException.AcessoProibido,
                        Mensagens = new List<string> { "Acesso não permitido" }
                    };
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    informacaoResponse = new MensagemResponse
                    {
                        Codigo = StatusException.NaoAutorizado,
                        Mensagens = new List<string> { "Acesso negado" }
                    };
                }

                await httpContext.Response.WriteAsJsonAsync(informacaoResponse);
            }
            else
                await _handler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }
    }

}
