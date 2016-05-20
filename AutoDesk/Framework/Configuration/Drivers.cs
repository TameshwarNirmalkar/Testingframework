using System.Collections.Generic;

namespace AutoDesk.Framework.Configuration
{
    /// <summary>
    /// Drivers represents the driver information.
    /// </summary>
    public class Drivers
    {
        public string name { get; set; }
        //public Proxies proxy { get; set; }
        public List<Capabilities> Capabilities { get; set; }
    }
}
