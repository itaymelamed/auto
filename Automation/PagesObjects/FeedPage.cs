using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class FeedPage
    {
        [FindsBy(How = How.CssSelector, Using = ".feedpage-article__title")]
        IList<IWebElement> articles { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public FeedPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public IWebElement SearchArticle(string title)
        {
            _browserHelper.WaitUntillTrue(() => articles.ToList().Count() == 15);
            return _browserHelper.ExecutUntillTrue(() => articles.ToList().Where(a => Regex.Replace(a.Text.Replace('-', ' ').ToLower(), @"[\d-]", string.Empty) == title).FirstOrDefault());
        }

        public bool ValidateArticleByTitle(string title)
        {
            return _browserHelper.RefreshUntillQuery(() => articles.Any(a => Regex.Replace(a.Text.Replace('-', ' ').ToLower(), @"[\d-]", string.Empty) == title), $"Post {title} was not found on feed." ,60);
        }

        public bool ValidatePostTitleInFeedPage(string title)
        {
            Base.MongoDb.UpdateSteps("Validating the more news title text.");
            bool result = false;
            _browserHelper.WaitUntillTrue(() => articles.ToList().Count() >= 2);
            _browserHelper.ExecuteUntill(() => result = articles.ToList().Any(t => t.Text == title));

            return result;
        }
    }
}