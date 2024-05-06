using Core.Entities;

namespace h2dYatırım.Entities
{
    public class AccountMovement:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CryptoAccountId { get; set; }
        public string CryptoId { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }

    }
}
