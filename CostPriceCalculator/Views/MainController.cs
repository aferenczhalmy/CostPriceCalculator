using System;
using System.Collections.Generic;
using System.Linq;
using CostPriceCalculator.Calculator;
using CostPriceCalculator.Models;
using GalaSoft.MvvmLight;

namespace CostPriceCalculator.Views
{
    public class MainController
    {
        public MainViewModel MainViewModel { get; set; }
        public CostPriceMethodCalculator CostPriceMethodCalculator { get; set; }

        public MainController(CostPriceMethodCalculator costPriceMethodCalculator, Func<IReadOnlyList<Asset>> assetService)
        {
            CostPriceMethodCalculator = costPriceMethodCalculator;
            const string sellDate = "2/3/2005";
            MainViewModel = new MainViewModel
            {
                SellDate = sellDate,
                CalculateAction = () => Calculate(),
                Assets = assetService()
            };
        }

        public ViewModelBase GetViewModel()
        {
            return MainViewModel;
        }

        private void Calculate()
        {
            var transaction = new Transaction { PricePerShare = MainViewModel.PricePerShare, SharesSold = MainViewModel.NumberSharesSold, Selldate = DateTime.Parse(MainViewModel.SellDate) };

            var result = CostPriceMethodCalculator.CalculateCostPrice(MainViewModel.SelectedCostMethod, transaction, MainViewModel.Assets);

            MainViewModel.CostPriceOfSoldShares = result.CostPriceSold;
            MainViewModel.CostPriceOfRemainingShares = result.CostPriceRemain;
            MainViewModel.GainLostOnSale = (MainViewModel.PricePerShare - result.CostPriceSold) * MainViewModel.NumberSharesSold;
            MainViewModel.NumberOfRemainingShares = MainViewModel.Assets.Sum(a => a.ShareCount) - MainViewModel.NumberSharesSold;
        }
    }
}
