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
        public int LocationIndex { get; set; }

        public JsonConfiguration GetJsonObject()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(JsonConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
