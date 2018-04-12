using Automation.BrowserFolder;
using Automation.PagesObjects.CasterObjectsFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class AdminPage
    {
        [FindsBy(How = How.CssSelector, Using = "[href*='/castr']")]
        IWebElement caster { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href*=schedulr]")]
        IWebElement schedulr { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href*='/create_post']")]
        IWebElement createPostLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".message")]
        IWebElement postTitle { get; set; }

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
            Base.MongoDb.UpdateSteps($"Click on Castr.");
            _browserHelper.WaitForElement(caster, nameof(caster));
            _browserHelper.Click(caster, nameof(caster));

            return new CastrPage(_browser);
        }

        public SchedulrPage ClickOnSchedulrLink()
        {
            Base.MongoDb.UpdateSteps($"Click on Scedulr.");
            _browserHelper.WaitForElement(schedulr, nameof(schedulr));
            _browserHelper.Click(schedulr, nameof(schedulr));

            return new SchedulrPage(_browser);
        }

        public void ClickOnCreatePost()
        {
            Base.MongoDb.UpdateSteps($"Click on Create Post.");
            _browserHelper.WaitForElement(createPostLink, nameof(createPostLink));
            _browserHelper.ClickJavaScript(createPostLink);
        }

        public string GetPostTitle()
        {
            _browserHelper.WaitForElement(postTitle, nameof(postTitle));
            return postTitle.Text;
        }
    }
}