namespace Chronos.Domain.Contracts.Response
{
    public class Usuario_ProjetoResponse
    {
        public string NomeDoProjeto { get; set; }
        public ICollection<TarefaResponse> Tarefas { get; set; }
    }
}