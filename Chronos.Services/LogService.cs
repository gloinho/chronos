using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _logRepository = logRepository;

        }

        public async Task<MensagemResponse> LogAsync(string localAcao, string acao, int alterado)
        {
            var alteracao = $"{acao} em {localAcao}. Referencia: Id {alterado}";

            var log = new Log { Responsavel = $"UsuarioId: {UsuarioId}" , Alteracao = alteracao };

            await _logRepository.CadastrarAsync(log);

            return new MensagemResponse
            {
                Mensagens = new List<string> {"Sucesso"}
            };
        }

        public async Task<MensagemResponse> LogAsync(string localAcao, string acao, int alterado, int responsavel)
        {
            var alteracao = $"{acao} em {localAcao}. Referencia: Id {alterado}";

            var log = new Log { Responsavel = $"UsuarioId: {responsavel}", Alteracao = alteracao };

            await _logRepository.CadastrarAsync(log);

            return new MensagemResponse
            {
                Mensagens = new List<string> { "Sucesso" }
            };
        }
    }
}
