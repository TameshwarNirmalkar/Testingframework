using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using AutoDesk.Framework.Waits;
using AutoDesk.Framework.Log;
using AutoDesk.Framework.Driver;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage.Actions partial class supporting actions to execute in the page. 
    /// Such as: click, double click, input text, etc.
    /// </summary>
    public abstract partial class BasePage
    {
        /// <summary>
        /// This function enter text in the specified element.
        /// 1. Clear the field
        /// 2. Send keys (Text) to the web element
        /// </summary>
        /// <param name="element"> Element to enter text into </param>
        /// <param name="text">Text to enter in element </param>
        public void TypeText(IWebElement element, String text)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            try
            {
                element.Clear();
                element.SendKeys(text);
                LogHandler.Info("TypeText::The text " + text + " has been entered in InputBox with value: " + text);
            }
            catch (Exception e)
            {
                LogHandler.Error("TypeText::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("TypeText::" + e.Message);
            }            
        }

        /// <summary>
        /// This function enter text in the specified element.
        /// 1. Clear the field
        /// 2. Send keys (Text) to the web element
        /// </summary>
        /// <param name="element"> Element to enter text into </param>
        /// <param name="text">Text to enter in element </param>
        public void TypeText(IWebElement element, String text, String elementName)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, elementName, "");
            try
            {
                element.Clear();
                element.SendKeys(text);
                LogHandler.Info("TypeText::The text has been entered in InputBox '" + elementName + "' with value: " + text);
            }
            catch (Exception e)
            {
                LogHandler.Error("TypeText::The text has been failed to enter in InputBox '" + elementName + "' with value: " + text);
                LogHandler.Error("TypeText::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("TypeText::" + e.Message);
            }
        }
        /// <summary>
        /// Highlights an element.
        /// </summary>
        /// <param name="element">the element to be highlighted</param>
        protected void Highlight(IWebElement element)
        {
            // IsElementDisplayed(element, "", "");
            var jsDriver = (IJavaScriptExecutor)DriverManager.GetDriver();
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }

        /// <summary>
        /// This function perform Click operation on the specified element. 
        /// </summary>
        /// <param name="element">IWebElement</param>
        public void Click(IWebElement element)
        {
           //Highlight(element);
          // WaitsHandler.WaitForElementClickeable(baseDriver, element, "", "");
            try
            {
                element.Click();
                LogHandler.Info("Click::The element has been clicked");
            }
            catch (Exception e)
            {
                LogHandler.Error("Click::Exception - " + e.Message);
                throw new NoSuchElementException("Click::Exception - " + e.Message);
            }
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }

        /// <summary>
        /// This function perform Click operation on the specified element after waiting for other element to be invisiable. 
        /// </summary>
        /// <param name="element">IWebElement</param>
        public void ClickWait(IWebElement element, String locatorName, String locator)
        {
            //Highlight(element);
            WaitsHandler.WaitForElememntTobeInvisiable(baseDriver, locatorName, locator, "", "");
            try
            {
                element.Click();
                LogHandler.Info("ClickWait::The element has been clicked");
            }
            catch (Exception e)
            {
                LogHandler.Error("ClickWait::Exception - " + e.Message);
                throw new NoSuchElementException("Click::Exception - " + e.Message);
            }
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }
        /// <summary>
        /// This function perform Click operation on the specified element. 
        /// </summary>
        /// <param name="element">IWebElement</param>
        public void Click(IWebElement element, String ElementName)
        {
            //Highlight(element);
            WaitsHandler.WaitForElementClickeable(baseDriver, element, ElementName, "");
            try
            {
                element.Click();
                LogHandler.Info("Click::The element '" + ElementName + "' has been clicked");
            }
            catch (Exception e)
            {
                LogHandler.Info("Click::The click on element '" + ElementName + "' has been failed");
                LogHandler.Error("Click::Exception - " + e.Message);
                throw new NoSuchElementException("Click::Exception - " + e.Message);
            }
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }


        /// <summary>
        /// Performs a  Double Click operation on the specified element. 
        /// </summary>
        /// <param name="eLocator"> String for element to locate </param>
        /// <param name="eType">Option for selection such as id / Xpath /..etc </param>
        public void DoubleClick(IWebElement element)
        {
            WaitsHandler.WaitForElementClickeable(baseDriver, element, "", "");
            try
            {
                new Actions(baseDriver).DoubleClick(element).Perform();
                LogHandler.Info("DoubleClick::The element has been double clicked");
            }
            catch (Exception e)
            {
                LogHandler.Error("DoubleClick::Exception - " + e.Message);
                throw new NoSuchElementException("DoubleClick::Exception - " + e.Message);
            }
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }

        /// <summary>
        /// Performs a  Double Click operation on the specified element. 
        /// </summary>
        /// <param name="eLocator"> String for element to locate </param>
        /// <param name="eType">Option for selection such as id / Xpath /..etc </param>
        public void DoubleClick(IWebElement element, String elementName)
        {
            WaitsHandler.WaitForElementClickeable(baseDriver, element, elementName, "");
            try
            {
                new Actions(baseDriver).DoubleClick(element).Perform();
                LogHandler.Info("DoubleClick::The element '" + elementName + "' has been doubleclicked");
            }
            catch (Exception e)
            {
                LogHandler.Error("DoubleClick::The doubleclick on element '" + elementName + "' has been failed");
                LogHandler.Error("DoubleClick::Exception - " + e.Message);
                throw new NoSuchElementException("DoubleClick::Exception - " + e.Message);
            }
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }

        /// <summary>
        /// Selects value from element using specified text
        /// </summary>
        /// <param name="element"> Element to locate on web page</param>
        /// <param name="text">Text value for selection</param>
        public void SelectByText(IWebElement element, String text)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            try
            {
                SelectElement select = new SelectElement(element);
                select.SelectByText(text);
                LogHandler.Info("SelectByText::The option has been selected by text: " + text);
            }
            catch (Exception e)
            {
                LogHandler.Error("SelectByText::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SelectByText::" + e.Message);
            }
        }
        /// <summary>
        /// Selects value from element using specified text
        /// </summary>
        /// <param name="element"> Element to locate on web page</param>
        /// <param name="text">Text value for selection</param>
        public void SelectByText(IWebElement element, String text, String elementName)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, elementName, "");
            try
            {
                SelectElement select = new SelectElement(element);
                select.SelectByText(text);
                LogHandler.Info("SelectByText::The element '" + elementName + "' select option has been selected by text: " + text);
            }
            catch (Exception e)
            {
                LogHandler.Error("SelectByText::The element '" + elementName +"' - "+"NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SelectByText::" + e.Message);
            }
        }


        /// <summary>
        /// Selects value from element using specified index.
        /// </summary>
        /// <param name="element"> Element to locate on web page</param>
        /// <param name="index">Index number for selection</param>
        public void SelectByIndex(IWebElement element, int index)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            try
            {
                SelectElement select = new SelectElement(element);
                select.SelectByIndex(index);
                LogHandler.Info("SelectByText::The option has been selected by index: " + index);
            }
            catch (Exception e)
            {
                LogHandler.Error("SelectByIndex::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SelectByIndex::" + e.Message);
            }
        }


        /// <summary>
        /// Selects value from element using specified value
        /// </summary>
        /// <param name="locator"> String for element to locate </param>
        /// <param name="type">Option for selection such as id / Xpath /..etc </param>
        /// <param name="index">Index value for selection</param>
        public void SelectByValue(IWebElement element, String value, String elementName)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, elementName, "");
            try
            {
                SelectElement select = new SelectElement(element);
                IWebElement sdsd = select.SelectedOption;
                select.SelectByValue(value);                
                LogHandler.Info("SelectByValue::The element '" + elementName + "' select option has been selected by value: " + value);
            }
            catch (Exception e)
            {
                LogHandler.Error("SelectByValue::The element '" + elementName + "' - " + "NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SelectByValue::" + e.Message);
            }
        }

        /// <summary>
        /// Moves the mouse over the element.
        /// </summary>
        /// <param name="Element">the target element</param>
        public void MouseOver(IWebElement element)
        {
            WaitsHandler.WaitForElementToBeSelected(baseDriver, element, "", "");
            try
            {
                new Actions(baseDriver).MoveToElement(element).Perform();
            }
            catch (Exception e)
            {
                LogHandler.Error("MouseOver::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("MouseOver::" + e.Message);
            }
        }

        /// <summary>
        /// This function will append text to the already present text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public void ConcatinateText(IWebElement element, String text)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            try
            {
                element.SendKeys(text);
                LogHandler.Info("TypeText::The text " + text + " has been entered in InputBox with value: " + text);
            }
            catch (Exception e)
            {
                LogHandler.Error("TypeText::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("TypeText::" + e.Message);
            }
        }

    }
}
