using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoDesk.Framework.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutoDesk.Framework.Configuration;
using System.Reflection;
using AutoDesk.Framework.PageObject;
using AutoDesk.Framework.Log;
using System.Globalization;
using AutoDesk.Framework.Enums;
using AutoDesk.Framework.Helper;
using AutoDesk.Framework.Environment;
using AutoDesk.Framework.Waits;
using System.Text.RegularExpressions;
using NUnit.Framework.Interfaces;
using log4net;

namespace AutoDesk.Framework.TestSuite
{
    /// <summary>
    /// BaseTestSuite class is used by every TestSuite. 
    /// It has basic common functions needed by each Suite , such as Setup and TearDown.
    /// </summary>
    public abstract partial class BaseTestSuite
    {

        [OneTimeSetUp]
        public void BeforeSuite()
        {
            OnBeforeSuite();
        }

        [OneTimeTearDown]
        public void AfterSuite()
        {
            OnAfterSuite();
            }

        [SetUp]
        public void BeforeTest()
        {
            OnBeforeTest();
        }

        [TearDown]
        public void AfterTest()
        {
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
            {
                Console.WriteLine("Test Status : FAILED");
            }
            else if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
            {
                Console.WriteLine("Test Status : PASSED");
            }
            OnAfterTest();
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
            {
                Console.WriteLine("Fail");
            }
            else if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
            {
                Console.WriteLine("PASS");
            }
            LogHandler.Info("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  END OF SCENARIO   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n");
            LogManager.Shutdown();

        }

        protected virtual void OnBeforeSuite()
        {
        }

        protected virtual void OnAfterSuite()
        {
        }

        protected virtual void OnBeforeTest()
        {            
        }

        protected virtual void OnAfterTest()
        {
 
        }

        /// <summary>
        /// Gets an instance of a particular PageObject of Dynamo application.
        /// </summary>
        /// <typeparam name="T">The PO class to be created by reflection</typeparam>
        /// <returns>the PageObject</returns>
        protected T GetPage<T>() where T : BasePage
        {
            return PageFactoryHelper.GetPage<T>();
        }

        /// <summary>
        /// Init logs based on Logger configuration.
        /// </summary>
        /// <param name="parameters">test parameters</param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected void InitLogs(List<string> parameters)
        {
            String DataSet = "";
            if (EnvironmentReader.Logger)
            {
                var callingMethod = new System.Diagnostics.StackTrace(1, false).GetFrame(0).GetMethod().Name;
                var callingClassName = new System.Diagnostics.StackTrace(1, false).GetFrame(0).GetMethod().DeclaringType.Name;
                foreach (string item in parameters)
                {
                    DataSet = DataSet + item + ",";
                }
                LogHandler.setLogs(callingMethod, callingClassName, DataSet, EnvironmentReader.Logger);
            }
            DataSet = "";
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
        /// Get the current value of Date Time in EST(Eastern Standard Time).
        /// </summary>
        /// <return name = "ESTtime">EST time in string format</return>        
        protected String GetESTtime()
        {
            //Based on Sebastian comments, we should not check the time just the date
            DateTime eastern = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");
            //FIXME: If the format is dd/MM/yyyy hh:mm, an error could happen due to the minute has changed.
            String ESTtime = eastern.ToString("dd/MM/yyyy hh", CultureInfo.InvariantCulture);
            return ESTtime;
        }

        /// <summary>
        /// Get the current value of Date Time in EST(Eastern Standard Time).
        /// </summary>
        /// <return name = "ESTtime">EST time in string format</return>        
        protected String GetESTDate()
        {
            //Based on Sebastian comments, we should not check the time just the date
            DateTime eastern = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");
            //FIXME: If the format is dd/MM/yyyy hh:mm, an error could happen due to the minute has changed.
            String ESTtime = eastern.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return ESTtime;
        }

        /// <summary>
        /// Verify if the current value of application Date Time in EST(Eastern Standard Time).
        /// </summary>        
        protected void IsESTtimeFormat(String est, String elementName, String page)
        {
            try
            {
                Regex regex = new Regex("^([0]?[1-9]|[1][0-2])[/]([0]?[1-9]|[1|2][0-9]|[3][0|1])[/]([0-9]{4}) ([0-9]|[1][0-2]):([0-5][0-9]):([0-5][0-9]) (AM|PM)$");
                //Match match = regex.Match("5/3/2010 1:00:00 AM");                
                Match match = regex.Match(est);
                Assert.True(match.Success);
                LogHandler.Info("IsESTtimeFormat::The element  " + elementName + " is in est time format on the page " + page);                
            }
            catch (Exception e)
            {
                LogHandler.Error("IsESTtimeFormat::Exception - " + e.Message + ". The element  " + elementName + " is not in est time format on the page " + page);
                throw new Exception("IsESTtimeFormat::The element  " + elementName + " is not in est time format on the page " + page);
            }
        }

        /// <summary>
        /// Compares current est time with application actual time.
        /// </summary>
        /// <param name="appTime">the actual time inside the web element</param>
        /// <param name="ESTtime">the expected time inside the web element</param>
        /// <param name="elementName">the element name</param>
        /// <param name="page">the element page name</param>
        protected void CompareWithESTtime(string appTime, string ESTtime, string elementName, string page)
        {
            //Based on Sebastian comments, we should not check the time just the date
            string[] dateString = appTime.Split('/');
            DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
            //FIXME: If the format is dd/MM/yyyy hh:mm, an error could happen due to the minute has changed.
            String appFormatedTime = enter_date.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            Assert.True(appFormatedTime.Contains(ESTtime), "The date: " + ESTtime + " is not contained in: " + appFormatedTime);
        }

        /// <summary>
        /// GetRandomEmailAddress.
        /// </summary>
        /// <returns></returns>
        protected string GetRandomEmailAddress()
        {
            return "test_" + GetRandomStrings(10) + "@automation.com";
        }

        /// <summary>
        /// GenerateRandomName.
        /// </summary>
        /// <param name="size">the size for the random name</param>
        /// <returns></returns>
        protected string GenerateRandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        /// <summary>
        /// To generate random 7 digit number
        /// </summary>
        /// <returns>phone number</returns>
        protected string GenerateContactNumber()
        {
            string number =null;
            Random random = new Random();            
            for (int i = 1; i < 11; i++)
            {
                number += random.Next(1, 9).ToString();
            }
            return number;
        }

        /// <summary>
        /// To generate random 4 digit number
        /// </summary>
        /// <returns>phone number</returns>
        protected string GenerateSSN()
        {
            string number = null;
            Random random = new Random();
            for (int i = 1; i < 5; i++)
            {
                number += random.Next(1, 9).ToString();
            }
            return number;
        }

        /// <summary>
        /// Generate a random string to be used to update text  fields
        /// </summary>
        /// <param name="size">Number of elements in the string</param>
        /// <returns>Random string</returns>
        protected string GetRandomStrings(int size, bool numbersOnly = false)
        {
            var builder = new StringBuilder();
            if (size > 0)
            {
                var random = new Random(DateTime.Now.Millisecond);
                char ch;
                if (numbersOnly)
                    ch = Convert.ToChar(random.Next(8) + 49);
                else
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
                for (int i = 1; i < size; i++)
                {
                    if (numbersOnly)
                        ch = Convert.ToChar(random.Next(9) + 48);
                    else
                        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// This method verifies if the page is opened or not with the specified title.
        /// </summary>
        /// <param name="title">Page Title That to be Verify</param>
        /// <returns></returns>
        public bool IsPageOpened(string title)
        {
            IWebDriver currentWindowHandle = null;
            try
            {
                ReadOnlyCollection<string> windowHandles = DriverManager.GetDriver().WindowHandles;
                foreach (string handle in windowHandles)
                {
                    currentWindowHandle = DriverManager.GetDriver().SwitchTo().Window(handle);
                    if (currentWindowHandle.Url.ToLower().Contains(title.ToLower()) || currentWindowHandle.Title.ToLower().Contains(title.ToLower()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("IsPageOpened::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("IsPageOpened::" + e.Message);
            }
            return false;
        }

        /// <summary>
        /// Message info constructor to add parameters information in the assert message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string MessageInfo(string message, List<string> parameters)
        {
            return String.Format(message + ", Parameters: {0} ", String.Join(",", parameters.ToArray()));
        }
    }
}
