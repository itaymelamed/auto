using Automation.BrowserFolder;
using Automation.PagesObjects.EchoFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class NewsRoomPage
    {
        [FindsBy(How = How.CssSelector, Using = "[href*='editor/new']")]
        IWebElement editorBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Echo')]")]
        IWebElement echoBtn { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public NewsRoomPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public EditorPage ClickOnEditorBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on editor Btn");
            _browserHelper.WaitForElement(editorBtn,nameof(editorBtn));
            _browserHelper.Click(editorBtn, nameof(editorBtn));
            _browser.SwitchToLastTab();
            return new EditorPage(_browser);
        }

        public bool ValidateEditorBtn()
        {
            Base.MongoDb.UpdateSteps("Validate editor btn.");
            _browserHelper.WaitForElement(editorBtn, nameof(editorBtn));
            return editorBtn.Displayed;
        }

        public EchoPage ClickOnEchoBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on echo Btn");
            _browserHelper.WaitForElement(echoBtn, nameof(echoBtn));
            _browserHelper.Click(echoBtn,nameof(echoBtn));
            return new EchoPage(_browser);
        }
    }
}