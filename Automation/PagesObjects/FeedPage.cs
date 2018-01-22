using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

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
            return _browserHelper.RefreshUntill(() => articles.Any(a => Regex.Replace(a.Text.Replace('-', ' ').ToLower(), @"[\d-]", string.Empty) == title), 60);
        }
    }
}
