using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class FeedPage : BaseObject
    {
        List<IWebElement> articles => FindElements(".feedpage-article__title");

        public FeedPage(Browser browser)
            : base(browser)
        {
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