using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.PagesObjects.Influencers
{
    public class BrowseAffiliatesPage : BaseObject
    {
        SelectElement _statusDropBox => new SelectElement(FindElement("#filters_status"));

        SelectElement _managerDropBox => new SelectElement(FindElement("#filters_manager_id"));

        SelectElement _countryDropBox => new SelectElement(FindElement("#filters_manager_id"));

        IWebElement _filterBtn => FindElement(".filter");

        List<IWebElement> _affiliates => FindElements("tbody tr");

        public enum Status { waiting, pending, trial, active, blocked, declined };

        public BrowseAffiliatesPage(Browser browser)
            :base(browser)
        {
        }

        public void ClickonFilterbtn()
        {
            UpdateStep($"Clicking on filter button.");
            _browserHelper.Click(_filterBtn);
        }

        public BrowseAffiliatesPage SelectStatus(Status status)
        {
            UpdateStep($"Selecting status: {status} from status drop box.");
            _statusDropBox.SelectByText(status.ToString());
            ClickonFilterbtn();

            return new BrowseAffiliatesPage(_browser);
        }

        public BrowseAffiliatesPage SelectManager(string manager)
        {
            UpdateStep($"Selecting manager: {manager} from manager drop box.");
            _managerDropBox.SelectByText(manager);
            ClickonFilterbtn();

            return new BrowseAffiliatesPage(_browser);
        }

        public BrowseAffiliatesPage SelectCountry(string country)
        {
            UpdateStep($"Selecting country: {country} from country drop box.");
            _countryDropBox.SelectByText(country);
            ClickonFilterbtn();

            return new BrowseAffiliatesPage(_browser);
        }

        List<string> GetColData(int i)
        {
            UpdateStep("Getting al statuses.");
            _browserHelper.WaitUntillTrue(() => _affiliates.Count > 0);
            return _affiliates.Select(a => a.FindElements(By.XPath("(.//tr")).ElementAt(i).Text).ToList();
        }
    }
}