using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CostPriceCalculator.Calculator;
using CostPriceCalculator.Calculator.CostMethods;
using CostPriceCalculator.Data;
using CostPriceCalculator.Models;
using CostPriceCalculator.Views;

namespace CostPriceCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            base.OnStartup(e);

            Dictionary<string, ICostMethod> costMethods =
                new Dictionary<string, ICostMethod>
                {
                    {"Fifo", new Fifo()},
                    {"Lifo", new Lifo()},
                    {"Highest Cost", new HighestCost()},
                    {"Lowest Cost", new LowestCost()},
                    {"Weighted Average", new WeightedAverage()}
                };

            var calculator = new CostPriceMethodCalculator(costMethods);
            var mainController = new MainController(calculator, AssetService.GetAssets);

            var mainWindow = new MainWindow() { DataContext = mainController.GetViewModel() };
            mainWindow.Show();

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
