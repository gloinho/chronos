namespace Chronos.Domain.Contracts.Response
{
    public class Usuario_ProjetoResponse
    {
        public string NomeDoUsuario { get; set; }
        public ICollection<TarefaResponse> Tarefas { get; set; }
    }
}
