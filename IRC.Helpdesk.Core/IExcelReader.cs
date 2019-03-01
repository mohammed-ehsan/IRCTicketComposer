using IRC.Helpdesk.Core.POCOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{
    public interface IAssetSource : IDisposable
    {
        void SetSource(Stream fileStream);
        void Configure(IAssetSourceConfiguration Config);
        IEnumerable<AssetTicket> ReadAssets();
        IEnumerable<AssetTicket> ReadAssets(string text);
    }
}
