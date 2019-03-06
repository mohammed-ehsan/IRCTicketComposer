using System.IO;
using Newtonsoft.Json;

namespace IRC.Helpdesk.Core
{
    /// <summary>
    /// Provides access to application settings from json file.
    /// </summary>
    public class JsonSettingsProvider : ISettingsProvider
    {
        #region Private Fields

        private string _jsonPath;
        private AppSettings _settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor. Create new settings provider from json file.
        /// </summary>
        /// <param name="jsonPath">Json file containing app settings.</param>
        public JsonSettingsProvider(string jsonPath)
        {
            this._jsonPath = jsonPath;
            var serializer = new JsonSerializer();
            AppSettings settings = null;
            using (var fileReader = new StreamReader(jsonPath))
            {
                using (var reader = new JsonTextReader(fileReader))
                {
                    settings = serializer.Deserialize<AppSettings>(reader);
                }
            }
            if (settings != null)
                _settings = settings;
        }

        #endregion  
        #region Public methods

        /// <summary>
        /// Get the application settings object.
        /// </summary>
        /// <returns></returns>
        public AppSettings GetSettings() => _settings;

        #endregion
    }
}
