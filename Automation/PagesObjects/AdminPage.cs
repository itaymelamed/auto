using Automation.BrowserFolder;
using Automation.PagesObjects.CasterObjectsFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class AdminPage : BaseObject
    {
        IWebElement castr => FindElement("[href*='/castr']");

        IWebElement schedulr => FindElement("[href*=schedulr]");

        IWebElement createPostLink => FindElement("[href*='/create_post']");

        IWebElement postTitle => FindElement(".message");

        public AdminPage(Browser browser)
            :base(browser)
        {
        }

        public CastrPage ClickOnCasterLink()
        {
            UpdateStep($"Click on Castr.");
            _browserHelper.WaitForElement(() => castr, nameof(castr));
            _browserHelper.Click(castr, nameof(castr));

            return new CastrPage(_browser);
        }

        public SchedulrPage ClickOnSchedulrLink()
        {
            UpdateStep($"Click on Scedulr.");
            _browserHelper.WaitForElement(() => schedulr, nameof(schedulr));
            _browserHelper.Click(schedulr, nameof(schedulr));

            return new SchedulrPage(_browser);
        }

        public void ClickOnCreatePost()
        {
            UpdateStep($"Click on Create Post.");
            _browserHelper.WaitForElement(() => createPostLink, nameof(createPostLink));
            _browserHelper.ClickJavaScript(createPostLink);
        }

        public string GetPostTitle()
        {
            _browserHelper.WaitForElement(() => postTitle, nameof(postTitle));
            return postTitle.Text;
        }
    }
}