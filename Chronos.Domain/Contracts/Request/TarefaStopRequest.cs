using System.ComponentModel.DataAnnotations;

s;

namespace Chronos.Domain.Contracts.Request
{
    public class TarefaStopRequest
    {
        [Required]
        public int Tarefaid { get; set; }
    }
}
