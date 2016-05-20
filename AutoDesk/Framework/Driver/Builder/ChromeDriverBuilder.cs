using AutoDesk.Framework.Driver.Builder;
using AutoDesk.Framework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace AutoDesk.Framework.Driver
{
    /// <summary>
    /// ChromeDriverBuilder builds <see cref="ChromeDriver"/>.
    /// </summary>
    internal class ChromeDriverBuilder : IDriverBuilder
    {
        //The chrome options.
        private ChromeOptions chromeOptions;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="role">the <see cref="Roles"/>, refers to the specific remote server for it</param>
        public ChromeDriverBuilder()
        {
           
        }

        /// <summary>
        /// <see cref="IDriverBuilder.SetCapabilities"/>.
        /// </summary>
        public IDriverBuilder SetCapabilities(Browsers browser)
        {
            
            chromeOptions = new ChromeOptions();
            var capabilitySet = Configuration.ConfigurationReader.FrameworkConfig.GetDriverCapabilities(browser);
            foreach (var capability in capabilitySet)
            {
                chromeOptions.AddUserProfilePreference(capability.Name, capability.Value);
            }
            return this;
        }

        /// <summary>
        /// <see cref="IDriverBuilder.Build"/>.
        /// </summary>
        public IWebDriver Build()
        {
            if (chromeOptions == null)
            {
                chromeOptions = new ChromeOptions();
               // chromeOptions.Proxy = ProxyManager.GetProxy(role);
            }
            return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Framework\\Browsers", chromeOptions);
        }
    }
}
