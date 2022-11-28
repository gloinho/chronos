namespace Chronos.Domain.Entities;

public class Usuario_Projeto : BaseEntity
{
    public Usuario_Projeto()
    {
        Ativo = true;
    }

    public int UsuarioId { get; set; }
    public int ProjetoId { get; set; }
    public virtual Projeto Projeto { get; set; }
    public virtual Usuario Usuario { get; set; }
    public bool Ativo { get; set; }
    public virtual ICollection<Tarefa> Tarefas { get; set; }
}
