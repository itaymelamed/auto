using System.Collections.Generic;
using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.Influencers
{
    public class ReportsPage : BaseObject
    {
        List<IWebElement> _influencers => FindElements("tbody tr");

        IWebElement _totalSessionsValue => FindElement(By.XPath("(//*[@class='large-2 columns'])[1]//div"));

        IWebElement _activeInfluencerValue => FindElement(By.XPath("(//*[@class='large-2 columns'])[2]//div"));

        IWebElement _totalAdjustmentsValue => FindElement(By.XPath("(//*[@class='large-2 columns'])[3]//div"));

        IWebElement _totalEaringsValue => FindElement(".end div");

        public ReportsPage(Browser browser)
            :base(browser)
        {
        }
    }
}