using Automation.BrowserFolder;
using Automation.PagesObjects.EchoFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class NewsRoomPage : BaseObject
    {
        IWebElement editorBtn => _browserHelper.FindElement("[href*='editor/new']");

        IWebElement echoBtn => _browserHelper.FindElement(By.XPath("//span[contains(text(), 'Echo')]"), "Echo Button", 0);

        public NewsRoomPage(Browser browser)
            :base(browser)
        {
        }

        public EditorPage ClickOnEditorBtn()
        {
            UpdateStep("Clicking on editor button");
            _browserHelper.WaitForElement(() => editorBtn,nameof(editorBtn));
            _browserHelper.Click(editorBtn, nameof(editorBtn));
            _browser.SwitchToLastTab();
            return new EditorPage(_browser);
        }

        public EchoPage ClickOnEchoBtn()
        {
            UpdateStep("Clicking on echo button");
            _browserHelper.WaitForElement(() => echoBtn, nameof(echoBtn));
            _browserHelper.Click(echoBtn, nameof(echoBtn));
            return new EchoPage(_browser);
        }

        public bool ValidateEditorBtn()
        {
            UpdateStep("Validating editor button.");
            _browserHelper.WaitForElement(() => editorBtn, nameof(editorBtn));
            return editorBtn.Displayed;
        }
    }
}