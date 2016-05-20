using Frontier.IHD.PageObject;
using NUnit.Framework;
using Frontier.Framework.Enums;
using OpenQA.Selenium;
using Frontier.IHD.DataSource;
using System.Collections.Generic;

namespace Frontier.IHD.TestSuite
{
    [TestFixture]
    public class AdvancedSearchSuite : IHDBaseTestSuite
    {


        /// <summary>
        /// Task - 15174
        /// TC12333 - Automate  TC - Search by "BTN" field with different number of digits
        /// </summary>
        /// <param name="role"></param>
        /// <param name="dataset">array index: 0 - Customer Type, 1 - BTN </param>

        [Test, TestCaseSource(typeof(DataProviderHelper), "TC12333_SearchByBTNWithDifferentDigits")]
        public void TC12333_SearchByBTNWithDifferentDigits(Roles role, List<string> dataset)
        {
            string BTN = dataset[1];
            // Step 1. Access to IHD URL @URL
            // Step 2. Login as role @Role and Open IHD
            DashboardPage dashboard = GetPage<DashboardPage>(role);
            Assert.IsTrue(ValidateCurrentPageTitle(DashboardPage.Title), "Failed to load Dashboard page");

            // Step 3. Click on Advanced Search button on IHD
            AdvancedSearchPage advanceSearch = dashboard.ClickOnAdvancedSearch();
            Assert.IsTrue(ValidateCurrentPageTitle(AdvancedSearchPage.Title), "Search window is not displayed");

            //Search a customer @customer, enter BTN @BTN in the BTN field
            //Click on Search button
            advanceSearch = advanceSearch.EnterBTN(BTN).Submit();

            //Expected Result : Results are displayed in the grid
            Assert.AreEqual(ElementIsPresent(advanceSearch.SearchResultTable), true, "Users with BTN " + BTN + " displayed in Advance Search Page Table");
        }



        /// <summary>
        /// Task - 15184
        /// TC12512 - Search and Validation - Customer Summary section is displayed
        /// </summary>
        /// <param name="role"></param>
        /// <param name="dataset">array index: 0 - Customer Type, 1 - BTN </param>
        [Test, TestCaseSource(typeof(DataProviderHelper), "TC12512_CustomerSummaryForUserBTN")]
        public void TC12512_CustomerSummaryForUserBTN(Roles role, List<string> dataset)
        {
            string BTN = dataset[1];
            DashboardPage dashboard = GetPage<DashboardPage>(role);
            Assert.IsTrue(ValidateCurrentPageTitle(DashboardPage.Title), "Failed to load Dashboard page");

            AdvancedSearchPage advanceSearch = dashboard.ClickOnAdvancedSearch();

            ValidationSearchPage validate = advanceSearch.EnterBTN(BTN).Submit().SelectUserWithValue(BTN).Continue();
            Assert.AreEqual(ElementIsPresent(validate.CustomerSummaryTable), true);

            // ToDo - Customer Summary data check after RESI is live
        }




        /// <summary>
        /// Task - 15181
        /// TC12512 - Automate TC - Bypass validation button
        /// </summary>
        /// <param name="role"></param>
        /// <param name="dataset">array index: 0 - Customer Type, 1 - Reason , 2 - BTN </param>
        [Test, TestCaseSource(typeof(DataProviderHelper), "TC12482_ByPassValidationForUser")]
        public void TC12482_ByPassValidationForUser(Roles role, List<string> dataset)
        {

            string BTN = dataset[2];
            string Reason = dataset[1];

            DashboardPage dashboard = GetPage<DashboardPage>(role);
            Assert.IsTrue(ValidateCurrentPageTitle(DashboardPage.Title), "Failed to load Dashboard page");

            AdvancedSearchPage advanceSearch = dashboard.ClickOnAdvancedSearch();
            ValidationSearchPage validate = advanceSearch.EnterBTN(BTN).Submit().SelectUserWithValue(BTN).Continue();

            Customer360Page customer360 = validate.ByPassValidation(Reason);
            Assert.IsTrue(ValidateCurrentPageTitle(Customer360Page.Title), "Failed to load Customer 360 Page");


        }

        /// <summary>
        /// Task - 15182
        /// TC12512 - Automate TC - Validation Failed button
        /// </summary>
        /// <param name="role"></param>
        /// <param name="dataset">array index: 0 - Customer Type, 1 - BTN </param>
        [Test, TestCaseSource(typeof(DataProviderHelper), "TC12498_UserValidationFailed")]
        public void TC12498_UserValidationFailed(Roles role, List<string> dataset)
        {
            string BTN = dataset[1];
            DashboardPage dashboard = GetPage<DashboardPage>(role);
            Assert.IsTrue(ValidateCurrentPageTitle(DashboardPage.Title), "Failed to load Dashboard page");

            AdvancedSearchPage advanceSearch = dashboard.ClickOnAdvancedSearch();
            ValidationSearchPage validate = advanceSearch.EnterBTN(BTN).Submit().SelectUserWithValue(BTN).Continue();

            AdvancedSearchPage advSearch = validate.ValidationFailed();
            Assert.AreEqual(ElementIsPresent(advanceSearch.SearchResultTable), true);
            Assert.AreEqual(ValidateCurrentPageTitle(AdvancedSearchPage.Title), true, "Failed to load Search Page");
        }


    }
}
