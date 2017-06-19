using System;
using CostPriceCalculator;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CostPriceCalculator.Calculator.CostMethods;
using CostPriceCalculator.Models;

namespace CostPriceTests
{
    [TestClass]
    public class CalculateCostPriceTests
    {
        public List<Asset> Assets { get; set; }
        public Transaction Transaction { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Assets = new List<Asset>
            {
                new Asset {DateOfPurchase = new DateTime(2005, 1, 1), ShareCount = 100, PurchasePrice = 10 },
                new Asset {DateOfPurchase = new DateTime(2005, 2, 2), ShareCount = 40, PurchasePrice = 12 },
                new Asset {DateOfPurchase = new DateTime(2005, 3, 3), ShareCount = 50, PurchasePrice = 11 }
            };
            Transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
        }

        [TestMethod]
        public void FifoTestOneAsset()
        {
            var transaction = new Transaction { SharesSold = 100, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var fifo = new Fifo();
            var assets = Assets.Take(1).ToList();
            var costPrice = fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(10.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void FifoTestOneAssetSellMoreSharesThanOwn()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var fifo = new Fifo();
            var assets = Assets.Take(1).ToList();
            Action action = () => fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        }

        [TestMethod]
        public void FifoTestTwoAssets()
        {
        var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var fifo = new Fifo();
            var assets = Assets.Take(2).ToList();
            var costPrice = fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1240.0m/120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void FifoTest140SharesSoldTwoAssets()
        {
            var transaction = new Transaction { SharesSold = 140, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var fifo = new Fifo();
            var assets = Assets.Take(2).ToList(); ;
            var costPrice = fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1480.0m / 140.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void FifoTest120SharesSold()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 4, 3) };
            var fifo = new Fifo();
            var assets = Assets;
            var costPrice = fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1240.0m / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void FifoTest140SharesSoldSpecifyingDateInMiddleOfTransactions()
        {
            var transaction = new Transaction { SharesSold = 100, PricePerShare = 10.5m, Selldate = new DateTime(2005, 1, 3) };
            var fifo = new Fifo();
            var assets = Assets;
            var costPrice = fifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1000.0m / 100.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void LifoTest120SharesSold()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 4, 3) };
            var lifo = new Lifo();
            var assets = Assets;
            var costPrice = lifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1330.0m / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void LifoTest120SharesSoldSpecifyingDateInMiddleOfTransactions()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var lifo = new Lifo();
            var assets = Assets;
            var costPrice = lifo.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1280 / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void HighestCostTest120SharesSold()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 4, 3) };
            var highestCost = new HighestCost();
            var assets = Assets;
            var costPrice = highestCost.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1330.0m / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void HighestCostTest120SharesSoldSpecifyingDateInMiddleOfTransactions()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var highestCost = new HighestCost();
            var assets = Assets;
            var costPrice = highestCost.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1280 / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void LowestCostTest120SharesSold()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 4, 3) };
            var lowestCost = new LowestCost();
            var assets = Assets;
            var costPrice = lowestCost.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1220.0m / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void LowestCostTest120SharesSoldSpecifyingDateInMiddleOfTransactions()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var lowestCost = new LowestCost();
            var assets = Assets;
            var costPrice = lowestCost.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1240.0m / 120.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void WeightedAveragetTest120SharesSold()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 4, 3) };
            var weightedAverage = new WeightedAverage();
            var assets = Assets;
            var costPrice = weightedAverage.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(2030.0m / 190.0m, costPrice.CostPriceSold);
        }

        [TestMethod]
        public void WeightedAveragetTest120SharesSoldSpecifyingDateInMiddleOfTransactions()
        {
            var transaction = new Transaction { SharesSold = 120, PricePerShare = 10.5m, Selldate = new DateTime(2005, 2, 3) };
            var weightedAverage = new WeightedAverage();
            var assets = Assets;
            var costPrice = weightedAverage.CostPriceOfSharesSoldAndSharesRemaining(transaction, assets);

            Assert.AreEqual(1480.0m / 140, costPrice.CostPriceSold);
        }
    }
}
