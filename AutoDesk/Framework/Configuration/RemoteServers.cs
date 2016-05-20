using AutoDesk.Framework.Enums;

namespace AutoDesk.Framework.Configuration
{
    /// <summary>
    /// RemoteServers represents the remote servers.
    /// </summary>
    public class RemoteServers
    {
        public string url { get; set; }
       // public string proxyChrome { get; set; }
        public string proxyIE { get; set; }
        public bool useGrid { get; set; }
    }
}
