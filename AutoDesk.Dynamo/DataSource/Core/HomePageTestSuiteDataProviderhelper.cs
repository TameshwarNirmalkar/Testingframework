using AutoDesk.Framework.DataSource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDesk.Dynamo.DataSource.Core
{
    class HomePageTestSuiteDataProviderhelper : DataProviderHelper
    {
        /// <summary>
        /// TC001_ValidatetheDynamoHomePage
        /// </summary>
        /// <returns>Array of parameters</returns>
        public static IEnumerable TC001_ValidatetheDynamoHomePage()
        {
            return CsvDataSource("HomePageSuite\\HomePageSuite.csv");
        }

        public static IEnumerable TC002_ValidatetheDynamoHomePage()
        {
            return CsvDataSource("HomePageSuite\\HomePageSuite.csv");
        }

       
    }
}
