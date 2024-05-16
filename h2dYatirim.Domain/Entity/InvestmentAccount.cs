using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace h2dYatirim.Domain.Entity
{
    public class InvestmentAccount:IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal AmountInAccount { get; set; }
        public decimal ProtfolioValue { get; set; }
    }
}
