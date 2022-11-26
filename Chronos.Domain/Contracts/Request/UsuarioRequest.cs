using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class UsuarioRequest
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
        public string? TogglId { get; set; }
    }
}
