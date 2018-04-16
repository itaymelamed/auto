using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class ManagementPage : BaseObject
    {
        IWebElement EditorButton => FindElement(".admin-tools__item-icon--editor");

        public ManagementPage(Browser browser)
            :base(browser)
        {
        }

        public void ClickOnEditorButton()
        {
            Base.MongoDb.UpdateSteps("Clicking on the editor button");
            _browserHelper.WaitForElement(() => EditorButton, nameof(EditorButton));
            _browserHelper.Click(EditorButton, nameof(EditorButton));
        }
    }
}