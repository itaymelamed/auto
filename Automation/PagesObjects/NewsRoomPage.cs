using Automation.BrowserFolder;
using Automation.PagesObjects.EchoFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class NewsRoomPage : BaseObject
    {
        IWebElement editorBtn => _browserHelper.FindElement("[href*='editor/new']");

        IWebElement echoBtn => _browserHelper.FindElement("//span[contains(text(), 'Echo')]");

        public NewsRoomPage(Browser browser)
            :base(browser)
        {
        }

        public EditorPage ClickOnEditorBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on editor button");
            _browserHelper.WaitForElement(() => editorBtn,nameof(editorBtn));
            _browserHelper.Click(editorBtn, nameof(editorBtn));
            _browser.SwitchToLastTab();
            return new EditorPage(_browser);
        }

        public bool ValidateEditorBtn()
        {
            Base.MongoDb.UpdateSteps("Validating editor button.");
            _browserHelper.WaitForElement(() => editorBtn, nameof(editorBtn));
            return editorBtn.Displayed;
        }

        public EchoPage ClickOnEchoBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on echo button");
            _browserHelper.WaitForElement(() => echoBtn, nameof(echoBtn));
            _browserHelper.Click(echoBtn,nameof(echoBtn));
            return new EchoPage(_browser);
        }
    }
}