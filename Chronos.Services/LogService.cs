using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;

namespace Chronos.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<MensagemResponse> LogAsync(
            string localAcao,
            string acao,
            int alterado,
            int? usuarioId
        )
        {
            var alteracao = $"{acao} em {localAcao}. Referencia: Id {alterado}";

            var log = new Log { Responsavel = usuarioId, Alteracao = alteracao };

            await _logRepository.CadastrarAsync(log);

            return new MensagemResponse { Mensagens = new List<string> { "Sucesso" } };
        }
    }
}
