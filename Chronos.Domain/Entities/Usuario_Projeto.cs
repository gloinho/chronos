namespace Chronos.Domain.Entities;

public class Usuario_Projeto : BaseEntity
{
    public int UsuarioId { get; set; }
    public int ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; }
}
