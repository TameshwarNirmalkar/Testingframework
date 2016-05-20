using AutoDesk.Framework.Helper;
using AutoDesk.Framework.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDesk.Dynamo.PageObject
{
    public class SearchPage : BasePage
    {
        #region WebElement locators

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/container/search-component/div/div[1]/div[1]/div[1]/input")]
        public IWebElement SearchInput { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='sorting']/span[2]")]
        public IWebElement Name { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='sorting']/span[3]")]
        public IWebElement Group { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='sorting']/span[4]")]
        public IWebElement Votes { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='sorting']/span[5]")]
        public IWebElement Downloads { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='search_container']/div[2]//div/h2")]
        public IWebElement DefualtResultPackage { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='search_container']/div[2]/div[2]")]
        public IWebElement FirstPackage { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[//*[@id=\"navigationcollapse\"]/ul/li[2]/a/h4")]
        public IWebElement ExplorePackagesSearch { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"navigationcollapse\"]/ul/li[3]/a/h4")]
        public IWebElement ExploreAuthorsSearch { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='navigationcollapse']/ul/li[4]/a/h4")]
        public IWebElement MyPackagesSearch { get; private set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='navigationcollapse']/ul/li[5]/a/h4")]
        public IWebElement PublishSearch { get; private set; }
        

        #endregion

         public SearchPage(IWebDriver Driver)
            : base(Driver)
        {
        }

         /// <summary>
         /// To verify the search page.
         /// </summary>
         /// <returns></returns>
         public SearchPage ValidatePackagesInSearch()
         {
             IsElementDisplayed(ExplorePackagesSearch, "Explore Packages", "Search Page");
             IsElementDisplayed(ExploreAuthorsSearch, "Explore Authors", "Search Page");
             IsElementDisplayed(MyPackagesSearch, "My Packages Search", "Search Page");
             IsElementDisplayed(PublishSearch, "Publish search", "Search Page");
             return this;
         }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public SearchPage ClickOnFirstPackage()
        {
            Click(FirstPackage);
            SleepHelper.Sleep(3);
            return new SearchPage(baseDriver);
        }

        /// <summary>
        /// To enter value in the search feild.
        /// </summary>
        public SearchPage EnterSearchValue(String Value)
        {
            TypeText(SearchInput, Value);
            return this;
        }

        
    }
}
