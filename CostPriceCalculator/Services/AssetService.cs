using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Data
{
    public static class AssetService
    {
        public static IReadOnlyList<Asset> GetAssets()
        {
            return new List<Asset>
            {
                new Asset {DateOfPurchase = new DateTime(2005, 1, 1), ShareCount = 100, PurchasePrice = 10 },
                new Asset {DateOfPurchase = new DateTime(2005, 2, 2), ShareCount = 40, PurchasePrice = 12 },
                new Asset {DateOfPurchase = new DateTime(2005, 3, 3), ShareCount = 50, PurchasePrice = 11 }
            };
        }
    }
}
