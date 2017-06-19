using System;

namespace CostPriceCalculator.Models
{
    public class Transaction
    {
        public int SharesSold { get; set; }
        public decimal PricePerShare { get; set; }
        public DateTime Selldate { get; set; }
    }
}
