using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class TwitterAppPage : BaseObject
    {
        List<IWebElement> tweets => FindElements(".content");

        public TwitterAppPage(Browser browser)
            :base(browser)
        {
        }

        IWebElement SearchTweet(string title)
        {
            UpdateStep($"Search Tweet: {title}.");
            return _browserHelper
                .ExecutUntillTrue(() => tweets.ToList().Where(t => t.FindElement(By.XPath(".//p")).Text.Contains(title)).FirstOrDefault());
        }

        public bool ValidateTweetByTitle(string title)
        {
            return SearchTweet(title).Displayed;
        }

        public PostPage ClickOnTweetLink(string title)
        {
            var link = _browserHelper.ExecutUntillTrue(() => SearchTweet(title).FindElement(By.XPath($".//a[title*='{Base._config.Env.ToString().ToLower()}']")));
            _browserHelper.Click(link, "Twiter Link");

            return new PostPage(_browser); 
        }
    }
}
