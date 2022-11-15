namespace Chronos.Domain.Entities;

public class Tarefa : BaseEntity
{
    public int Usuario_ProjetoId { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; } = null;
    public TimeSpan TotalHoras { get; set; }
    public virtual Usuario_Projeto Usuario_Projeto { get; set; }
}
