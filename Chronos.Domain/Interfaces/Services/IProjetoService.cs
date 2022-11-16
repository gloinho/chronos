using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IProjetoService : IBaseService<ProjetoRequest, ProjetoResponse>
    {
        Task<MensagemResponse> AdicionarColaboradores(
            int projetoId,
            AdicionarColaboradoresRequest request
        );
        Task<List<ProjetoResponse>> ObterPorUsuarioId(int usuarioId);
    }
}
