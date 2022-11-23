

namespace Chronos.Domain.Interfaces.Services
{
    public interface ILogService
    {
        Task LogAsync(string localAcao, string acao, int alterado);

    }
}
