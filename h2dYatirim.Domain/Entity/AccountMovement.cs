using Core.Entities;

namespace h2dYatırım.Entities
{
    public class AccountMovement:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string AssetId { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }

    }
}
