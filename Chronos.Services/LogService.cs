using Chronos.Domain.Entities;
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

        public async Task LogAsync(string localAcao, string acao, int alterado)
        {
            var alteracao = $"{acao} em {localAcao}. Referencia: Id {alterado}";

            var log = new Log { Responsavel = UsuarioId, Alteracao = alteracao };

            await _logRepository.CadastrarAsync(log);
        }
    }
}
