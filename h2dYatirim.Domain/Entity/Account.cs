using Core.Entities;
using h2dYatırım.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace h2dYatirim.Domain.Entity
{
    public class Account:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public CryptoAccount? CryptoAccount { get; set; }
        public InvestmentAccount? InvestmentAccount { get; set; }
        public decimal AmountInAccount { get; set; }
        public decimal AssetValue { get; set; }
    }
}
