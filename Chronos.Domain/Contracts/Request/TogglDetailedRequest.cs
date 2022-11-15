namespace Chronos.Domain.Contracts.Request
{
    public class TogglDetailedRequest
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
