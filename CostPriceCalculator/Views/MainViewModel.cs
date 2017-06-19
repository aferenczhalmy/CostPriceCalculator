using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CostPriceCalculator.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CostPriceCalculator.Views
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CalculateCmd = new RelayCommand(Calculate, () => NumberSharesSold != 0 && NumberSharesSold <= TotalSharesAvailableToSell && PricePerShare != 0 && SellDate != "" && SelectedCostMethod != null);
        }

        public int TotalSharesAvailableToSell
        {
            get { return _totalSharesAvailableToSell; }
            set {
                _totalSharesAvailableToSell = value;
                RaisePropertyChanged("TotalSharesAvailableToSell");
            }
        }

        public IReadOnlyList<Asset> Assets
        {
            get { return _assets; }
            set
            {
                _assets = value;
                RaisePropertyChanged("Assets");
                UpdateAvailableShares();
            }
        }

        public int NumberSharesSold
        {
            get { return _numberSharesSold; }
            set { _numberSharesSold = value; RaisePropertyChanged("NumberSharesSold"); CalculateCmd.RaiseCanExecuteChanged();}
        }

        public decimal PricePerShare
        {
            get { return _pricePerShare; }
            set { _pricePerShare = value; RaisePropertyChanged("PricePerShare"); CalculateCmd.RaiseCanExecuteChanged(); }
        }

        public string SellDate
        {
            get { return _sellDate; }
            set
            {
                _sellDate = value;
                UpdateAvailableShares();
                RaisePropertyChanged("SellDate");
                CalculateCmd.RaiseCanExecuteChanged();
            }
        }

        public List<string> CostMethod { get; set; } = new List<string> { "Fifo", "Lifo", "Highest Cost", "Lowest Cost", "Weighted Average" };

        public string SelectedCostMethod
        {
            get { return _selectedCostMethod; }
            set { _selectedCostMethod = value; RaisePropertyChanged("SelectedCostMethod"); CalculateCmd.RaiseCanExecuteChanged(); }
        }

        private decimal _costOfSoldShares;
        public decimal CostPriceOfSoldShares { get { return _costOfSoldShares; } set { _costOfSoldShares = value; RaisePropertyChanged(nameof(CostPriceOfSoldShares)); } }
        private decimal _gainLostOnSale;
        public decimal GainLostOnSale { get { return _gainLostOnSale; } set { _gainLostOnSale = value; RaisePropertyChanged(nameof(GainLostOnSale)); } }
        private int _numberOfRemainingShares;
        public int NumberOfRemainingShares { get { return _numberOfRemainingShares; } set { _numberOfRemainingShares = value; RaisePropertyChanged(nameof(NumberOfRemainingShares)); } }
        private decimal _costPriceOfRemainingShares;
        public decimal CostPriceOfRemainingShares { get { return _costPriceOfRemainingShares; } set { _costPriceOfRemainingShares = value; RaisePropertyChanged(nameof(CostPriceOfRemainingShares)); } }

        public RelayCommand CalculateCmd { get; set; }

        public Action CalculateAction;
        private int _numberSharesSold;
        private decimal _pricePerShare;
        private string _sellDate;
        private string _selectedCostMethod;
        private int _totalSharesAvailableToSell;
        private IReadOnlyList<Asset> _assets;

        public void UpdateAvailableShares()
        {
            TotalSharesAvailableToSell = Assets?.Where(a => a.DateOfPurchase <= DateTime.Parse(_sellDate)).Sum(a => a.ShareCount) ?? 0;
        }

        private void Calculate()
        {
            if(CalculateCmd.CanExecute(null))
                CalculateAction?.Invoke();
        }
    }
}
