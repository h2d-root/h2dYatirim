using Core.Entities;

namespace h2dYatırım.Entities
{
    public class CryptoAccount:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal AmountInAccount { get; set; }
        public decimal ProtfolioValue { get; set; }
    }
}
