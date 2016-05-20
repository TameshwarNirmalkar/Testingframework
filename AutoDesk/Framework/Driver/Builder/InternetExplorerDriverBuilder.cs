using AutoDesk.Framework.Driver.Builder;
using AutoDesk.Framework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace AutoDesk.Framework.Driver
{
    /// <summary>
    /// InternetExplorerDriverBuilder builds <see cref="InternetExplorerDriver"/>.
    /// </summary>
    internal class InternetExplorerDriverBuilder : IDriverBuilder
    {
        // The internet explorer options.
        private InternetExplorerOptions internetExplorerOptions;

    

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="role">the <see cref="Roles"/>, refers to the specific remote server for it</param>
        public InternetExplorerDriverBuilder()
        {
           
        }

        /// <summary>
        /// <see cref="IDriverBuilder.SetCapabilities"/>.
        /// </summary>
        public IDriverBuilder SetCapabilities(Browsers browser)
        {
            internetExplorerOptions = new InternetExplorerOptions();
            var capabilitySet = Configuration.ConfigurationReader.FrameworkConfig.GetDriverCapabilities(browser);
            foreach (var capability in capabilitySet)
            {
                internetExplorerOptions.AddAdditionalCapability(capability.Name, capability.Value); 
            }
           
            return this;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.Build"/>.
        /// </summary>
        public IWebDriver Build()
        {
            if (internetExplorerOptions == null)
            {
                internetExplorerOptions = new InternetExplorerOptions();
                DesiredCapabilities desiredCapabilities = DesiredCapabilities.InternetExplorer();
                foreach (var capability in desiredCapabilities.ToDictionary())
                {
                    internetExplorerOptions.AddAdditionalCapability(capability.Key, capability.Value);
                }
                
            }
            var driverDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Framework\\Browsers";
            return new InternetExplorerDriver(driverDirectory, internetExplorerOptions);
        }
    }
}
