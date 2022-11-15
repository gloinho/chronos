namespace Chronos.Domain.Contracts.Response
{
    public class TarefaResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public TimeSpan TotalHoras { get; set; }
        public Usuario_ProjetoResponse Usuario_Projeto { get; set; }
    }
}
