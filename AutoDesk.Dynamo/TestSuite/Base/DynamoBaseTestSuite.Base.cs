using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using AutoDesk.Framework.TestSuite;
using AutoDesk.Framework.PageObject;
using AutoDesk.Framework.Enums;
using AutoDesk.Framework.Driver;
using AutoDesk.Framework.Waits;
using AutoDesk.Dynamo.PageObject;
using OpenQA.Selenium;
using NUnit.Framework;
using AutoDesk.Framework.Environment;

namespace AutoDesk.Dynamo.TestSuite.Base
{
    /// <summary>
    /// DynamoBaseTestSuite.Base represents the base class for test suite.
    /// 
    /// DynamoBaseTestSuite.Base supports generic methods to be used for Dynamo test automation.
    /// 
    /// DynamoTestSuite.Base inherit generic methods also from <see cref="BaseTestSuite"/>.
    /// </summary>
    public abstract partial class DynamoBaseTestSuite : BaseTestSuite
    {

        protected const String DYNAMO_HOME_PAGE = "Dynamo home Page";
        

        /// <summary>
        /// Custom on after test configuration method for Dynamo application.
        /// </summary>
        protected override void OnAfterTest()
        {
            DriverManager.CloseDriver();
        }

        

        #region Utiliy methods not related to shared steps or validation

     


        /// <summary>
        /// Gets expernal URL from properties.
        /// </summary>
        /// <param name="Key">the key</param>
        /// <returns></returns>
        public string GetExternalURL(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }



        /// <summary>
        /// Validates the Current page title.
        /// </summary>
        /// <param name="title"> String to check with page title</param>
        /// <returns> True if value is same else false</returns>
        public void ClickDashboardTab()
        {
            string dashboardpath = "//*[@id=\"tabsContainer\"]/li[1]/a";
            //SleepHelper.Sleep(5);
            DriverManager.GetDriver().FindElement(By.XPath(dashboardpath)).Click();
        }


        /// <summary>
        /// Click on Tab
        /// </summary>
        /// <param name="title">Btn Number</param>
        public void ClickOnTab(string title)
        {  
            string path = "//*[@id='tabsContainer']/li/a/span[contains(text(),'" + title + "')]";
            DriverManager.GetDriver().FindElement(By.XPath(path)).Click();
        }

        /// <summary>
        /// Close active tab.
        /// </summary>
        public void CloseActiveTab()
        {
            string active = "active";
            string path = "//*[@id='tabsContainer']/li";
            WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), By.XPath(path), "Active Tab", "Current Page");
            IList<IWebElement> tablist = DriverManager.GetDriver().FindElements(By.XPath(path));
            foreach (var v in tablist)
            {
                if (active == v.GetAttribute("class"))
                {
                    string actual_path = path + "[contains(@class, 'active')]/span/i";
                    WaitsHandler.WaitForElementToBeVisible(DriverManager.GetDriver(), By.XPath(actual_path), "Active Tab Button", "Current Page");
                    DriverManager.GetDriver().FindElement(By.XPath(actual_path)).Click();

                }
            }

        }
       
        /// <summary>
        /// Verifies if the specified tab is present or not
        /// </summary>
        /// <param name="title">Btn Number</param>
        public bool IsTabPresent(string title)
        {
            bool value = false;
            try
            {
                string path = "//*[@id='tabsContainer']/li/a/span[contains(text(),'" + title + "')]";
                IWebElement tab = DriverManager.GetDriver().FindElement(By.XPath(path));
                if (tab.Displayed)
                {
                    value = true;
                }
            }
            catch (Exception)
            {
                value = false;
            }
            return value;
        }


        /// <summary>
        /// Verifies the current page title.
        /// </summary>
        /// <param name="title"> String to check with page title</param>
        /// <returns> True if value is same else false</returns>
        public Boolean VerifyCurrentPageTitle(String title)
        {
            WaitsHandler.WaitForAjaxToComplete(DriverManager.GetDriver());
            string value = null;
            string path = "//*[@id='tabsContainer']/li";
            string active = "active";
            //SleepHelper.Sleep(5);
            IList<IWebElement> tablist = DriverManager.GetDriver().FindElements(By.XPath(path));
            foreach (var v in tablist)
            {
                if (active == v.GetAttribute("class"))
                {
                    string actual_path = path + "[contains(@class, 'active')]/a";
                    value = DriverManager.GetDriver().FindElement(By.XPath(actual_path)).Text;
                    if (value.ToUpper().Contains(title.ToUpper()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This method is closing session.
        /// </summary>
        public void CloseSession()
        {
            OnAfterTest();
        }

        #endregion

       
    }
}
