using Chronos.Domain.Entities.Enums;

namespace Chronos.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public Permissao Permissao { get; set; }
    public bool Confirmado { get; set; }
    public string? ConfirmacaoToken { get; set; } = null;
    public string? ResetSenhaToken { get; set; } = null;
    public virtual ICollection<Usuario_Projeto> Projetos { get; set; }
}
