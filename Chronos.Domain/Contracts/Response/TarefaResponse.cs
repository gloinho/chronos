namespace Chronos.Domain.Contracts.Response
{
    public class TarefaResponse
    {
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public TimeSpan TotalHoras { get; set; }
    }
}
