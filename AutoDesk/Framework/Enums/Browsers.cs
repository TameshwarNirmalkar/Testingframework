using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoDesk.Framework.Enums
{
    /// <summary>
    /// Supported browsers.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Browsers
    {
        IExplorer,
        Chrome,
        Firefox
    }
}
