using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using AutoDesk.Framework.Waits;
using AutoDesk.Framework.Log;

namespace AutoDesk.Framework.PageObject
{
    /// <summary>
    /// BasePage.GetElement provides mechanism to get information from specific web elements.
    /// </summary>
    public abstract partial class BasePage
    {

        /// <summary>
        /// Gets dropdown list items content.
        /// </summary>
        /// <param name="dropDown">the dropdown web element</param>
        /// <returns>list items content.</returns>
        public List<string> GetDropdownListItems(IWebElement dropDown)
        {
            List<string> data = new List<string>();
            SelectElement selectList = GetSelectElement(dropDown);
            IList<IWebElement> elements = selectList.Options;
            foreach (IWebElement element in elements)
            {
                data.Add(element.Text);
            }
            LogHandler.Info("GetDropdownListItems::Successfully retrieved data from dropdown");
            return data;
        }


        /// <summary>
        /// Gets the current dropdown selection.
        /// </summary>
        /// <param name="dropDown">Combobox locator to get value from</param>
        /// <returns>the <see cref="IWebElement"/></returns>
        public IWebElement GetDropdownSelectedValue(IWebElement dropDown)
        {
            return GetSelectElement(dropDown).SelectedOption;
        }

        /// <summary>
        /// Gets web element as a Select.
        /// </summary>
        /// <param name="element">the web element</param>
        /// <returns>the <see cref="SelectElement"/></returns>
        protected SelectElement GetSelectElement(IWebElement element)
        {
            WaitsHandler.WaitForElementToBeVisible(baseDriver, element, "", "");
            return new SelectElement(element);
        }

        /// <summary>
        /// Gets the values from the dropdownlist.
        /// </summary>
        /// <returns>a list of all the values from the dropdownlist</returns>
        public List<string> GetDropdownValues(IWebElement elementlist)
        {
            //TODO: Can we remove it? we already have GetDropdownListItems
            List<string> data = new List<string>();
            IReadOnlyCollection<IWebElement> elements = elementlist.FindElements(By.TagName("li"));
            foreach (IWebElement element in elements)
            {
                data.Add(element.Text);
            }
            return data;
        }
    }
}
