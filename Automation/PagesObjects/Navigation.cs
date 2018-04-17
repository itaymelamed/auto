using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class Navigation : BaseObject
    {
        public Navigation(Browser browser)
            : base(browser)
        {
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
                UpdateStep($"Finding element by selctor {selector}");
                IWebElement el = FindElement(selector);
                UpdateStep("Clicking on Icon.");
                _browserHelper.Click(el, l);

                var acUrl = _browser.GetUrl().ToLower().Split('?').First();
                var exUrl = GetExUrl(l, url ,selector.Contains("video")).Split('?').First();

                UpdateStep("Validating url.");
                errors += acUrl.ToLower() == exUrl.ToLower() ? "" : $"Expected url: {exUrl}. Actual: {acUrl}";
            });

            return errors;
        }

        string GetExUrl(string language, string url ,bool video = false)
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
  
            return $"{Base._config.Url.ToLower()}/{language}{url}";
        }
    }
}