using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class ResetSenhaRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
