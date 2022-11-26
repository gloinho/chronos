namespace Chronos.Domain.Contracts.Response
{
    public class TogglDetailedResponse
    {
        //public ulong total_grand { get; set; }
        //public ulong total_billable { get; set; }
        //public ulong total_count { get; set; }
        //public ulong per_page { get; set; }
        //public Total_Currencies[] total_currencies { get; set; }

        public List<string> Mensagens { get; set; }
        public Data[] data { get; set; }
    }

    //public class Total_Currencies
    //{
    //    public string currency { get; set; }
    //    public float amount { get; set; }
    //}

    public class Data
    {
        public ulong id { get; set; }
        public ulong? pid { get; set; }
        public ulong? tid { get; set; }
        public ulong uid { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public DateTime updated { get; set; }
        public int dur { get; set; }
        public string user { get; set; }
        public bool use_stop { get; set; }
        public string client { get; set; }
        public string project { get; set; }
        public string task { get; set; }
        public float billable { get; set; }
        public bool is_billable { get; set; }
        public string cur { get; set; }
        public string[] tags { get; set; }
    }

}
