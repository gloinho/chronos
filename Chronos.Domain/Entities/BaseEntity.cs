namespace Chronos.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
