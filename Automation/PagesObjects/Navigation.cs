using System;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class Navigation
    {
        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public Navigation(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public string ValidateIcon(BsonValue parameters)
        {
            var errors = string.Empty;

            var icon = parameters["Icon"];
            var languages = parameters["Languages"].AsBsonArray.Select(l => l.ToString()).ToList();
            var selector = icon["Selector"].ToString();
            var url = icon["url"].ToString();

            languages.ForEach(l =>
            {
                _browser.Navigate($"{Base._config.Url}/{l}");
                IWebElement el = _browserHelper.FindElement(By.CssSelector(selector), "Icon");
                el.Click();
                var acUrl = _browser.GetUrl().ToLower();
                var exUrl = GetExUrl(l, selector, url ,selector.Contains("video"));

                errors += acUrl.ToLower() == exUrl.ToLower() ? "" : $"Expected url: {exUrl}. Actual: {acUrl}";
            });

            return errors;
        }

        string GetExUrl(string language, string selector, string url ,bool video = false)
        {
            if (video)
                return $"http://videos.{Base._config.SiteName.ToLower()}{url}";

            if (language == "en")
                return $"{Base._config.Url.ToLower()}{url}";
            else
                return $"{Base._config.Url.ToLower()}/{language}{url}";
        }
    }
}
