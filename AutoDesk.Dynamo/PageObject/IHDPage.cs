using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Frontier.Framework.PageObject;
using Frontier.IHD.PageObject;

namespace frontier.IHD.POs
{
    /// <summary>
    /// IHDPage represents the main page, the Dashboard.
    /// </summary>
    class IHDPage : BasePage
    {
        private String BASE_URL;
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = "#searchRow > table > tbody > tr > td:nth-child(1) > i")]
        private IWebElement AdvancedSearchPageButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[1]")]
        private IWebElement Homepagetitle;

        [FindsBy(How = How.XPath, Using = "//*[@id='SelectedType']")]
        private IWebElement Dashboarddropdown;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[4]/label/b")]
        private IWebElement Mysummarybanner;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[1]/label/b")]
        private IWebElement IHDCasesOwnedbyme;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[5]/div[2]/div/table")]
        private IWebElement IHDCasesOwnedbyTeam;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[2]")]
        private IWebElement Name;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[3]")]
        private IWebElement BTNWTN;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[4]")]
        private IWebElement Category;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[5]")]
        private IWebElement SubCategory;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[6]")]
        private IWebElement Owner;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[7]")]
        private IWebElement DateCreated;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[8]")]
        private IWebElement Status;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[9]")]
        private IWebElement Rootcause;

        [FindsBy(How = How.XPath, Using = "//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/thead/tr/th[10]")]
        private IWebElement Resolution;

        /// <summary>
        /// To validate the content in the IHD home page, the Dashboard.
        /// </summary>
        /// <return>returns the rowcount of given feild<return>.
        public int Validatetable(IWebElement element)
        {
            int rowcount = 0;
            element = driver.FindElement(By.XPath("//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/tbody/tr/td"));
            if (element.Displayed)
            {
                IWebElement webElementHead = driver.FindElement(By.XPath("//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/tbody/tr"));
                IList<IWebElement> ElementCollectionHead = webElementHead.FindElements(By.XPath("//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/tbody/tr/td"));
                rowcount = ElementCollectionHead.Count;
                foreach (IWebElement item in ElementCollectionHead)
                {
                    Console.WriteLine(item.Text);
                }

                for (int numofrows = 1; numofrows <= 140; numofrows++)
                {
                    for (int numofcolumns = 1; numofcolumns <= 10; numofcolumns++)
                    {
                        Console.WriteLine(driver.FindElement(By.XPath("//*[@id='mainContent']/section/section/div[7]/div/div[6]/div/div[2]/div/table/tbody/tr["+numofrows+"]/td["+ numofcolumns + "]")));
                    }
                    Console.ReadLine();
                }
                Console.ReadLine();
                }

            return rowcount;
        }
               
        public IHDPage(IWebDriver Driver) : base(Driver) {
            //ResXResourceSet resx = new ResXResourceSet(@".\TestConfiguration.resx");
            //BASE_URL = resx.GetString("url");
            GoTo(BASE_URL);
        }

        /// <summary>
        /// Goes to advanced search page.
        /// </summary>
        /// <returns>Returns an instance of AdvancedSearchPage</returns>
        public AdvancedSearchPage AdvancedSearchPage()
        {
            click(AdvancedSearchPageButton);
            return new AdvancedSearchPage(baseDriver);
        }

        /// <summary>
        /// Goes to Left Navigation Page (Section).
        /// </summary>
        /// <returns>the LeftNavigationPage</returns>
        //public LeftNavigationPane LeftNavigationPage()
        //{
        //    return new LeftNavigationPane(Driver);
        //}
       

    }
        }