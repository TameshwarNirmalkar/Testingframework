using System;
using System.Collections.Generic;
using AutoDesk.Framework.Enums;

namespace AutoDesk.Framework.Configuration
{
    /// <summary>
    /// Configuration represents the Framework configuration model. 
    /// </summary>
    public class Configuration
    {
        // The list of remote servers.
        public List<RemoteServers> RemoteServers = new List<RemoteServers>();

        // The supported timeouts values.
        public Timeouts Timeouts = new Timeouts();

        // The supported drivers values.
        public List<Drivers> Drivers = new List<Drivers>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Configuration()
        {
        }

        /// <summary>
        /// Gets the implicity timeout in TimeSpan.
        /// </summary>
        /// <returns>Implicit timeout</returns>
        public TimeSpan GetImplicitlyTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.implicitly);
        }

        /// <summary>
        /// Gets the explicity timeout in TimeSpan.
        /// </summary>
        /// <returns>Explicity timeout</returns>
        public TimeSpan GetExplicitlyTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.explicitly);
        }

        /// <summary>
        /// Gets the page load timeout in TimeSpan.
        /// </summary>
        /// <returns>Page load timeout</returns>
        public TimeSpan GetPageLoadTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.pageLoad);
        }

        /// <summary>
        /// Gets the script timeout in TimeSpan.
        /// </summary>
        /// <returns>Script timeout</returns>
        public TimeSpan GetScriptTimeout()
        {
            return TimeSpan.FromSeconds(Timeouts.script);
        }

        /// <summary>
        /// Gets the capabilities for the specific driver.
        /// </summary>
        /// <returns>List of capabilities</returns>
        /// <param name="browser">Browsers</param>
        public List<Capabilities> GetDriverCapabilities(Browsers browser)
        {
            return Drivers.Find(item => item.name.Equals(browser.ToString())).Capabilities;
        }

      
    }
}
