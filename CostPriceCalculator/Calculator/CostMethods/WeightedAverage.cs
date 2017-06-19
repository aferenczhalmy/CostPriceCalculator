using System.Collections.Generic;
using System.Linq;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Calculator.CostMethods
{
    public class WeightedAverage : CostMethodBase
    {
        public override CostPriceResult CostPriceOfSharesSoldAndSharesRemaining(Transaction transaction, IReadOnlyList<Asset> assets)
        {
            ValidateTransactionDate(transaction.Selldate, assets);

            var costOfShares = assets.Where(a => a.DateOfPurchase <= transaction.Selldate).Sum(a => a.ShareCount * a.PurchasePrice);
            var totalShares = assets.Where(a => a.DateOfPurchase <= transaction.Selldate).Sum(a => a.ShareCount);

            var costPrice = costOfShares / totalShares;

            return new CostPriceResult { CostPriceSold = costPrice, CostPriceRemain = costPrice };
        }
    }
}
