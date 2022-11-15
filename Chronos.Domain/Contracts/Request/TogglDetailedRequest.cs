namespace Chronos.Domain.Contracts.Request
{
    public class TogglDetailedRequest
    {
        public string TokenToggl { get; set; }
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
