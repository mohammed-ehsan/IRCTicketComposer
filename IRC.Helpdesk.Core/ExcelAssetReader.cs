using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExcelDataReader;
using IRC.Helpdesk.Core.POCOs;

namespace IRC.Helpdesk.Core
{
    public class ExcelAssetReader : IAssetSource
    {
        private IAssetSourceConfiguration configuration;

        #region Public Properties 

        public DataSet Data { get; set; }

        #endregion
        
        #region Public Methods

        public void Configure(IAssetSourceConfiguration Config)
        {
            this.configuration = Config;
        }

        public IEnumerable<AssetTicket> ReadAssets()
        {
            var table = Data.Tables[0];
            List<AssetTicket> tickets = new List<AssetTicket>();
            int currentRow = 1;
            foreach (DataRow item in table.Rows)
            {
                if (currentRow < this.configuration.FirstRow)
                {
                    currentRow++;
                    continue;
                }

                var asset = CreateTicket(item.ItemArray.ToStringArray());
                tickets.Add(asset);
                currentRow++;
            }
            return tickets;
        }

        /// <summary>
        /// Read the assets from provided string.
        /// </summary>
        /// <param name="text">String contraining the assets text</param>
        /// <returns></returns>
        public IEnumerable<AssetTicket> ReadAssets(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            var rows = text.Split(new string[] { "\r\n" },StringSplitOptions.None);
            var assets = new List<AssetTicket>();
            foreach (var row in rows)
            {
                var data = row.Split('\t');
                assets.Add(CreateTicket(data));
            }
            return assets;
        }

        public void SetSource(Stream fileStream)
        {
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream))
            {
                Data = reader.AsDataSet();
            }
        }

        #endregion

        #region Private helpers

        private AssetTicket CreateTicket(string[] data)
        {
            if (data.Length == 0)
                return null;
            
            
            return new AssetTicket()
            {
                InventoryNumber = data.TryGetValue(this.configuration.InventoryNumberIndex - 1).ToString(),
                SerialNumber = data.TryGetValue(this.configuration.SerialNumberIndex - 1).ToString(),
                Location = data.TryGetValue(this.configuration.LocationIndex - 1).ToString(),
                SubLocation = data.TryGetValue(this.configuration.SubLocationIndex - 1).ToString(),
                MainCategory = data.TryGetValue(this.configuration.MainCategoryIndex - 1).ToString(),
                SubCategory = data.TryGetValue(this.configuration.SubCategoryIndex - 1).ToString()
            };
        }


        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.Data.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this.Data = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ExcelAssetReader() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
