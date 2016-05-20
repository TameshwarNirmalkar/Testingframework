using OpenQA.Selenium;
using AutoDesk.Framework.Enums;

namespace AutoDesk.Framework.Driver.Builder
{
    /// <summary>
    /// IDriverBuilder represents the common interface to build a specific web driver.
    /// </summary>
    interface IDriverBuilder
    {

        /// <summary>
        /// Sets the capabilities for the driver to be created.
        /// </summary>
        /// <param name="browrser">the browser <see cref="Browsers"/></param>
        /// <returns>the <see cref="IDriverBuilder"/></returns>
        IDriverBuilder SetCapabilities(Browsers browrser);
        
        /// <summary>
        /// Builds the web driver.
        /// </summary>
        /// <returns>the <see cref="IWebDriver"/></returns>
        IWebDriver Build();
        
    }
}
