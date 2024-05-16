using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace h2dYatirim.Domain.Entity
{
    public class ShareCertificate
    {
        public string Currency { get; set; }
        public string Name { get; set; }
        public string PriceStr { get; set; }
        public decimal Price { get; set; }
        public decimal Rate { get; set; }
        public string HacimLot { get; set; }
        public string HacimTl { get; set; }
        public string Time { get; set; }
    }

    
}
