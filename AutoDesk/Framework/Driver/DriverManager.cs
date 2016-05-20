using AutoDesk.Framework.Driver.Factory;
using OpenQA.Selenium;
using System.Threading;
using AutoDesk.Framework.Environment;
using AutoDesk.Framework.Enums;
using AutoDesk.Framework.Log;

namespace AutoDesk.Framework.Driver
{
    /// <summary>
    /// DriverManager manages the <see cref="IWebDriver"/> instances depending on the test configuration.
    /// 
    /// DriverManager supports parallel execution by storing web driver in <see cref="ThreadLocal{T}"/> array.
    /// 
    /// DriverManager delegates the web driver creation to <see cref="DriverFactory"/>.
    /// </summary>
    public sealed class DriverManager
    {
        // The driver container.
        private static ThreadLocal<IWebDriver> driverContainer = new ThreadLocal<IWebDriver>();

        /// <summary>
        /// Populates the WebDriver instance.
        /// It creates a new one if the driver does not exist for the current thread.
        /// </summary>
        /// <param name="role">the <see cref="Roles"/></param>
        /// <returns><see cref="IWebDriver"/></returns>
        public static IWebDriver PopulateDriver()
        {
            if (driverContainer.Value == null)
            {
                driverContainer.Value = DriverFactory.NewInstance(EnvironmentReader.Browser);
            }
            //LogHandler.Info("BASE URL - " + EnvironmentReader.Base_URL);
            //LogHandler.Info("BROWSER - " + EnvironmentReader.Browser);
            //LogHandler.Info("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            return driverContainer.Value;
        }

        /// <summary>
        /// Gets the WebDriver instance.
        /// </summary>
        /// <returns><see cref="IWebDriver"/></returns>
        public static IWebDriver GetDriver()
        {
            return driverContainer.Value;
        }

        /// <summary>
        /// Close the current instance of the driver.
        /// </summary>
        public static void CloseDriver()
        {
            if (driverContainer.Value != null)
            {
                driverContainer.Value.Quit();
                driverContainer.Value.Dispose();
                driverContainer.Value = null;
            }
        }
    }
}
