using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class AdicionarColaboradoresRequest
    {
        [Required]
        public List<int> Usuarios { get; set; }
    }
}
