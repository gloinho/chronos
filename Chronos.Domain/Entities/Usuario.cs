using Chronos.Domain.Entities.Enums;

namespace Chronos.Domain.Entities;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public Permissao Permissao { get; set; }
    public bool Confirmado { get; set; }
    public ICollection<Usuario_Projeto> Projetos { get; set; }
}
