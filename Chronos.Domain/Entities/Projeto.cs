namespace Chronos.Domain.Entities;

public class Projeto
{
    public int ProjetoId { get; set; }
    public string Nome { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public ICollection<Usuario_Projeto> Usuarios { get; set; }
}
