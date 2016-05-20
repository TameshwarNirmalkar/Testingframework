using NUnit.Framework;
using Frontier.Framework.Proxy;
using Frontier.Framework.Enums;
using frontier.IHD.POs;
using System.Threading;
using Frontier.IHD.PageObject;

namespace Frontier.IHD.TestSuite
{
    /// <summary>
    /// TC15185_AddNewLeadButtonTest verifies the functionality of Add New Lead Button in the IHD page 
    /// </summary>
    [TestFixture]
    public class TC15185AddNewLeadButtonTest : IHDBaseTestSuite
    {
        /// <summary>
        /// The setup method, stops the proxy.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            ProxyManager.StopProxies();
        }

        [Test]
        public void AddNewLeadButtonTestForTechnician()
        {
            DashboardPage dashboard = GetPage<DashboardPage>(Roles.Technician);
            AdvancedSearchPage objAdvanceSearch =  dashboard.GetAdvancedSearchPage();
            objAdvanceSearch.EnterWTN("2177683648.0");
            objAdvanceSearch.Submit();
        }

        
    }
}
