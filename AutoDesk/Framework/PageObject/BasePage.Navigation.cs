using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using AutoDesk.Framework.Log;
using AutoDesk.Framework.Waits;
using AutoDesk.Framework.Driver;
using AutoDesk.Framework.Waits;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage.Navigation provides operations to navigate to a specific page or between the pages on a particular application.
    /// </summary>
    public abstract partial class BasePage
    {

        #region Browser

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoTo(String Url)
        {
            baseDriver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoBack(String Url)
        {
            baseDriver.Navigate().Back();
        }

        /// <summary>
        /// Goes to a specific URL.
        /// </summary>
        /// <param name="Url">The expected URL</param>
        public void GoForward(String Url)
        {
            baseDriver.Navigate().Forward();
        }

        /// <summary>
        /// Closes the page.
        /// </summary>
        public void Close()
        {
            baseDriver.Close();
            baseDriver.Dispose();
        }

        #endregion

        #region Default Content + IFrame

        /// <summary>
        /// Moves to a particular iframe in the page.
        /// </summary>
        /// <param name="xpath">???</param>
        /// <returnsthe <see cref="IWebDriver"/>returns>
        public IWebDriver SwitchToDefaultContent()
        {
            return baseDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Moves to a particular iframe in the page.
        /// </summary>
        /// <param name="xpath">???</param>
        /// <returnsthe <see cref="IWebDriver"/>returns>
        public IWebDriver SwitchToIFrame()
        {
            return SwitchToDefaultContent().SwitchTo().Frame(0);
                //Frame(FindElement(By.TagName("iframe")));
        }
        #endregion

        #region Tabs

        /// <summary>
        /// Switch to a different windows.
        /// </summary>
        /// <param name="title">the windows title</param>
        public IWebDriver SwitchToTab(string title)
        {
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = baseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = baseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                    {
                        return baseDriver.SwitchTo().Window(handle);
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return baseDriver;
        }

        /// <summary>
        /// Gets the Parent Window handle
        /// </summary>
        /// <param name="title">the windows title</param>
        public IWebDriver GetParentWindowHandle(string title)
        {
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = baseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = baseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                    {
                        return currentWindowHandle;
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return baseDriver;
        }

        /// <summary>
        /// Get New Window Handle
        /// </summary>
        /// <returns>Returns new window handle</returns>
        public string GetNewWindowTitle()
        {
            IWebDriver newtWindowHandle = null;
            string oldtWindowHandle = baseDriver.CurrentWindowHandle;
            try
            {
                ReadOnlyCollection<string> windowHandles = baseDriver.WindowHandles;
                if (windowHandles.Count > 1)
                {
                    foreach (string handle in windowHandles)
                    {
                        if (handle != oldtWindowHandle)
                        {
                            newtWindowHandle = baseDriver.SwitchTo().Window(handle);
                        }
                    }
                }
                else
                {
                    newtWindowHandle = baseDriver.SwitchTo().Window(oldtWindowHandle);
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToTab::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SwitchToTab::" + e.Message);
            }
            return newtWindowHandle.Title;
        }

        /// <summary>
        /// Close tab by name (title).
        /// </summary>
        /// <param name="title">the title tab</param>
        public void CloseTab(string title)
        {
            SwitchToTab(title).Close();
        }

        /// <summary>
        /// Check if Tab is present 
        /// </summary>
        /// <param name="tabtitle">BTN number</param>
        /// <returns>true=if ihdcase tab is present; false=if it is not present</returns>
        public bool IsTabPresent(string tabtitle)
        {
            try
            {
                string path = "//*[@id='tabsContainer']/li";
                IList<IWebElement> tablist = DriverManager.GetDriver().FindElements(By.XPath(path));
                IWebElement customer360Tab = tablist.FirstOrDefault(t => t.Text.Contains(tabtitle));
                customer360Tab.FindElement(By.TagName("i"));
                return true;
            }
            catch (Exception)
            {
                //logger
            }
            return false;
        }

        /// <summary>
        /// Check if Tab is present 
        /// </summary>
        /// <param name="tabtitle">BTN number</param>
        /// <returns>true=if ihdcase tab is present; false=if it is not present</returns>
        public bool IsWindowPresent(string windowtitle)
        {
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = baseDriver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = baseDriver.SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(windowtitle.ToLower()) || currentWindowHandle.Title.ToLower().Contains(windowtitle.ToLower()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            //logger
            }
            return false;            
        }

        /// <summary>
        /// Close the active tab
        /// </summary>
        public void CloseActiveTab()
        {
            
            try
            {
                string path = "//*[@id='tabsContainer']/li[@class='active']/span";
                Driver.DriverManager.GetDriver().FindElement(By.XPath(path)).Click();
                WaitsHandler.WaitForAjaxToComplete(baseDriver);
            }
            catch(Exception)
            {
            }
        }
        #endregion

        #region JS Alert

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAlert SwitchToAlert()
        {     
            try
            {
                WaitsHandler.WaitForAlert(baseDriver, "");
                return baseDriver.SwitchTo().Alert();                
            }
            catch (Exception e)
            {
                LogHandler.Error("SwitchToAlert::Exception - " + e.Message);
                throw new NoSuchElementException("SwitchToAlert::" + e.Message);
            }
            
        }
        /// <summary>
        /// Accept Alert message
        /// </summary>
        /// <returns></returns>
        public void AcceptAlert()
        {
            SwitchToAlert().Accept();
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }

        #endregion

    }
}
