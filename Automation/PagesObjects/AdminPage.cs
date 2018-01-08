using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class AdminPage
    {
        [FindsBy(How = How.CssSelector, Using = "[href*='/castr']")]
        IWebElement caster { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public AdminPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public CastrPage ClickOnCasterLink()
        {
            Base.MongoDb.UpdateSteps($"Click on Caster.");
            _browserHelper.WaitForElement(caster, nameof(caster));
            _browserHelper.Click(caster, nameof(caster));

            return new CastrPage(_browser);
        }
    }
}
