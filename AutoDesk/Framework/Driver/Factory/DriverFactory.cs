using AutoDesk.Framework.Enums;
using AutoDesk.Framework.Configuration;
using AutoDesk.Framework.Driver.Builder;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutoDesk.Framework.Driver.Factory
{
    /// <summary>
    /// DriverFactory creates a new instance of a particular web driver.
    /// 
    /// DriverFactory delegates the creation itself to the specific builder.
    /// </summary>
    internal sealed class DriverFactory
    {

        /// <summary>
        /// Returns a new instance of [see cref="IWebDriver"]/>.
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <param name="remote">the boolean value to determine if the driver is remote or not</param>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns></returns>
        public static IWebDriver NewInstance(Browsers browser)
        {
            IWebDriver driver = null;
           
            {
                driver = NewLocalInstance(browser);
            }

            driver.Manage().Timeouts().ImplicitlyWait(ConfigurationReader.FrameworkConfig.GetImplicitlyTimeout());
            driver.Manage().Timeouts().SetScriptTimeout(ConfigurationReader.FrameworkConfig.GetScriptTimeout());
            driver.Manage().Timeouts().SetPageLoadTimeout(ConfigurationReader.FrameworkConfig.GetPageLoadTimeout());
            driver.Manage().Window.Maximize();

            return driver;
        }

        /// <summary>
        /// Returns a new local instance of <see cref="IWebDriver"/>.
        /// </summary>
        /// <param name="browser">the <see cref="Browsers"/></param>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns>the <see cref="IWebDriver"/></returns>
        private static IWebDriver NewLocalInstance(Browsers browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    driver = new ChromeDriverBuilder().Build();
                    break;
                case Browsers.IExplorer:
                    driver = new InternetExplorerDriverBuilder().Build();
                    break;
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
            }
            return driver;
        }
    }
}


