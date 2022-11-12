using System.ComponentModel.DataAnnotations;

u
namespace Chronos.Domain.Contracts.Request
{
    public class TarefaStartRequest
    {
        [Required]
        public int TarefaId { get; set; }
    }
}
