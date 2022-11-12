

namespace Chronos.Domain.Contracts.Response
{
    public class TarefaResponse
    {
        public string NomeDoProjeto {get; set;}
        public DateTime DataFinal {get; set;}
        public DateTime DataInicial {get; set;}
        public string Descricao {get; set;}
        public TimeSpan TotalDeHoras {get; set;}
       
    }
}
