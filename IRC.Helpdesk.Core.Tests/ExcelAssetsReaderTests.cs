using IRC.Helpdesk.Core;
using IRC.Helpdesk.Core.POCOs;
using IRC.Helpdesk.Core.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Tests for <see cref="ExcelAssetReader"/> Class
    /// </summary>
    public class ExcelAssetsReaderTests
    {
        /// <summary>
        /// Mocking assets source configuration provider.
        /// </summary>
        public IAssetSourceConfiguration Config;

        [SetUp]
        public void Setup()
        {
            Config = new AssetsConfigurationSourceMock { FirstRow = 2, MainCategoryIndex = 1, SubCategoryIndex = 2, InventoryNumberIndex=3, LocationIndex=4, SerialNumberIndex=5, SubLocationIndex=6 };
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Test if <see cref="ExcelAssetReader.ReadAssets"/> works.
        /// </summary>
        [Test]
        public void ReadAssetsTest()
        {
            var assetsReader = new ExcelAssetReader();
            assetsReader.Configure(this.Config);
            IEnumerable<AssetTicket> tickets = null;
            using (Stream assetsFile = new FileStream(Environment.CurrentDirectory + "\\TestFiles\\Computer Info.xlsx", FileMode.Open))
            {
                assetsReader.SetSource(assetsFile);
                tickets = assetsReader.ReadAssets();
            }
            Assert.IsNotNull(tickets);
            Assert.Greater(tickets.Count(), 0);
        }

        /// <summary>
        /// Test if <see cref="AssetsSourceConfiguration"/> is working properly.
        /// </summary>
        [Test]
        public void ReadJsonConfiguration()
        {
            var configuration = new AssetsSourceConfiguration();
            configuration.Configure(Environment.CurrentDirectory + "\\TestFiles\\AssetsConfigFile.json");

            Assert.AreEqual(configuration.FirstRow, 2);
            Assert.AreEqual(configuration.InventoryNumberIndex, 2);
            Assert.AreEqual(configuration.SerialNumberIndex, 3);
            Assert.AreEqual(configuration.LocationIndex, 4);
            Assert.AreEqual(configuration.SubLocationIndex, 5);
            Assert.AreEqual(configuration.MainCategoryIndex, 6);
            Assert.AreEqual(configuration.SubCategoryIndex, 7);
        }

        [TestCase("A", ExpectedResult =1)]
        [TestCase("AA", ExpectedResult =27)]
        [Test]
        public int TransalateStringToNumberTest(string columnId)
        {
            //Preparing
            var configuration = new AssetsSourceConfiguration();
            return configuration.Translate(columnId);
            
        }
    }
}