using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class TwitterAppPage
    {
        [FindsBy(How = How.CssSelector, Using = "li.stream-item")]
        IList<IWebElement> tweets { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.stream-item a.twitter-timeline-link")]
        IList<IWebElement> tweetsLinks { get; set; }

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
            Base.MongoDb.UpdateSteps($"Search Tweet: {title}.");
            return _browserHelper
                    .ExecutUntillTrue(() => tweets.ToList().Where(t => t.FindElement(By.XPath(".//p")).Text == title).FirstOrDefault());
        }

        public bool ValidateTweetByTitle(string title)
        {
            return SearchTweet(title).Displayed;
        }

        public PostPage ClickOnTweetLink(string title)
        {
            var index = tweets.ToList().LastIndexOf(SearchTweet(title));
            var link = _browserHelper.ExecutUntillTrue(() => tweetsLinks.ToList().Where((t,i) => i == index).FirstOrDefault());
            _browserHelper.Click(link, "Twiter Link");

            return new PostPage(_browser); 
        }
    }
}
