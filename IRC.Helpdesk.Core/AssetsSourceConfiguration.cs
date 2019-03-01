using System;
using Newtonsoft.Json;
using System.IO;

namespace IRC.Helpdesk.Core
{
    public class AssetsSourceConfiguration : IAssetSourceConfiguration
    {
        #region Public Properties

        public int FirstRow { get; set; }
        public int MakeIndex { get; set; }
        public int ModelIndex { get; set; }
        public int InventoryNumberIndex { get; set; }
        public int UserIndex { get; set; }
        public int DelivaryDateIndex { get; set; }


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
            this.MakeIndex = Translate(config.MakeColumn);
            this.ModelIndex = Translate(config.ModelColumn);
            this.InventoryNumberIndex = Translate(config.InventoryNumberColumn);
            this.UserIndex = Translate(config.UserColumn);
            this.DelivaryDateIndex = Translate(config.DelivaryDateColumn);
        }

        #endregion
    }
}
