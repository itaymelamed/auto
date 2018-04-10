using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class ManagementPage
    {
        [FindsBy(How = How.CssSelector, Using = ".admin-tools__item-icon--editor")]
        IWebElement EditorButton { get; set; }

        protected BrowserHelper _browserHelper;
        Browser _browser;
        IWebDriver _driver;

        public ManagementPage(Browser browser) 
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void ClickOnEditorButton()
        {
            Base.MongoDb.UpdateSteps("Clicking on the editor button");
            _browserHelper.WaitForElement(EditorButton, nameof(EditorButton));
            _browserHelper.Click(EditorButton, nameof(EditorButton));
        }
    }
}