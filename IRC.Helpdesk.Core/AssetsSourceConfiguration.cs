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

        #region Private Fields

        /// <summary>
        /// json configuration file path.
        /// </summary>
        private string _jsonPath;

        /// <summary>
        /// Internal json configuration object.
        /// </summary>
        private JsonConfiguration _jsonConfiguration;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor. Creates new <see cref="AssetsSourceConfiguration"/> provided json file path.
        /// </summary>
        /// <param name="jsonPath"></param>
        public AssetsSourceConfiguration(string jsonPath)
        {
            this._jsonPath = jsonPath;
            try
            {
                var serializer = new JsonSerializer();
                using (var jsonStreamReader = File.OpenText(jsonPath))
                {
                    this._jsonConfiguration = serializer.Deserialize<JsonConfiguration>(new JsonTextReader(jsonStreamReader));
                }
                Fill(this._jsonConfiguration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the indecies of this configuration source by the provided <see cref="JsonConfiguration"/>.
        /// </summary>
        /// <param name="configuration">Updated json configuration object.</param>
        public void Update(JsonConfiguration configuration)
        {
            this._jsonConfiguration = configuration;
            Fill(this._jsonConfiguration);
        }

        /// <summary>
        /// Saves the updated configuration values to the json file.
        /// </summary>
        public void SaveChanges()
        {
            var serializer = new JsonSerializer();
            using (var jsonFile = new StreamWriter(this._jsonPath))
            {
                serializer.Serialize(jsonFile, this._jsonConfiguration);
            }
        }

        /// <summary>
        /// Retreive the undelying json object.
        /// </summary>
        /// <returns></returns>
        public JsonConfiguration GetJsonObject() => this._jsonConfiguration;

        /// <summary>
        /// Converts column name string into zero based numerical index.
        /// </summary>
        /// <param name="columnId">Column name in excel sheet</param>
        /// <returns></returns>
        public int Convert(string columnId)
        {
            int value = 0;
            var input = columnId.ToLowerInvariant();

            for (int i = 0; i < input.Length; i++)
            {
                int oridenalPower = (int)Math.Pow(26.0, ((input.Length - 1) - i));
                if (!Char.IsLetter(input[i]))
                    continue;
                value += ((int)input[i] - ((int)'a' - 1)) * oridenalPower;
            }

            return value;
        }
        #endregion


        #region Private Methods

        

        /// <summary>
        /// Fill the public properties of this object.
        /// </summary>
        /// <param name="config"></param>
        private void Fill(JsonConfiguration config)
        {
            this.FirstRow = config.FirstRow;
            this.MakeIndex = Convert(config.MakeColumn);
            this.ModelIndex = Convert(config.ModelColumn);
            this.InventoryNumberIndex = Convert(config.InventoryNumberColumn);
            this.UserIndex = Convert(config.UserColumn);
            this.DelivaryDateIndex = Convert(config.DeliveryDateColumn);
        }

        #endregion
    }
}
