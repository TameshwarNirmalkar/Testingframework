using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using AutoDesk.Framework.Log;
using AutoDesk.Framework.Waits;
using AutoDesk.Framework.Driver;
using AutoDesk.Framework.Helper;
using OpenQA.Selenium.Support.UI;
using AutoDesk.Framework.Configuration;

namespace AutoDesk.Framework.TestSuite
{
    /// <summary>
    /// BaseTestSuite.ElementValidation provides generic and reusable methods for verifying the web element properties, such as: visibility or not visibility, etc.
    /// </summary>
    public abstract partial class BaseTestSuite
    {

        /// <summary>
        /// Verifies if an element is clickable.
        /// Wrapper method to WaitsHandler.WaitForElementClickeable
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the page name</param>
        protected void IsElementClickable(IWebElement element, String elementName, String page)
        {
            WaitsHandler.WaitForElementClickeable(DriverManager.GetDriver(), element, elementName, page);
        }


        /// <summary>
        /// Verifies if an element text content is equals that the expected text.
        /// Wrapper method to WaitsHandler.WaitForTextToBePresent
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="expectedText">the expected text inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsTextPresent(IWebElement element, String expectedText, String elementName, String page)
        {
            WaitsHandler.WaitForTextToBePresent(DriverManager.GetDriver(), element, expectedText, elementName, page);
        }

        /// <summary>
        /// Verifies if an element text content is equals that the expected text.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="expectedText">the expected text inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsTextPresent(String actualText, String expectedText, String elementName, String page)
        {
            //TODO: We should try to use always IsTextPresent(IWebElement element, String expectedText, String elementName, String page).
            //TODO: Remove the method once we have more time.
            //TODO: In this situation we can't use a explicit wait because we are not passing web elements just text.
            Assert.AreEqual(actualText, expectedText, "The element '" + elementName + "' text is not equal to the expected text on " + page);
            LogHandler.Info("IsTextPresent::The element '" + elementName + "' text is equal to the expected text on " + page);
        }

        /// <summary>
        /// Verifies if an element value attribute content is equals to the expected text.
        /// Wrapper method to WaitsHandler.WaitForTextToBePresentInValue
        /// </summary>
        /// <param name="expectedText">the expected text inside the web element</param>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsValuePresent(IWebElement element, String expectedValue, String elementName, String page)
        {
            WaitsHandler.WaitForTextToBePresentInValue(DriverManager.GetDriver(), element, expectedValue, elementName, page);
        }

        /// <summary>
        /// Verifies if an element text content is contained inside the actual text.
        /// </summary>
        /// <param name="element">the actual text inside the web element</param>
        /// <param name="expectedText">the expected text part inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsTextContained(IWebElement element, String expectedText, String elementName, String page)
        {
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
            Assert.True(element.Text.ToLower().Contains(expectedText.ToLower()), "The element '" + elementName + "' actual text '" + element.Text + "' does not contain '" + expectedText + "' on " + page);
            LogHandler.Info("IsTextContained::The element " + elementName + "text contains in actual text -'" + element.Text + "'");
        }

        /// <summary>
        /// Verifies if an element text content is contained inside the actual text.
        /// </summary>
        /// <param name="actualText">the actual text inside the web element</param>
        /// <param name="expectedText">the expected text part inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsTextContained(String actualText, String expectedText, String elementName, String page)
        {
            //TODO: We should try to use always IsTextContained(IWebElement element, String expectedText, String elementName, String page).
            //TODO: Remove the method once we have more time.
            //TODO: In this situation we can't use a explicit wait because we are not passing web elements just text.
            Assert.True(actualText.Contains(expectedText), "The element '" + elementName + "' actual text '" + actualText + "' does not contain '" + expectedText + "' on " + page);
            LogHandler.Info("IsTextContained::The element " + elementName + "text contains in actual text -'" + actualText + "'");
        }

        /// <summary>
        /// Verifies if an element text content is cleared or not.
        /// </summary>
        /// <param name="actualText">the actual text inside the web element</param>
        /// <param name="expectedText">the expected text inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsElementEmpty(IWebElement element, String elementName, String page)
        {
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
            try
            {
                string value = element.GetAttribute("value");
                Assert.True((value == "" || value == null), "The element " + elementName + " text is not empty on " + page);
                LogHandler.Info("IsElementEmpty::The element " + elementName + " text is empty on " + page);
            }
            catch (Exception e)
            {
                LogHandler.Error("IsElementEmpty::The element " + elementName + " text is not empty on " + page);
                Assert.True(false, e.Message);
            }
        }

        /// <summary>
        /// Verifies if the element is not empty.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the page name</param>
        protected void IsElementNotEmpty(IWebElement element, String elementName, String page)
        {
            //Highlight(element);
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
            try
            {
                if ((element.TagName).Equals("input") || (element.TagName).Equals("textarea"))
                {
                    Assert.AreNotEqual("", element.GetAttribute("value"), "The element " + elementName + " text is empty on " + page);
                    LogHandler.Info("IsElementNotEmpty::The element " + elementName + " text is not empty on " + page);
                }
                else
                {
                    Assert.AreNotEqual("", element.Text, "The element " + elementName + " text is empty on " + page);
                    LogHandler.Info("IsElementNotEmpty::The element " + elementName + " text is not empty on " + page);
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("IsElementNotEmpty::The element " + elementName + " text is empty on " + page);
                Assert.True(false, e.Message);
            }
        }

        /// <summary>
        /// Verifies if an element is displayed on the page.
        /// Wrapper method to WaitsHandler.WaitForElementToBeVisible
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsElementDisplayed(IWebElement element, String elementName, String page)
        {
            //Highlight(element);
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
        }

        /// <summary>
        /// Verifies if an element is displayed on the page.
        /// It uses WaitsHandler.WaitForElementUntilCondition
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="element">the display condition</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        //protected void IsElementDisplayed(IWebElement element, bool condition, String elementName, String page)
        //{
           
        //    Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated."); 
        //    try
        //    {
        //        Func<IWebDriver, Boolean> function = delegate(IWebDriver d) { return element.Displayed == condition; };
        //        WaitsHandler.WaitForElementUntilCondition(DriverManager.GetDriver(), function, page);
        //    }
        //    catch (Exception)
        //    {
        //        if (condition)
        //        {
        //            LogHandler.Error("IsElementDisplayed::The expected element " + elementName + "to be visible IS NOT visible on the page " + page);
        //            Assert.Fail("The expected element " + elementName + "to be visible IS NOT visible on the page " + page);
        //        }
        //        else
        //        {
        //            LogHandler.Error("IsElementDisplayed::The expected element " + elementName + "to be not visible IS visible on the page " + page);
        //            Assert.Fail("The expected element " + elementName + "to be not visible IS visible on the page " + page);
        //        }

        //    }
        //}

        /// <summary>
        /// Verifies if an element is not displayed on the page.
        /// It uses WaitsHandler.WaitForElementUntilCondition
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsElementNotDisplayed(IWebElement element, String elementName, String page)
        {
            Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated."); 
            try
            {                
                if(element.TagName.Equals("i")){
                   Func<IWebDriver, Boolean> function = delegate(IWebDriver d) { return element.GetAttribute("style").Equals("display: none;"); };
                    WaitsHandler.WaitForElementUntilCondition(DriverManager.GetDriver(), function, page);
                }
                else{
                    Func<IWebDriver, Boolean> function = delegate(IWebDriver d) { return element.Displayed == false; };
                    WaitsHandler.WaitForElementUntilCondition(DriverManager.GetDriver(), function, page);
                }               
            }
            catch (Exception)
            {
                Assert.Fail("The expected element " + elementName + "to be not visible IS visible on the page " + page);
            }
        }

        /// <summary>
        /// Verifies if an element is not enabled on the page.
        /// It uses WaitsHandler.WaitForElementUntilCondition
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsElementNotEnabled(IWebElement element, String elementName, String page)
        {
            Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated."); 
            try
            {
                Func<IWebDriver, Boolean> function = delegate(IWebDriver d) { return element.Enabled == false; };
                WaitsHandler.WaitForElementUntilCondition(DriverManager.GetDriver(), function, page);
            }
            catch (Exception)
            {
                Assert.Fail("The expected element " + elementName + "to be not enable IS enable on the page " + page);
            }
        }

        /// <summary>
        /// Verifies if an element is enabled on the page.
        /// It uses WaitsHandler.WaitForElementUntilCondition
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void IsElementEnabled(IWebElement element, String elementName, String page)
        {            
            Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated.");
            IsElementDisplayed(element, elementName, page);
            try
            {
                Func<IWebDriver, Boolean> function = delegate(IWebDriver d) { return element.Enabled == true; };
                WaitsHandler.WaitForElementUntilCondition(DriverManager.GetDriver(), function, page);
            }
            catch (Exception)
            {
                Assert.Fail("The expected element " + elementName + "to be enable IS NOT enable on the page " + page);
            }
        }

        /// <summary>
        /// Verifies if an elelement is editable.
        /// It uses WaitsHandler.WaitForElementUntilCondition
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the page name</param>
        protected void IsElementEditable(IWebElement element, String elementName, String page)
        {
            //Highlight(element);
            Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated."); 
            try
            {
                string tagName = element.TagName;
                if ((tagName).Equals("input") || (tagName).Equals("textarea"))
                {
                    string disabled = element.GetAttribute("disabled");
                    Assert.AreNotEqual(disabled, "true", "The element " + elementName + " is not editable on " + page);
                    LogHandler.Info("IsElementEditable::The element " + elementName + " is editable on " + page);
                }
                else if ((tagName).Equals("select"))
                {
                    Assert.True(element.Enabled, "The element " + elementName + " is not editable on " + page);
                    LogHandler.Info("IsElementEditable::The element " + elementName + " is editable on " + page);
                }
                else
                {
                    Assert.True(false, "The element " + elementName + " is not editable on " + page);
                    LogHandler.Info("IsElementEditable::The element " + elementName + " is not editable on " + page);
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("IsElementEditable::Exception - " + e.Message);
                Assert.True(false, "IsElementEditable::Exception - " + e.Message);
            }
        }

        /// <summary>
        /// Verifies if the element is not editable.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the page name</param>
        protected void IsElementNotEditable(IWebElement element, String elementName, String page)
        {
            //Highlight(element);
            Assert.NotNull(element, "NPE::The element " + elementName + " on the page " + page + " is not populated."); 
            try
            {
                string tagName = element.TagName;
                if ((tagName).Equals("input") )
                {
                    Assert.AreEqual(element.GetAttribute("disabled"), "true", "The element " + elementName + " is editable on " + page);
                    LogHandler.Info("IsElementNotEditable::The element " + elementName + " is not editable on " + page);
                }
                else if ((tagName).Equals("textarea"))
                {
                    try
                    {
                        Assert.AreEqual(element.GetAttribute("disabled"), "true", "The element " + elementName + " is editable on " + page);
                    }
                    catch (Exception e)
                    {
                        Assert.AreEqual(element.GetAttribute("readonly"), "true", "The element " + elementName + " is editable on " + page);
                        LogHandler.Info("IsElementNotEditable::The element " + elementName + " is not editable on " + page);
                    }
                }
                else if ((tagName).Equals("select"))
                {
                    Assert.False(element.Enabled, "The element " + elementName + " is editable on " + page);
                    LogHandler.Info("IsElementNotEditable::The element " + elementName + " is not editable on " + page);
                }
                else
                {                    
                    Assert.True(true, "The element " + elementName + " is editable on " + page);
                    LogHandler.Info("IsElementNotEditable::The element " + elementName + " is not editable on " + page);
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("IsElementNotEditable::Exception - " + e.Message);
                Assert.True(false, "IsElementNotEditable::Exception - " + e.Message);
            }
        }

        /// <summary>
        /// Verifies if the links in the list are in order based on expectedLinks order.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="expectedLinks">the list of expected inks</param>
        // <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void AreLinksInOrder(IWebElement element, List<String> expectedLinks, string elementName, string page)
        {
            try
            {
                int ItemCount = 0;
                int ExpItemCount = 0;
                int i = 0;
                IsElementDisplayed(element, elementName, page);
                IList<IWebElement> items = element.FindElements(By.TagName("a"));
                ItemCount = items.Count;
                ExpItemCount = expectedLinks.Count;
                if (ItemCount == ExpItemCount)
                {
                    foreach (IWebElement ite in items)
                    {
                        string LinkText = ite.Text;
                        string ExpLinkText = expectedLinks[i];
                        LogHandler.Info("VerifyLinksInOrder:: Actual Text - " + LinkText + " Expected Text -" + ExpLinkText);
                        IsTextPresent(ite, ExpLinkText, elementName, page);
                        i++;
                    }
                }
                else
                {
                    LogHandler.Error("VerifyLinksInOrder::Items count mismatch");
                    Assert.True(false, "VerifyLinksInOrder::The expected links count is not equals to the current links on the page.");
                }
                LogHandler.Info("VerifyLinksInOrder::Expected Links found");
            }
            catch (Exception e)
            {
                LogHandler.Error("VerifyLinksInOrder::Expected Link not found");
                Assert.True(false, "VerifyLinksInOrder::Unexpected error - " + e.Message);
            }
        }

        /// <summary>
        /// Find whether the given element is readonly 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elementName"></param>
        /// <param name="page"></param>
        public void IsElementReadonly(IWebElement element, string elementName, string page)
        {
            var result = element.GetAttribute("readonly");
            //Highlight(element);
            if (bool.Parse(result))
            {
                Assert.AreEqual(result, "true", "The element " + elementName + " is editable on " + page);
                LogHandler.Info("IsElementNotEditable::The element " + elementName + " is not editable on " + page);
            }
            else
            {
                LogHandler.Error("IsElementReadonly::Expected element is editable");
                Assert.True(false, "IsElementReadonly::Expected element is editable.");
            }
        }

        /// <summary>
        /// Verifies if an element is displayed on the page.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <param name="by">the web element locator</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        /// <returns>true = If element is visible; 
        ///          false = If eltemen is not visible
        /// </returns>
        public bool IsElementDisplayed(IWebElement element,By by, String elementName, String page)
        {
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverManager.GetDriver(), ConfigurationReader.FrameworkConfig.GetExplicitlyTimeout());
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
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
                return false;
            }
        }

        /// <summary>
        /// Overloaded Method
        /// </summary>
        /// <param name="elements">List of elements</param>
        /// <param name="expectedLinks">List of exected values</param>
        /// <param name="elementName">Name of the element</param>
        /// <param name="page"></param>
        protected void AreLinksInOrder(IList<IWebElement> elements, List<String> expectedLinks, string elementName, string page)
        {
            try
            {
                int i = 0;
                foreach (IWebElement element in elements)
                {                         
                            string LinkText = element.Text;
                            string ExpLinkText = expectedLinks[i];
                            LogHandler.Info("VerifyLinksInOrder:: Actual Text - " + LinkText + " Expected Text -" + ExpLinkText);
                            IsTextPresent(LinkText, ExpLinkText, elementName, page);
                            i++;
                }
                    
            }
            catch (Exception)
            {
                LogHandler.Error("VerifyLinksInOrder::Expected Link not found");
                Assert.True(false, "VerifyLinksInOrder::Unexpected error. Check the logs for more information.");
            }
        }

        /// <summary>
        /// Verifies if an element is on the page.
        /// If is on the page then WaitsHandler.WaitForElementToBeVisible
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="condition">the expected condition. True the element should be on the page.</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the page name</param>
        protected void IsElementOnPage(IWebElement element, bool condition, String elementName, String page)
        {
            System.Drawing.Point location;
            try
            {
                location = element.Location;
                Assert.False(location.IsEmpty);
                Assert.True(condition, "The element " + elementName + " is not on the page " + page + " but it's expected to be.");
                WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), element, elementName, page);
            }
            catch (Exception)
            {
                if (!condition)
                Assert.False(condition, "The element " + elementName + " is on the page " + page + " but it's not expected to be.");
                else
                Assert.False(condition, "The element " + elementName + " is not on the page " + page + " but it's expected to be.");
            }
        }
    }
}
