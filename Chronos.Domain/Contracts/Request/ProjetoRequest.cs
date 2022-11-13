using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class ProjetoRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }
        public List<int> Usuarios { get; set; }
    }
}
