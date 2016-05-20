using AutoDesk.Framework.Enums;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using AutoDesk.Framework.Environment;
using Newtonsoft.Json;
using System.IO;
using NUnit.Framework.Constraints;
using System.Configuration;

namespace AutoDesk.Framework.DataSource
{
    public class DataProviderHelper
    {

        /// <summary>
        /// Generic method to process the XLS source file.
        /// </summary>
        /// <param name="SuiteName">TestSuite name to look into</param>
        /// <param name="TCId">This is sheet name in Xls file</param>
        /// <returns></returns>
        protected static IEnumerable XlsDataSource(string SuiteName, string TCId)
        {
            Roles role = Roles.None;
            List<string> param = new List<string>();
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string XlsFile = dir + "\\DataSource\\XLS\\" + SuiteName + ".xlsx";
            DataSet dsExcel = new DataSet();
            string con = "Provider=Microsoft.ACE.OLEDB.12.0;"
                         + "Data Source=" + XlsFile + ";"
                         + // .xls Excel 2003 format 
                         "Extended Properties=\"Excel 8.0;HDR=NO;\"";

            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [" + TCId + "$]", connection);

                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    dr.Read(); //Read the first row and ignore it, as it will contain the column headers
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string str = Convert.ToString(dr[i]);
                            if (i == 0)
                            {
                                role = (Roles) Enum.Parse(typeof (Roles), str);
                            }
                            else
                            {
                                param.Add(str);
                            }
                        }
                        yield return new TestCaseData(role, param);
                    }
                }
            }

        }

        protected static string GetDataSourcePath()
        {
            String baseURL = EnvironmentReader.Base_URL;
            if (baseURL.Contains("01"))
            {
                return "\\DataSource\\CSV\\QAT01\\";
            }
            else if (baseURL.Contains("02"))
            {
                return "\\DataSource\\CSV\\QAT02\\";
            }
            else
            {
                return "\\DataSource\\CSV\\QAT03\\";
            }
        }

        /// <summary>
        /// Generic method to process the csv source file.
        /// </summary>
        /// <param name="csvFile">cCsv file name with partial path</param>
        /// <returns>Role and array of parameters</returns>
        protected static IEnumerable CsvDataSource(string csvFile)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPathCsvFile = dir + GetDataSourcePath() + csvFile;
            var reader = new TextFieldParser(fullPathCsvFile);
            reader.SetDelimiters(",");

            string roles = EnvironmentReader.Roles;
            if (roles != null && roles.Length > 0)
            {
                bool found = false;
                List<string> roleList = null;
                char[] delimiter = { ',' };
                string[] requiredRoles = roles.Split(delimiter);
                roleList = new List<string>(requiredRoles);
                while (!reader.EndOfData)
                {
                    string[] fields = reader.ReadFields();
                    if (fields != null)
                    {
                        Roles role = (Roles)Enum.Parse(typeof(Roles), fields[0]);
                        if (roleList != null && roleList.Contains(role.ToString()))
                        {
                            if (!found)
                            {
                                found = true;
                            }
                            List<string> parameters = new List<string>();
                            for (int i = 1; i < fields.Length; i++)
                            {
                                parameters.Add(fields[i]);
                            }
                            yield return new TestCaseData(role, parameters);
                        }
                    }
                }
                // Added to avoid failing the test due to not arguments provided
                if (!found)
                {
                    yield return new TestCaseData().Ignore("TC Skipped: The TC does not applies for: " +  EnvironmentReader.Roles);
                }
            }
            else
            {
                while (!reader.EndOfData)
                {
                    string[] fields = reader.ReadFields();
                    if (fields != null)
                    {
                        Roles role = (Roles)Enum.Parse(typeof(Roles), fields[0]);
                        List<string> parameters = new List<string>();
                        for (int i = 1; i < fields.Length; i++)
                        {
                            parameters.Add(fields[i]);
                        }
                        yield return new TestCaseData(role, parameters);
                    }
                }
            }
        }
        /// <summary>
        /// Generic method to process the csv source file by Scheme
        /// </summary>
        /// <param name="jsonFile">JSON file name only, no path included</param>
        /// <returns>Role and array of parameters</returns>
        protected static IEnumerable SchemeDataSource(string jsonFile, bool firstLineOnly =true)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            Assert.True(FileSystem.FileExists(dir + "\\DataSource\\Scheme\\" + jsonFile),
                "JSON File not found : " + jsonFile);
            DataSourceScheme dataSourceScheme =
                JsonConvert.DeserializeObject<DataSourceScheme>(
                    File.ReadAllText(dir + "\\DataSource\\Scheme\\" + jsonFile));

            var url = EnvironmentReader.Base_URL;
            string csvPath = "";
            if (url.ToUpper().Contains("QAT01")) csvPath = "DataSource\\Scheme\\QAT01\\";
            if (url.ToUpper().Contains("QAT02")) csvPath = "DataSource\\Scheme\\QAT02\\";
            if (url.ToUpper().Contains("QAT03")) csvPath = "DataSource\\Scheme\\QAT03\\";
            string fullPathCsvFile = dir + "\\" + csvPath;

            foreach (var scenario in dataSourceScheme.Scenarios)
            {
                if (scenario.ParameterFileSource != "")
                {
                    Assert.True(FileSystem.FileExists(fullPathCsvFile + scenario.ParameterFileSource+".csv"),
                        "CSV File source not found : " + scenario.ParameterFileSource+".csv");
                    var reader = new TextFieldParser(fullPathCsvFile + scenario.ParameterFileSource + ".csv");
                    reader.SetDelimiters(",");

                    bool moreLines = true;
                    while (!reader.EndOfData && moreLines)
                    {
                        string[] fields = reader.ReadFields();
                        List<string> parameters = new List<string>();
                        if (fields != null)
                        {
                            foreach (var field in fields)
                            {
                                parameters.Add(field);
                            }
                        }
                        if (firstLineOnly) moreLines = false;
                        if (scenario.Role != Roles.None)
                            yield return new TestCaseData(scenario.Role, parameters);
                        else
                            yield return new TestCaseData(parameters);
                    }
                    reader.Close();
                }
                else
                {
                    if (scenario.Role != Roles.None)
                        yield return new TestCaseData(scenario.Role);
                }
            }
        }
    }

    public class DataSourceScheme
    {
        // The list of scenarios
        public List<Scenarios> Scenarios = new List<Scenarios>();

        public DataSourceScheme()
        {
        }
    }

    public class Scenarios
    {
        public Roles Role = new Roles();
        public string ParameterFileSource = "";
        public Scenarios()
        {
        }
    }
}
