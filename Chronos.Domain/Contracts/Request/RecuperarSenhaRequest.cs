

namespace Chronos.Domain.Contracts.Request
{
    public class RecuperarSenhaRequest
    {
        public string email { get; set;}
        public string Senha {get; set;}
        public string ConfirmacaoSenha {get; set;}
    }
}
