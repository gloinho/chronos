

namespace Chronos.Domain.Entities
{
    public class Log 
    {
        public Log()
        {
            DataAlteracao = DateTime.Now;
        }
        
        public int Id { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int? ResponsavelId { get; set; }
        public Usuario Responsavel { get; set; }
        public string Alteracao { get; set; }
    }
}
