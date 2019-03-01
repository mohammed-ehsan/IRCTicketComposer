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
            Config = new AssetsConfigurationSourceMock { FirstRow = 2, MakeIndex = 3, ModelIndex = 2, InventoryNumberIndex=3, UserIndex=4, DelivaryDateIndex=5 };
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
            Assert.AreEqual(configuration.MakeIndex, 1);
            Assert.AreEqual(configuration.ModelIndex, 2);
            Assert.AreEqual(configuration.InventoryNumberIndex, 3);
            Assert.AreEqual(configuration.UserIndex, 4);
            Assert.AreEqual(configuration.DelivaryDateIndex, 5);
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

        /// <summary>
        /// Test reading assets from excel text copied row.
        /// </summary>
        /// <param name="input"></param>
        [TestCase(@"helpdesk@rescue.org	18-Aug-09	Office Equipment	 Camera	Sony	Nil Digital 	BFUM10302073	46171	518		270.00	OFDA	GO238	Erbil Office	Ops Store		S		5IQ/ERB/AR#0304							
")]
        [Test]
        public void ReadAssetsTest(string input)
        {
            var reader = new ExcelAssetReader();

            reader.Configure(this.Config);
            var result = reader.ReadAssets(input).ToArray();

            Assert.AreEqual(result[0].Model, "18-Aug-09");
        }

        /// <summary>
        /// Test reading a string containing multiple assets.
        /// </summary>
        [Test]
        public void ReadAssets_MultipleAssetsText()
        {
            var data = File.ReadAllText(Environment.CurrentDirectory + "\\TestFiles\\MultilineAssetsTestText.txt");
            var reader = new ExcelAssetReader();
            reader.Configure(this.Config);
            var assets = reader.ReadAssets(data);
            Assert.Greater(assets.Count(), 0);
        }
    }
}