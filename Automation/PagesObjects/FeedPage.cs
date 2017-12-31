using System.Collections.Generic;
using System.Linq;
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
            return _browserHelper.ExecutUntillTrue(() => articles.ToList().Where(a => a.Text == title).FirstOrDefault());
        }

        public bool ValidateArticleByTitle(string title)
        {
            return articles.Any(a => a.Text == title);
        }
    }
}
