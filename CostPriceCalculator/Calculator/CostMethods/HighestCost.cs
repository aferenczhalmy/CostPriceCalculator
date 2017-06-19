using System.Collections.Generic;
using System.Linq;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Calculator.CostMethods
{
    public class HighestCost : CostMethodBase
    {
        public override CostPriceResult CostPriceOfSharesSoldAndSharesRemaining(Transaction transaction, IReadOnlyList<Asset> assets)
        {
            ValidateTransactionDate(transaction.Selldate, assets);

            var sortedAssets = assets.Where(a => a.DateOfPurchase <= transaction.Selldate).OrderByDescending(a => a.PurchasePrice).ToList();

            return CostPriceOfSharesSoldAndSharesRemaining(transaction.SharesSold, sortedAssets, assets);
        }
    }
}
