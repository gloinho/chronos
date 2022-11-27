using System.Text.Json.Serialization;

namespace Chronos.Domain.Contracts.Response
{
    public class TogglDetailedResponse
    {
        public List<string> Mensagens { get; set; }

        [JsonPropertyName("data")]
        public Data[] Data { get; set; }
    }
    public class Data
    {
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        [JsonPropertyName("pid")]
        public ulong? Pid { get; set; }

        [JsonPropertyName("tid")]
        public ulong? Tid { get; set; }

        [JsonPropertyName("uid")]
        public ulong Uid { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("dur")]
        public int Dur { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("use_stop")]
        public bool UseStop { get; set; }

        [JsonPropertyName("client")]
        public string Client { get; set; }

        [JsonPropertyName("project")]
        public string Project { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; }

        [JsonPropertyName("billable")]
        public float Billable { get; set; }

        [JsonPropertyName("is_billable")]
        public bool IsBillable { get; set; }

        [JsonPropertyName("cur")]
        public string Cur { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
    }

}
