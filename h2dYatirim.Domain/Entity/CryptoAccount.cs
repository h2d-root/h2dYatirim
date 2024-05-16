using Core.Entities;

namespace h2dYatırım.Entities
{
    public class CryptoAccount:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal WalletValue { get; set; }
    }
}
