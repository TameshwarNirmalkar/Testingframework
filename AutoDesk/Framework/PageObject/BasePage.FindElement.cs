using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using AutoDesk.Framework.Waits;
using AutoDesk.Framework.Log;
using AutoDesk.Framework.Configuration;
using AutoDesk.Framework.Driver;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage.FindElement provides a mechanism to by element or subelements from a base web element.
    /// </summary>
    public abstract partial class BasePage
    {

        /// <summary>
        /// Finds a web element ny By.
        /// </summary>
        /// <param name="by">the strategy for finding the element</param>
        /// <returns>the <see cref="IWebElement"/></returns>
        public IWebElement FindElement(By by)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, by, "", "");
            return baseDriver.FindElement(by);
        }

        /// <summary>
        /// Finds a set of web element by By.
        /// </summary>
        /// <param name="by">the strategy for finding the element</param>
        /// <returns>a list of <see cref="IWebElement"/></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            //WaitsHandler.WaitForElementToBeVisible(baseDriver, by, "", "");
            return baseDriver.FindElements(by);
        }

        /// <summary>
        /// Finds a set of subelement from an element by By.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="by">the strategy for finding the element</param>
        /// <returns>a list of <see cref="IWebElement"/></returns>
        public ReadOnlyCollection<IWebElement> FindSubElements(IWebElement element, By by)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            return element.FindElements(by);
        }

        /// <summary>
        /// Finds a subelement from an element by By.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="by">the strategy for finding the element</param>
        /// <returns>the <see cref="IWebElement"/></returns>
        public IWebElement FindSubElement(IWebElement element, By by)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            return element.FindElement(by);
        }

        /// <summary>
        /// Verifies if an element is displayed on the page.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        /// <returns>true = If element is visible; 
        ///          false = If eltemen is not visible
        /// </returns>
        public bool IsElementDisplayed(IWebElement element, String elementName, String page)
        {
            List<IWebElement> elementList = new List<IWebElement>();
            elementList.Add(element);
            ReadOnlyCollection<IWebElement> elements = new ReadOnlyCollection<IWebElement>(elementList);

            try
            {
                WebDriverWait wait = new WebDriverWait(DriverManager.GetDriver(), ConfigurationReader.FrameworkConfig.GetExplicitlyTimeout());
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(elements));
                LogHandler.Info("IsElementDisplayed::The element " + elementName + " located by is visible on the page " + page);
                return true;
            }
            catch (Exception e)
            {
                LogHandler.Error("IsElementDisplayed::Exception - " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks if Alert is Present
        /// </summary>
        /// <param name="driver">the <see cref="IWebDriver"/></param>
        /// <param name="page">the page name</param>
        /// <returns>true=If Alert is present; false=If Alert is not present</returns>
        public static bool IsAlertPresent(IWebDriver driver, String page)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, ConfigurationReader.FrameworkConfig.GetExplicitlyTimeout());
                wait.Until(ExpectedConditions.AlertIsPresent());
                LogHandler.Info("WaitForAlert:: The alert is present on the page " + page);
                return true;
            }
            catch (Exception e)
            {
                LogHandler.Error("WaitForAlert::Exception - " + e.Message);
                throw new Exception("WaitForAlert::Exception - " + e.Message);
                return false;
            }
        }
    }
}
