using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class ColaboradoresRequest
    {
        [Required]
        public List<int> Usuarios { get; set; }
    }
}
