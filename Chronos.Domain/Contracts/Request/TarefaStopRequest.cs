using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class TarefaStopRequest
    {
        [Required]
        public int TarefaId { get; set; }
    }
}
