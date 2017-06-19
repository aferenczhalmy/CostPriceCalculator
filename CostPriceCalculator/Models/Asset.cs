using System;

namespace CostPriceCalculator.Models
{
    public class Asset
    {
        public DateTime DateOfPurchase { get; set; }
        public int ShareCount { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}