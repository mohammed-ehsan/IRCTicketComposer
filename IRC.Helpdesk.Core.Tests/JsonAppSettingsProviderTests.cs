using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace IRC.Helpdesk.Core.Tests
{
    public class JsonAppSettingsProviderTests
    {
        [SetUp]
        public void SetUp()
        { 
        }

        [Test]
        public void GetSettings_CreateNewAndCheckSettings()
        {
            //Arrange
            string jsonPath = Environment.CurrentDirectory + "\\TestFiles\\AppSettings.json";

            //Act
            var settingsProvider = new JsonSettingsProvider(jsonPath);
            var settings = settingsProvider.GetSettings();

            //Assert
            Assert.AreEqual("Helpdesk@rescue.org", settings.To);
            
        }
    }
}
