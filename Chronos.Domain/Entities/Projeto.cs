namespace Chronos.Domain.Entities;

public class Projeto
{
    public int ProjetoId { get; set; }
    public string Nome { get; set; }
    public TimeSpan TotalHoras { get; set; }
    public ICollection<Usuario_Projeto> Usuarios { get; set; }
}
