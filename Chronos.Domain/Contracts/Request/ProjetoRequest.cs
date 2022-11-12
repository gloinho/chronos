
namespace Chronos.Domain.Contracts.Request
{
    public class ProjetoRequest
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<int> Usuarios { get; set; }
    }
}
