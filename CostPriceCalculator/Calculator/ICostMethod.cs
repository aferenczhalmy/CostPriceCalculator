using System.Collections.Generic;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Calculator
{
    public interface ICostMethod
    {
        CostPriceResult CostPriceOfSharesSoldAndSharesRemaining(Transaction transaction, IReadOnlyList<Asset> allAssets);
    }
}
