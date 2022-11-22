namespace Chronos.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DataInclusao = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
