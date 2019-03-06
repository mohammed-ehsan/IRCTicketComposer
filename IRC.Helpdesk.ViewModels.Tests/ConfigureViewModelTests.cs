using IRC.Helpdesk.Core;
using IRC.Helpdesk.ViewModels;
using ME.Wpf.Mvvm;
using Moq;
using NUnit.Framework;
using System;

namespace Tests
{
    public class ConfigureDialogViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Update_SaveFailed_NoException()
        {
            //Arrange
            var dialogMock = new Mock<IDialogService>();
            dialogMock.Setup(t => t.ShowDialog(It.IsAny<IClosable>())).Returns(true);

            var assetsSourceConfig = new Mock<IAssetSourceConfiguration>();
            assetsSourceConfig.Setup(a => a.SaveChanges()).Throws(new Exception());
            assetsSourceConfig.Setup(a => a.GetJsonObject()).Returns(
                new JsonConfiguration
                {
                    FirstRow = 1,
                    MakeColumn = "A",
                    ModelColumn = "B",
                    InventoryNumberColumn = "C",
                    UserColumn = "D",
                    LocationColumn = "E"
                }
                );
            var viewModel = new ConfigureDialogViewModel(assetsSourceConfig.Object, dialogMock.Object);

            //Act, Asserts
            Assert.DoesNotThrow(() => viewModel.Update());
        }
    }
}