namespace Chronos.Domain.Entities;

public class Tarefa : BaseEntity
{
    public int Usuario_ProjetoId { get; set; }
    public DateTime DataInicial { get; set; } = DateTime.Now;
    public DateTime? DataFinal { get; set; } = null;
<<<<<<< HEAD
    public string? TogglId { get; set; }
    public virtual Usuario_Projeto Usuario_Projeto { get; set; }
=======
    public TimeSpan TotalHoras { get; set; }
    public Usuario_Projeto Usuario_Projeto { get; set; }
>>>>>>> origin/main
}
