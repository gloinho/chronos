using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class RecuperarSenhaRequest
    {
        [Required]
        public string Email { get; set;}

        [Required]
        public string Senha {get; set;}

        [Required]
        public string ConfirmacaoSenha {get; set;}
    }
}
