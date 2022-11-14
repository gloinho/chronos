namespace Chronos.Domain.Entities;

public class Projeto : BaseEntity
{
    public string Nome { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public virtual ICollection<Usuario_Projeto> Usuarios { get; set; }
}
