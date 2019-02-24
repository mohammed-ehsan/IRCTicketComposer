using System;
using Newtonsoft.Json;
using System.IO;

namespace IRC.Helpdesk.Core
{
    public class AssetsSourceConfiguration : IAssetSourceConfiguration
    {
        #region Public Properties
        
        /// <summary>
        /// Index of first row.
        /// </summary>
        public int FirstRow { get; set; }

        /// <summary>
        /// Index of category column.
        /// </summary>
        public int MainCategoryIndex { get; set; }

        /// <summary>
        /// Index of sub category column.
        /// </summary>
        public int SubCategoryIndex { get; set; }

        /// <summary>
        /// Inventory number column index.
        /// </summary>
        public int InventoryNumberIndex { get; set; }

        /// <summary>
        /// Serialnumber column index.
        /// </summary>
        public int SerialNumberIndex { get; set; }

        /// <summary>
        /// Setup location index.
        /// </summary>
        public int LocationIndex { get; set; }

        /// <summary>
        /// Setup sub location index.
        /// </summary>
        public int SubLocationIndex { get; set; }

        #endregion

        #region Public Methods

        public void Configure(string jsonPath)
        {
            try
            {
                var serializer = new JsonSerializer();
                var jsonConfig = serializer.Deserialize<JsonConfiguration>(new JsonTextReader(File.OpenText(jsonPath)));
                Fill(jsonConfig);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Translate(string columnId)
        {
            int value = 0;
            var input = columnId.ToLowerInvariant();

            for (int i = 0; i < input.Length; i++)
            {
                int oridenalPower = (int)Math.Pow(26.0, ((input.Length - 1) - i));
                if (!Char.IsLetter(input[i]))
                    continue;
                value += ((int)input[i]-((int)'a'-1)) * oridenalPower;
            }

            return value;
        }

        #endregion

        #region Private Methods

        private void Fill(JsonConfiguration config)
        {
            this.FirstRow = config.FirstRow;
            this.MainCategoryIndex = Translate(config.MainCategoryColumn);
            this.SubCategoryIndex = Translate(config.SubCategoryColumn);
            this.InventoryNumberIndex = Translate(config.InventoryNumberColumn);
            this.SerialNumberIndex = Translate(config.SerialNumberColumn);
            this.LocationIndex = Translate(config.LocationColumn);
            this.SubLocationIndex = Translate(config.SubLocationColumn);
        }

        #endregion
    }
}
