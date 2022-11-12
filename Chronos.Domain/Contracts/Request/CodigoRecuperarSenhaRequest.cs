

using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class CodigoRecuperarSenhaRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
