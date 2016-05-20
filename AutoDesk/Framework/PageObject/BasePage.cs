using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using AutoDesk.Framework.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoDesk.Framework.Log;
using AutoDesk.Framework.Helper;
using AutoDesk.Framework.Waits;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage contains all the common functionality wrappers for Selenium operations.
    /// </summary>
    public abstract partial class BasePage
    {
        //The Driver instance.
        protected IWebDriver baseDriver;

        /// <summary>
        /// Default constructor.
        /// Use PageFactory to initialize web elements.
        /// </summary>
        /// <param name="driver">the <see cref="IWebDriver"/></param>
        public BasePage(IWebDriver driver)
        {
            baseDriver = driver;
            PageFactory.InitElements(baseDriver, this);
            WaitsHandler.WaitForAjaxToComplete(baseDriver);
        }

    }
}