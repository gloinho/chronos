using Chronos.Domain.Exceptions;
using Chronos.Domain.Utils;

namespace Chronos.Domain.Contracts.Response
{
    public class MensagemResponse
    {
        public StatusException Codigo { get; set; }
        public string Descricao { get { return Codigo.Description(); } }
        public List<string> Mensagens { get; set; }
        public string Detalhe { get; set; }
    }

}
