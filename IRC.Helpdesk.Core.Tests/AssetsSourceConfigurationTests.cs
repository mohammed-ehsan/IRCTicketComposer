using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace IRC.Helpdesk.Core.Tests
{
    public class AssetsSourceConfigurationTests
    {
        [Test]
        public void SaveChanges_Successfull()
        {
            //Arrange
            string jsonPath = Environment.CurrentDirectory + "\\TestFiles\\AssetsConfigFile.json";
            var config = new AssetsSourceConfiguration(jsonPath);
            var oldConfig = config.GetJsonObject();
            var newJsonConfig = new JsonConfiguration
            {
                FirstRow = 10,
                MakeColumn = "AA",
                ModelColumn = "AB",
                InventoryNumberColumn = "AC",
                UserColumn = "AD",
                DeliveryDateColumn = "AF"
            };

            //Act
            config.Update(newJsonConfig);
            config.SaveChanges();
            var updatedConfig = new AssetsSourceConfiguration(jsonPath);
            var updatedJsonConfig = updatedConfig.GetJsonObject();

            updatedConfig.Update(oldConfig);
            updatedConfig.SaveChanges();
            
            //Assert
            Assert.AreEqual(newJsonConfig.FirstRow, updatedJsonConfig.FirstRow);
            Assert.AreEqual(newJsonConfig.MakeColumn, updatedJsonConfig.MakeColumn);
            Assert.AreEqual(newJsonConfig.ModelColumn, updatedJsonConfig.ModelColumn);
            Assert.AreEqual(newJsonConfig.InventoryNumberColumn, updatedJsonConfig.InventoryNumberColumn);
            Assert.AreEqual(newJsonConfig.UserColumn, updatedJsonConfig.UserColumn);
            Assert.AreEqual(newJsonConfig.DeliveryDateColumn, updatedJsonConfig.DeliveryDateColumn);


        }
    }
}
