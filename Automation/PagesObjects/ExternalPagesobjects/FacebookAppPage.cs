using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class FacebookAppPage
    {
        [FindsBy(How = How.CssSelector, Using = "[role='article']")]
         IList<IWebElement> posts { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public FacebookAppPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public IWebElement SearchPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Serach post in Facebook.");
            var post = _browserHelper.ExecutUntillTrue(() => posts.ToList().Where(p => p.FindElement(By.XPath(".//p")).Text == title).FirstOrDefault(), "Facebook post was not created.");
            return post;
        }

        public string GetPostTitle(string title)
        {
            return SearchPost(title).Text;
        }

        public IWebElement GetPostBody(string title)
        {
            Base.MongoDb.UpdateSteps($"Serach post in body.");
            return SearchPost(title).FindElement(By.XPath($".//a[contains(text(),'{title}')]"));
        }

        public bool VlidatePostDetails(string title)
        {
            Base.MongoDb.UpdateSteps($"Validate post context.");
            return GetPostBody(title).GetAttribute("value").Contains(title);
        }

        public PostPage ClickOnPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Click On facebook Post.");
            _browserHelper.Click(GetPostBody(title), "FacebookPost");
            _browser.SwitchToLastTab();
            return new PostPage(_browser); 
        }
    }
}
