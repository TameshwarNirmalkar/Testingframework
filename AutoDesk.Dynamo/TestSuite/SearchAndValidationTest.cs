using frontier.IHD.POs;
using Frontier.Framework.Enums;
using Frontier.IHD.PageObject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontier.IHD.TestSuite
{
    public class SearchAndValidationTest : IHDBaseTestSuite
    {
        [Test]
        public void TestSearchbyWTN()
        {
            IHDPage dashboard = GetPage<IHDPage>(Roles.Technician);
            AdvancedSearchPage objAdvanceSearch = dashboard.AdvancedSearchPage();
            objAdvanceSearch.Submit();
            objAdvanceSearch.Continue();
        }
    }
}
