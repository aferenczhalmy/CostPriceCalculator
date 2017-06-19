using System;
using System.Collections.Generic;
using System.Linq;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Calculator.CostMethods
{
    public abstract class CostMethodBase : ICostMethod
    {
        public abstract CostPriceResult CostPriceOfSharesSoldAndSharesRemaining(Transaction transaction, IReadOnlyList<Asset> allAssets);

        public CostPriceResult CostPriceOfSharesSoldAndSharesRemaining(int sharesSold, IReadOnlyList<Asset> sortedAssets, IReadOnlyList<Asset> allAssets)
        {
            var costPriceSold = CalculateCostPrice(sharesSold, sortedAssets);
            var costPriceRemain = CalculateCostPrice(allAssets.Sum(a => a.ShareCount) - sharesSold, allAssets.Reverse().ToList());

            return new CostPriceResult { CostPriceSold = costPriceSold, CostPriceRemain = costPriceRemain };
        }

        private decimal CalculateCostPrice(int sharesSold, IReadOnlyList<Asset> assets)
        {
            if (sharesSold > assets.Sum(a => a.ShareCount))
                throw new ArgumentOutOfRangeException("Shares Sold", sharesSold, "You can't sell more shares than you currently own");

            decimal costPrice = 0;
            decimal costOfShares = 0;
            decimal sharesRemaining = sharesSold;

            if (sharesRemaining == 0)
                return 0;

            var index = 0;
            do
            {
                if (sharesRemaining > assets[index].ShareCount)
                {
                    sharesRemaining -= assets[index].ShareCount;
                    costOfShares += assets[index].ShareCount * assets[index].PurchasePrice;
                }
                else
                {
                    costOfShares += sharesRemaining * assets[index].PurchasePrice;
                    sharesRemaining = 0;
                }

                index++;

            } while (sharesRemaining > 0);

            costPrice = costOfShares / sharesSold;

            return costPrice;
        }

        public void ValidateTransactionDate(DateTime SellDate, IReadOnlyList<Asset> assets)
        {
            if (assets.All(a => SellDate <= a.DateOfPurchase))
                throw new ArgumentOutOfRangeException("Transaction.SellDate", SellDate, "Can't have transaction dates before purchse dates");
        }
    }
}
