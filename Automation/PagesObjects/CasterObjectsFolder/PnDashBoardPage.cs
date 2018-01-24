using Automation.BrowserFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class PnDashBoardPage
    {
        [FindsBy(How = How.XPath, Using = "//header//b[contains(text(), 'Premier League')]")]
        IWebElement PremierLeague { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public PnDashBoardPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }
    }
}