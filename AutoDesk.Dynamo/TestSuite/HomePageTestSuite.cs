using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using AutoDesk.Dynamo.TestSuite.Base;
using AutoDesk.Dynamo.PageObject;
using AutoDesk.Framework.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using AutoDesk.Dynamo.DataSource.Core;


namespace AutoDesk.Dynamo.TestSuite
{
    /// <summary>
    /// HomePageTestSuite represents a suite of tests to verify Home page
    /// </summary>
    [TestFixture]
    public class HomePageTestSuite : DynamoBaseTestSuite
    {
        public IWebDriver baseDriver;
        /// <summary>
        /// Test Case 001:Home Page -Check home page.
        /// </summary>
        /// <param name="parameter">the test parameters</param>
        [Test, TestCaseSource(typeof(HomePageTestSuiteDataProviderhelper), "TC002_ValidatetheDynamoHomePage")]
        [Category("Smoke")]

        public void TC001_ValidatetheDynamoHomePage(List<String> parameters)
        {
            Console.WriteLine("Start - TC"); 
            //To get the home of dynamo url.
            HomePage homepage = GetPage<HomePage>();
            String UserName = parameters[0];
            String Password = parameters[1];
            homepage.EntertheUserName(UserName).EnterthePassword(Password).ClickOnSubmit();
            IsElementDisplayed(homepage.Logout, "Logout option", "Home Page");
        }

        //    /// <summary>
        //    /// Test Case 002:Home Page
        //    /// </summary>
        //    /// <param name="parameter">the test parameters</param>

        //[Test]
        //public void TC002_VerifySearchPage()
        //{
        //    var parameter = "spring node";
        //    HomePage homepage = GetPage<HomePage>();
        //    IsElementDisplayed(homepage.DynamoLogo, "Dynamo Logo", "HomePage");
        //    SearchPage searchpage = homepage.ClickOnSearch();
        //    searchpage.EnterSearchValue(parameter).ClickOnDefualtPackage();
        //    IsTextPresent(searchpage.DefualtResultPackage, "spring nodes1", "Spring Nodes1", "Result Package1");

        //}

        }

    }
