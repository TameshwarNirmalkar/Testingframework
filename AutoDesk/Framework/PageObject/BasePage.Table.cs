using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using AutoDesk.Framework.Log;
using System.Globalization;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage.Table provides different operations to interact with web element tables, row or columns.
    /// </summary>
    public abstract partial class BasePage
    {

        /// <summary>
        /// Gets the table's cell text from a specific table.
        /// </summary>
        /// <param name="table">WebElement of the table grid</param>
        /// <param name="rowNumber">Number of row starting on zero ( 0 ), title included</param>
        /// <param name="colNumber">Number of column starting on zero ( 0 ), check box included</param>
        /// <returns>Text content in the cell</returns>
        public string GetCellTextOnTableData(IWebElement table, int rowNumber, int colNumber)
        {
            string cellText = "";
            IReadOnlyCollection<IWebElement> bodyRows = FindSubElements(table, By.TagName("tr"));
            if (bodyRows.Count > 1)
            {
                IReadOnlyCollection<IWebElement> bodyCols = FindSubElements(bodyRows.ElementAt(rowNumber), By.TagName("td"));
                IWebElement cellBody = bodyCols.ElementAt(colNumber);
                cellText = cellBody.Text;
            }
            else
            {
                throw new Exception("GetCellTextOnTableData:: The table is empty.");
            }
            return cellText;
        }

        /// <summary>
        /// Gets the array of cell values in the Grid.
        /// </summary>
        /// <param name="table">IWebElement</param>
        /// <returns>YTwo dimensional array of strings</returns>
        public static string[][] GetAllCellValuesInGrid(IWebElement table)
        {
            string[][] cellArray;
            try
            {
                var tbody = table.FindElements(By.TagName("tbody"));
                ReadOnlyCollection<IWebElement> rows = tbody[0].FindElements(By.TagName("tr"));
                cellArray = rows.Select(x => x.FindElements(By.TagName("td")).Select(y => y.Text).ToArray()).ToArray();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return cellArray;
        }

        /// <summary>
        /// Gets the Collection of Rows as WebElements in the Grid.
        /// </summary>
        /// <param name="table">IWebElement</param>
        /// <returns>ReadOnlyCollection<IWebElement> of Rows</returns>
        public ReadOnlyCollection<IWebElement> GetAllRowsInTheGrid(IWebElement table)
        {
            var rows = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            try
            {
                var tbody = table.FindElements(By.TagName("tbody"));
                rows = tbody[0].FindElements(By.TagName("tr"));
            }
            catch (Exception)
            {
            }
            return rows;
        }

        /// <summary>
        /// Searches and click a row with 1 value matching.
        /// </summary>
        /// <param name="element">Table data</param>
        /// <param name="value">Value to search for </param>
        public void SelectTableRowByValue(IList<IWebElement> element, String value)
        {
            try
            {
                foreach (IWebElement data in element)
                {
                    if (data.Text.Contains(value))
                    {
                        data.Click();
                    }
                }
            }
            catch (WebDriverException e)
            {
                LogHandler.Error("SelectTableRowByValue::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("SelectTableRowByValue::" + e.Message);
            }
        }

        /// <summary>
        /// It Double Clicks On a provided row in a table.
        /// </summary>
        /// <param name="body">the table body</param>
        /// <param name="rowNo">the table row</param>
        public void DoubleClickTableRowByRowNo(IWebElement body, int rowNo)
        {
            IReadOnlyCollection<IWebElement> bodyRows = body.FindElements(By.TagName("tr"));
            for (int i = 1; i < bodyRows.Count; i++)
            {
                if (i == rowNo)
                {
                    DoubleClick(bodyRows.ElementAt(i));
                }
            }
        }

        /// <summary>
        /// Get the No. of Rows of the table on the page
        /// </summary>
        /// <param name="body">Table body element</param>
        /// <param name="rowNo">Table Row No to be selected</param>
        /// <returns>RowElement</returns>
        public int GetTableRowsOnPage(IWebElement body)
        {
            IReadOnlyCollection<IWebElement> bodyRows = body.FindElements(By.TagName("tr"));
            return bodyRows.Count;          
        }
        /// <summary>
        /// Double click on the row if the value matchs in the row.
        /// </summary>
        /// <param name="element"> the table</param>
        /// <param name="value">value to serached in the table</param>
        public void DoubleClickRowByValue(IWebElement element, string value)
        {
            IList<IWebElement> rows = element.FindElements(By.XPath("//tr"));
            try
            {
                foreach (IWebElement row in rows)
                {
                    if (row.Text.Contains(value))
                    {
                        IList<IWebElement> columns = row.FindElements(By.XPath("//td"));
                        DoubleClick(columns[1]);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("DoubleClickRowByValue::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("DoubleClickRowByValue::" + e.Message);
            }

        }

        /// <summary>
        /// This function Double click on the row after match in the properties
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="values">List of values</param>
        public void DoubleClickRowByValue(IWebElement table, List<string> values)
        {
            IReadOnlyCollection<IWebElement> categoryRowValues = table.FindElements(By.TagName("tr"));         
            try
            {
                
                for (int i = 0; i < categoryRowValues.Count; i++)
                {
                    IWebElement categoryRow = categoryRowValues.ElementAt(i);
                    if (categoryRow != null)
                    {
                        bool found = true;
                        for (int j = 0; j < values.Count && found; j++)
                        {
                            if (!categoryRow.Text.ToLower().Contains(values[j].ToLower()))
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            DoubleClick(categoryRow);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Error("DoubleClickRowByValue::NoSuchElementException - " + e.Message);
                throw new NoSuchElementException("DoubleClickRowByValue::" + e.Message);
            }

        }


        /// <summary>
        /// Clicks on the table row.
        /// </summary>
        /// <param name="body">Table body element</param>
        /// <param name="rowNo">Table Row No to be selected</param>
        public void SelectTableRowByRowNo(IWebElement body, int rowNo)
        {
            // TODO: This method seems duplicated. 
            IsElementDisplayed(body, "", "");
            IReadOnlyCollection<IWebElement> bodyRows = body.FindElements(By.TagName("tr"));
            for (int i = 1; i < bodyRows.Count; i++)
            {
                if (i == rowNo)
                {
                    Click(bodyRows.ElementAt(i));
                }
            }
        }

        /// <summary>
        /// Get the Table Row Element by scanning all pages of Table Grid
        /// </summary>
        /// <param name="table">table web element</param>
        /// <param name="cellValues">a list of expected cell values</param>
        /// <param name="pageName">the page name</param>    
        /// <param name="GridNextPage">Next button of Pagination control of Table Grid</param>
        /// <returns>Element if present else null</returns>
        public IWebElement GetTableRowByScanningAllPages(IWebElement table, List<String> cellValues, String pageName, IWebElement GridNextPage)
        {
            try
            {
                bool IsRowPresentInCurrentPage = false;
                List<string> previousPageRows = new List<string>();
                List<string> currPageRows = new List<string>();
                do
                {
                    IReadOnlyCollection<IWebElement> row = table.FindElements(By.TagName("tr"));
                    int totalRowsScanned = 0;
                    List<string> currentPageRows = null;
                    String temp = null;
                    for (int rowNo = 0; rowNo < row.Count; rowNo++)
                    {
                        IWebElement element = row.ElementAt(rowNo);
                        bool IsCellValuePresentInRow = true;
                        currentPageRows = new List<string>();                       
                        for (int i = 0; i < cellValues.Count; i++)
                        {
                            if (!element.Text.ToLower().Contains(cellValues[i].ToLower()))
                            {
                                IsCellValuePresentInRow = false;
                            }
                            temp = cellValues[i];
                        }
                        currentPageRows.Add(element.Text);
                        currPageRows.Add(element.Text);
                        if (IsCellValuePresentInRow)
                        {
                            IsRowPresentInCurrentPage = true;
                            LogHandler.Info("GetTableRowByScanningAllPages::Row element with Cell value found: " + temp);
                            return element;
                        }
                        totalRowsScanned++;
                    }
                    //if the value is not found in the current page, move to the next page of the table grid
                    if (row.Count != 1 && totalRowsScanned == row.Count && (!currPageRows.SequenceEqual(previousPageRows)))
                    {
                        Click(GridNextPage);
                        previousPageRows = new List<string>(currPageRows);
                        currPageRows.Clear();
                    }
                    else
                    {
                        //if the value is not present in the table rows in all pages, then break and return false
                        LogHandler.Error("GetTableRowByScanningAllPages::cell value:"+temp+" is not present in the table rows in all pages of table");
                        IsRowPresentInCurrentPage = false;
                        break;
                    }

                } while (!IsRowPresentInCurrentPage);
            }
            catch (Exception ex)
            {
                LogHandler.Error("ScanGridPagesAndVerifyTableRow::Exception - " + ex.Message);
            }
            return null;
            throw new Exception("ScanGridPagesAndVerifyTableRow::value not found in Table");
        }


        /// <summary>
        /// Get the Table Row Element by scanning all pages of Table Grid
        /// </summary>
        /// <param name="table">table web element</param>
        /// <param name="cellValues">a list of expected cell values</param>
        /// <param name="pageName">the page name</param>    
        /// <param name="GridNextPage">Next button of Pagination control of Table Grid</param>
        /// <returns>Element if present else null</returns>
        public bool ValidatePagination(IWebElement table, int PaginationOpt, String pageName, IWebElement GridNextPage)
        {
            try
            {
                IReadOnlyCollection<IWebElement> row;                
                bool flag = false;
                IWebElement fr;
                IWebElement tr=null;
                do
                {
                    row = table.FindElements(By.TagName("tr"));
                    int rows = (row.Count - 1);
                    if (rows == PaginationOpt)
                    {
                        Click(GridNextPage);                        
                        row = table.FindElements(By.TagName("tr"));
                        fr= GetTableRowByRowNo(table, 6);
                        fr = tr;
                        if (fr == tr)
                        {
                            flag = true;
                            LogHandler.Info("ValidatePagination::For pagination option '" + PaginationOpt + "'- Passed");
                            return flag;
                        }
                        flag = true;
                    }
                    else
                    {
                        if (rows <= PaginationOpt)
                        {
                            flag = true;
                            LogHandler.Info("ValidatePagination::For pagination option '" + PaginationOpt + "'- Passed");
                            return flag;
                        }
                        else
                        {
                            return false;
                        }
                    }
                } while (row.Count == (PaginationOpt + 1));                
                return flag;
            }
            catch (Exception ex)
            {
                LogHandler.Error("ValidatePagination::Exception - " + ex.Message);
            }
            return false;
        }


        /// <summary>
        /// Get the Table Column Element by scanning all pages of Table Grid
        /// </summary>
        /// <param name="tableRow">the row element</param>
        /// <param name="columnNo">the column no</param>
        /// <returns>Element if present else null</returns>
        public IWebElement GetTableColumnByScanningAllPages(IWebElement tableRow, int columnNo)
        {
            try
            {
                if (tableRow != null)
                {
                    //SleepHelper.Sleep(3);
                    IReadOnlyCollection<IWebElement> bodyRowColumns = tableRow.FindElements(By.TagName("td"));
                    for (int j = 0; j < bodyRowColumns.Count; j++)
                    {
                        IWebElement elementColumn = bodyRowColumns.ElementAt(j);
                        if (elementColumn.Displayed)
                        {
                            if (j == columnNo)
                            {
                                return elementColumn;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error("ScanGridPagesAndVerifyTableRow::Exception - " + ex.Message);
            }
            return null;
        }


        /// <summary>
        /// Get the Row of the table by row number
        /// </summary>
        /// <param name="body">Table body element</param>
        /// <param name="rowNo">Table Row No to be selected</param>
        /// <returns>RowElement</returns>
        public IWebElement GetTableRowByRowNo(IWebElement body, int rowNo)
        {
            IReadOnlyCollection<IWebElement> bodyRows = body.FindElements(By.TagName("tr"));
            for (int i = 1; i < bodyRows.Count; i++)
            {
                if (i == rowNo)
                {
                    return bodyRows.ElementAt(i);
                }
            }
            return null;
        }

        /// <summary>
        /// Verifies if the table contains a row with expected cell values.
        /// </summary>
        /// <param name="table">table web element</param>
        /// <param name="cellValues">a list of expected cell values</param>
        /// <param name="pageName">the page name</param>  
        /// <param name="GridNextPage">Next button of Pagination control of Table Grid</param>
        /// <returns>True if Row is found/False if Row is not found</returns>
        public bool IsRowWithExpValuesPresentInTable(IWebElement table, List<String> cellValues, String pageName, IWebElement GridNextPage)
        {
            bool IsRowPresentInCurrentPage = false;
            try
            {
                List<string> previousPageRows = new List<string>();
                List<string> currPageRows = new List<string>();
                do
                {
                    IReadOnlyCollection<IWebElement> row = table.FindElements(By.TagName("tr"));
                    int totalRowsScanned = 0;

                    for (int rowNo = 0; rowNo < row.Count; rowNo++)
                    {
                        IWebElement element = row.ElementAt(rowNo);
                        bool IsCellValuePresentInRow = true;
                        for (int i = 0; i < cellValues.Count; i++)
                        {
                            if (!element.Text.ToLower().Contains(cellValues[i].ToLower()))
                            {
                                IsCellValuePresentInRow = false;
                            }
                        }
                        currPageRows.Add(element.Text);
                        if (IsCellValuePresentInRow)
                        {
                            IsRowPresentInCurrentPage = true;
                            return true;
                        }
                        totalRowsScanned++;
                    }
                    //if the value is not found in the current page, move to the next page of the table grid
                    if (totalRowsScanned == row.Count && (!currPageRows.SequenceEqual(previousPageRows)))
                    {
                        Click(GridNextPage);
                        previousPageRows = new List<string>(currPageRows);
                        currPageRows.Clear();
                    }
                    else
                    {
                        //if the value is not present in the table rows in all pages, then break and return false
                        IsRowPresentInCurrentPage = false;
                        break;
                    }

                } while (!IsRowPresentInCurrentPage);
            }
            catch (Exception ex)
            {
                LogHandler.Error("ScanGridPagesAndVerifyTableRow::Exception - " + ex.Message);
            }
            return IsRowPresentInCurrentPage;
        }

        /// <summary>
        /// Is Data present in the Row of the Table
        /// </summary>
        /// <param name="row">Table row element</param>
        /// <param name="data">Data to be verified</param>
        /// <returns>true-if data is present in the row and false-if not present</returns>
        public bool IsDataPresentInRow(IWebElement row, List<string> data)
        {
            bool IsCellValuePresentInRow = true;
            for (int i = 0; i < data.Count; i++)
            {
                if (!row.Text.ToLower().Contains(data[i].ToLower()))
                {
                    IsCellValuePresentInRow = false;
                }
            }
            if (IsCellValuePresentInRow)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// private method sort the column data
        /// </summary>
        /// <param name="GridTable"></param>
        /// <param name="ColumnName"></param>
        /// <param name="IsDesc"></param>
        /// <param name="Message"></param>
        /// <param name="SortedData"></param>
        /// <returns></returns>
        public bool Sorting(IWebElement GridTable, string ColumnName, bool IsDesc, string Message)
        {
            try
            {
                var TableHeaderList = GridTable.FindElements(By.XPath("//thead/tr/th"));
                var TableRowData = GridTable.FindElements(By.XPath("//tbody/tr"));

                var TableHeaderclickable = TableHeaderList.FirstOrDefault(x => x.Text == ColumnName);
                var index = TableHeaderList.IndexOf(TableHeaderList.FirstOrDefault(x => x.Text == ColumnName));

                List<string> data = new List<string>();

                if (TableHeaderclickable != null)
                {
                    Click(TableHeaderclickable);
                    if (IsDesc)
                    {
                        Click(TableHeaderclickable);
                    }
                    foreach (var item in TableRowData)
                    {
                        var coldata = item.FindElements(By.XPath("//td"));
                        for (int i = 0; i < coldata.Count; i++)
                        {
                            if (i == index)
                            {
                                data.Add(coldata[i].Text);
                            }
                        }
                    }
                }
                else
                {
                    Message = "No header found";
                    return false;
                }
                IList<string> ordering = null;
                if (data.Count > 0)
                {
                    if (IsDesc)
                    {
                        ordering = data.OrderByDescending(x => x.ToString(CultureInfo.InvariantCulture)).ToList();
                    }
                    else
                    {

                        ordering = data.OrderBy(x => x.ToString(CultureInfo.InvariantCulture)).ToList();
                    }
                }

                return data.SequenceEqual(ordering);
            }
            catch (Exception)
            {
                Message = "No data found";
                return false;
            }
        }

        /// <summary>
        /// Get the Row of the table by row values
        /// </summary>
        /// <param name="body">Table body element</param>
        /// <param name="rowNo">Table Row values to be selected</param>
        /// <returns>RowElement</returns>
        public IWebElement GetTableRowByValue(IWebElement table, List<String> cellValues, String pageName)
        {
            try
            {
                bool IsRowPresent = false;
                do
                {
                    IReadOnlyCollection<IWebElement> row = table.FindElements(By.TagName("tr"));

                    for (int rowNo = 0; rowNo < row.Count; rowNo++)
                    {
                        IWebElement element = row.ElementAt(rowNo);
                        bool IsCellValuePresentInRow = true;
                        for (int i = 0; i < cellValues.Count; i++)
                        {
                            if (!element.Text.ToLower().Contains(cellValues[i].ToLower()))
                            {
                                IsCellValuePresentInRow = false;
                            }
                        }
                        if (IsCellValuePresentInRow)
                        {
                            IsRowPresent = true;
                            return element;
                        }

                    }
                } while (!IsRowPresent);
            }
            catch (Exception ex)
            {
                LogHandler.Error("VerifyTableRow::Exception - " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Gets the List of Column Header Element of a Table
        /// </summary>
        public List<IWebElement> GetTableColumnHeaderElementList(IWebElement table)
        {
            List<IWebElement> lstColumnHeader = new List<IWebElement>();
            IReadOnlyCollection<IWebElement> bodyRowColumns = table.FindElements(By.TagName("th"));
            for (int j = 0; j < bodyRowColumns.Count; j++)
            {
                lstColumnHeader.Add(bodyRowColumns.ElementAt(j));
            }
            return lstColumnHeader;
        }

        /// <summary>
        /// Gets the Column Values of all Rows displayed on the page
        /// </summary>
        public List<string> GetColumnValuesOfAllRows(IWebElement table, int columnIndex)
        {
            IReadOnlyCollection<IWebElement> row = table.FindElements(By.TagName("tr"));
            List<string> rowData = new List<string>();
            for (int rowNo = 1; rowNo < row.Count; rowNo++)
            {
                IWebElement element = row.ElementAt(rowNo);
                rowData.Add(GetTableColumnByScanningAllPages(element, columnIndex).Text);
            }
            return rowData;
        }

        /// <summary>
        /// Verifies if the Table Column is sorted
        /// </summary>
        /// <param name="table">Table on which Sorting has to be performed</param>
        /// <param name="column">Column Element to be sorted</param>
        /// <param name="columnIndex">Column Index to be sorted</param>
        /// <param name="IsDesc">OrderBy</param>
        /// <returns>True=Sorted successfully; False=Sorting Unsucessfull</returns>
        public bool Sort(IWebElement table, IWebElement column, int columnIndex, bool IsDesc)
        {
            double number;
            DateTime dt;
            try
            {
                Click(column);
                if (IsDesc)
                {
                    Click(column);
                }
                var lstColumnValues = GetColumnValuesOfAllRows(table, columnIndex);
                if (double.TryParse(lstColumnValues.First().ToString(),out number))
                {
                    List<double> lstSortedColumnValues = null;
                    if (lstColumnValues.Count > 0)
                    {
                        if (IsDesc)
                            lstSortedColumnValues = ConvertListToDouble(lstColumnValues).OrderByDescending(x => x).ToList();
                        else
                            lstSortedColumnValues = ConvertListToDouble(lstColumnValues).OrderBy(x => x).ToList();
                    }
                    return ConvertListToDouble(lstColumnValues).SequenceEqual(lstSortedColumnValues);
                }
                else if (DateTime.TryParse(lstColumnValues.First().ToString(),out dt))
                {
                    IList<DateTime> lstSortedColumnValues = null;
                    if (lstColumnValues.Count > 0)
                    {
                        if (IsDesc)
                            lstSortedColumnValues = ConvertListToDateTime(lstColumnValues).OrderByDescending(x => x).ToList();
                        else
                            lstSortedColumnValues = ConvertListToDateTime(lstColumnValues).OrderBy(x => x).ToList();
                    }
                    return ConvertListToDateTime(lstColumnValues).SequenceEqual(lstSortedColumnValues);
                }
                else
                {
                    IList<string> lstSortedColumnValues = null;
                    if (lstColumnValues.Count > 0)
                    {
                        if (IsDesc)
                            lstSortedColumnValues = lstColumnValues.OrderByDescending(x => x.ToString(CultureInfo.InvariantCulture)).ToList();
                        else
                            lstSortedColumnValues = lstColumnValues.OrderBy(x => x.ToString(CultureInfo.InvariantCulture)).ToList();
                    }
                    return lstColumnValues.SequenceEqual(lstSortedColumnValues); ;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Converts list of string to double
        /// </summary>
        public List<double> ConvertListToDouble(List<String> lstOfValues)
        {
            List<double> lstOfDoubleValues = new List<double>();
            foreach (var value in lstOfValues)
            {
                if (!String.IsNullOrEmpty(value))
                    lstOfDoubleValues.Add(Convert.ToDouble(value));
                else
                    lstOfDoubleValues.Add(0.00);
            }
            return lstOfDoubleValues;
        }

        /// <summary>
        /// Converts list of String to DateTime
        /// </summary>
        public List<DateTime> ConvertListToDateTime(List<String> lstOfValues)
        {
            string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd","M/d/yyyy hh:mm:ss tt", "M-d-yyyy", "MM-dd-yyyy","d/M/yyyy mm:ss tt",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "MM/dd/yyyy hh:mm:ss", "d/M/yyyy h:mm:ss tt", "d/M/yyyy hh:mm:ss tt","MM/dd/yyyy mm:ss tt"};
            List<DateTime> lstOfDateTime = new List<DateTime>();
            foreach (var value in lstOfValues)
            {
                if (!String.IsNullOrEmpty(value))
                   lstOfDateTime.Add(DateTime.ParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
                else
                    lstOfDateTime.Add(default(DateTime).Date);
            }
            return lstOfDateTime;
        }

    }
}
