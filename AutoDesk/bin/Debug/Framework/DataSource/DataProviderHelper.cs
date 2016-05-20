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
        /// Generic method to process the csv source file.
        /// </summary>
        /// <param name="csvFile">cCsv file name with partial path</param>
        /// <returns>Role and array of parameters</returns>
        protected static IEnumerable CsvDataSource(string csvFile)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPathCsvFile = dir + "\\DataSource\\CSV\\" + csvFile;
            var reader = new TextFieldParser(fullPathCsvFile);
            reader.SetDelimiters(",");
            bool found = false;
            char[] delimiter = { ',' };
            while (!reader.EndOfData)
            {
                string[] fields = reader.ReadFields();
                if (fields != null)
                {
                    if (!found)
                    {
                        found = true;
                    }
                    List<string> parameters = new List<string>();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        parameters.Add(fields[i]);
                    }
                    yield return new TestCaseData(parameters);
                }

                // Added to avoid failing the test due to not arguments provided
                if (!found)
                {
                    yield return new TestCaseData().Ignore("TC Skipped: The TC does not applies for: ");
                }
            }
        }
        
        /// <summary>
        /// Generic method to process the XLS source file.
        /// </summary>
        /// <param name="SuiteName">TestSuite name to look into</param>
        /// <param name="TCId">This is sheet name in Xls file</param>
        /// <returns></returns>
        protected static IEnumerable XlsDataSource(string SuiteName, string TCId)
        {
            string str;
            string role=null;
            List<string> param = new List<string>();
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string XlsFile = dir + "DataSource\\XLS\\DynamoPackages\\" + SuiteName + ".xlsx";
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
                            str = Convert.ToString(dr[i]);
                            if (i == 0)
                            {
                                role = str;
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

    }
}
    
      

   
   

