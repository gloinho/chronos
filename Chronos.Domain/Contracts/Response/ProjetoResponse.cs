using Chronos.Domain.Entities;

namespace Chronos.Domain.Contracts.Response
{
    public class ProjetoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
