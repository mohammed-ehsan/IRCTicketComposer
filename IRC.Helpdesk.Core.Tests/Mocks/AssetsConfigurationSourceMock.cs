using System;
using System.Collections.Generic;
using System.Text;

namespace IRC.Helpdesk.Core.Tests
{
    public class AssetsConfigurationSourceMock : IAssetSourceConfiguration
    {
        public int FirstRow { get; set; }
        public int MakeIndex { get; set; }
        public int ModelIndex { get; set; }
        public int InventoryNumberIndex { get; set; }
        public int UserIndex { get; set; }
        public int DelivaryDateIndex { get; set; }
    }
}
