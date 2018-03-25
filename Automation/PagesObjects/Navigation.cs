using System;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

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
            var url = icon["url"].ToString().Split('?').First();
                
            languages.ForEach(l =>
            {
                _browser.Navigate($"{Base._config.Url}/{l}");
                Base.MongoDb.UpdateSteps($"Finding element by selctor {selector}");
                IWebElement el = _browserHelper.FindElement(By.CssSelector(selector), "Icon");
                Base.MongoDb.UpdateSteps("Clicking on Icon.");
                el.Click();
                var acUrl = _browser.GetUrl().ToLower().Split('?').First();
                var exUrl = GetExUrl(l, selector, url ,selector.Contains("video")).Split('?').First();

                Base.MongoDb.UpdateSteps("Validating url.");
                errors += acUrl.ToLower() == exUrl.ToLower() ? "" : $"Expected url: {exUrl}. Actual: {acUrl}";
            });

            return errors;
        }

        string GetExUrl(string language, string selector, string url ,bool video = false)
        {
            var testId = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString();

            if (video) 
                return $"http://videos.{Base._config.SiteName.ToLower()}.com{url}";

            if (testId == "112" && language == "es")
                return $"{Base._config.Url.ToLower()}/{language}/categories/copa-libertadores";

            if (testId == "112" && language == "pt-BR")
                return $"{Base._config.Url.ToLower()}/{language}/leagues/copa-libertadores";

            if (testId == "111" && language == "es" && Base._config.SiteName == "90Min")
                return $"{Base._config.Url.ToLower()}/{language}/Categories/viral";

            if (language == "en")
                return $"{Base._config.Url.ToLower()}{url}";
            else
                return $"{Base._config.Url.ToLower()}/{language}{url}";
        }
    }
}
