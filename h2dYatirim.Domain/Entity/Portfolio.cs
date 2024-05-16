using Core.Entities;

namespace h2dYatırım.Entities
{
    public class Portfolio:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InvestmentAccountId { get; set; }
        public string ShareCertificateId { get; set; }
        public double Amount { get; set; }
        public decimal ValueChange { get; set; }
        public decimal ReceivedValue { get; set; }
        public decimal CurrentValue { get; set; }
    }
}
