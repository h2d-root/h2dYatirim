using Core.Entities;

namespace h2dYatırım.Entities
{
    public class Wallet : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CryptoAccountId { get; set; }
        public string CryptoId { get; set; }
        public double Amount { get; set; }
        public decimal ValueChange { get; set; }
        public decimal ReceivedValue { get; set; }
        public decimal CurrentValue { get; set; }
    }
}
