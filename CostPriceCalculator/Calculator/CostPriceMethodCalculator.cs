using System.Collections.Generic;
using CostPriceCalculator.Models;

namespace CostPriceCalculator.Calculator
{
    public class CostPriceMethodCalculator
    {
        Dictionary<string, ICostMethod> _costMethods;

        public CostPriceMethodCalculator(Dictionary<string, ICostMethod> costMethods)
        {
            _costMethods = costMethods;
        }

        public CostPriceResult CalculateCostPrice(string costMethod, Transaction transaction, IReadOnlyList<Asset> assets)
        {
            return _costMethods[costMethod].CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);
        }
    }
}
