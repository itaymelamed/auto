using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class TwitterAppPage
    {
        [FindsBy(How = How.CssSelector, Using = ".content")]
        IList<IWebElement> tweets { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public TwitterAppPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        IWebElement SearchTweet(string title)
        {
            BaseUi.MongoDb.UpdateSteps($"Search Tweet: {title}.");
            return _browserHelper
                .ExecutUntillTrue(() => tweets.ToList().Where(t => t.FindElement(By.XPath(".//p")).Text.Contains(title)).FirstOrDefault());
        }

        public bool ValidateTweetByTitle(string title)
        {
            return SearchTweet(title).Displayed;
        }

        public PostPage ClickOnTweetLink(string title)
        {
            var link = _browserHelper.ExecutUntillTrue(() => SearchTweet(title).FindElement(By.XPath($".//a[title*='{BaseUi._config.Env.ToString().ToLower()}']")));
            _browserHelper.Click(link, "Twiter Link");

            return new PostPage(_browser); 
        }
    }
}
