

using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class TarefaRequest
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        public int ProjetoId { get; set; }
    }
}
