using System;
using System.Collections.Generic;
using System.Text;

namespace IRC.Helpdesk.Core.Tests
{
    public class AssetsConfigurationSourceMock : IAssetSourceConfiguration
    {
        public int FirstRow { get; set; }
        public int InventoryNumberIndex { get; set; }
        public int SerialNumberIndex { get; set; }
        public int LocationIndex { get; set; }
        public int SubLocationIndex { get; set; }
        public int MainCategoryIndex { get; set; }
        public int SubCategoryIndex { get; set; }
    }
}
