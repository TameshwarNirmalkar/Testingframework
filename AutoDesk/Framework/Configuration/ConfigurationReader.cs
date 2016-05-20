using Newtonsoft.Json;
using System;
using System.IO;

namespace AutoDesk.Framework.Configuration
{
    /// <summary>
    /// ConfigurationReader reads Config/Config.json and populates <see cref="Configuration"/>.
    /// </summary>
    public class ConfigurationReader
    {
        // The framework configuration.
        public static readonly Configuration FrameworkConfig;

        /// <summary>
        /// Default constructor.
        /// Parse the JSON configuration file and converts it to [see cref="Configuration"]/>
        /// </summary>
        static ConfigurationReader()
        {
            try
            {
                FrameworkConfig = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+"\\Framework\\Configuration\\Config\\Config.json"));
            }
            catch (JsonReaderException ex)
            {
                throw new JsonReaderException(ex.Message);
            }
            
        }
    }
}
