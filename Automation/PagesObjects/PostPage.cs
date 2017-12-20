using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Automation.PagesObjects
{
    public class PostPage
    {
        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public PostPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public string ValidateComponents(BsonArray components)
        {
            var errors = string.Empty;
            List<string> componentsstrings = components.ToList().Select(x => x.ToString()).ToList();
            componentsstrings.ForEach(c =>
            {
                IWebElement el = null;
                Base.MongoDb.UpdateSteps($"Validate Component {c}.");
                if (!_browserHelper.ExecutUntillTrue(() => el = _driver.FindElement(By.CssSelector(c)), "", 0, false))
                    errors += $"Component {c} does not exsist {Environment.NewLine}";
            });

            return errors;
        }

        public string ValidateTagsOnSourcePage(BsonArray tags)
        {
            Base.MongoDb.UpdateSteps("Validate Tags On Source Page.");
            string errors = string.Empty;
            List<string> tagsList = tags.AsBsonArray.ToList().Select(t => t.ToString()).ToList();
            _browser.Navigate(_browser.GetUrl()+"?test=test");
            tagsList.ForEach(t => {
                errors += !_browser.GetSource().Contains(t) ? $"Tag {t} does not exsist on page source. {Environment.NewLine}" : "";
            });

            return errors;
        }

        public bool ValidatePostCreated(string postTitle)
        {
            _browserHelper.ExecutUntillTrue(() => _browser.GetUrl().Contains("posts"), "Post title is not shown on url.");
            _browserHelper.ExecutUntillTrue(() => _browser.GetUrl().Replace("-", " ").Contains(postTitle.ToLower().Replace(":", " ")), "User has not redirected to posts page.");
            return true;
        }
    }
}
