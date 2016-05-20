using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AutoDesk.Framework.PageObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDesk.Framework.Environment;
using AutoDesk.Framework.Helper;

namespace AutoDesk.Dynamo.PageObject
{
    public class HomePage: BasePage
    {
        #region WebElement locators

        [FindsBy(How = How.XPath, Using = "//*[@id='header']/topheader/header/div/span/a")]
        public IWebElement DynamoLogo { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='nav_container']/a[1]")]
        public IWebElement BrowseLink { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='nav_container']/a[2]")]
        public IWebElement SearchLink { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='package_stats']/h3")]
        public IWebElement Packages { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='package_stats']/div[1]/p/strong")]
        public IWebElement PackagesNewest { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='package_stats']/div[2]/p/strong")]
        public IWebElement PackagesMostRecent { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='package_stats']/div[3]/p/strong")]
        public IWebElement PackagesMostInstalled { get; private set; }
    
        [FindsBy(How = How.XPath, Using = "//*[@id='author_stats']/h3")]
        public IWebElement Authors { get; private set; }
        
        [FindsBy(How = How.XPath, Using = "//*[@id='browse_container']/div[2]/div/div/h4/a")]
        public IWebElement DynamoVisualProgrammingLink { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/container/undefined/div[1]/div/span[1]/center/a")]
        public IWebElement ExplorePackages { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/container/undefined/div[1]/div/span[2]/center/a")]
        public IWebElement ExploreAuthors { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/container/undefined/div[1]/div/span[3]/center/a")]
        public IWebElement MyPackages { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/container/undefined/div[1]/div/span[4]/center/a")]
        public IWebElement Publish { get; private set; }

        [FindsBy(How = How.XPath, Using =  "//*[@id='log']")]
        public IWebElement UserName { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='pwd']")]
        public IWebElement Password { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='login']")]
        public IWebElement Submit { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='account_logout']/a")]
        public IWebElement Logout { get; private set; }
      
       

        #endregion

         /// <summary>
        /// To pass the driver Instance
        /// </summary>
        public HomePage(IWebDriver Driver)
            : base(Driver)
        {
            GoTo(EnvironmentReader.Base_URL);
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public SearchPage ClickOnSearch()
        {
            Click(SearchLink);
            SleepHelper.Sleep(3);
            return new SearchPage(baseDriver);
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public SearchPage ClickOnExplorePackages()
        {
            Click(ExplorePackages);
            SleepHelper.Sleep(3);
            return new SearchPage(baseDriver);
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public HomePage EntertheUserName(String username)
        {
            TypeText(UserName, username);
            return this;
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public HomePage EnterthePassword(String password)
        {
            TypeText(Password, password);
            return this;
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public HomePage ClickOnSubmit()
        {
            Click(Submit);
            return this;
        }

    }
}
