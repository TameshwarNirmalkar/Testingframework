using NUnit.Framework;
using frontier.IHD.POs;
using Frontier.IHD.PageObject;
using Frontier.Framework.Enums;
using Frontier.IHD.DataSource;
using OpenQA.Selenium;
using System.Threading;

namespace Frontier.IHD.TestSuite
{
    [TestFixture]
    public class TC15189SearchByWTN : IHDBaseTestSuite
    {
        [Test, TestCaseSource(typeof(DataProviderHelper), "TC15189SearchByWTNData")]
        public void SearchbyWTNTest(Roles role, string WTN)
        {
            DashboardPage dashboard = GetPage<DashboardPage>(role);
            AdvancedSearchPage objAdvanceSearch = dashboard.GetAdvancedSearchPage().EnterWTN(WTN).Submit();
            Thread.Sleep(90000);
            IWebElement searchResultsGrid = objAdvanceSearch.GetSearchResultElement();
            Assert.NotNull(searchResultsGrid, "Results are displayed in the grid");
        }
    }
}
