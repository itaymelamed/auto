using MongoDB.Bson;
using System.Linq;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System;
using Automation.TestsFolder;

namespace Automation.PagesObjects
{
    public class EditorPage
    {
        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-article']")]
        IWebElement article { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-top_x']")]
        IWebElement list { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-lineup']")]
        IWebElement lineup { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-slideshow']")]
        IWebElement slideShow { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-timeout']")]
        IWebElement timeOut { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".templates li[class='template-tv']")]
        IWebElement tv { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public EditorPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public ArticleBase ClickOnArticle()
        {
            Base.MongoDb.UpdateSteps($"Click on Article template.");
            _browserHelper.WaitForElement(article, nameof(article));
            _browserHelper.Click(article, nameof(article));

            return new ArticleBase(_browser);
        }

        public string Validatetemplates(BsonArray templateNames)
        {
            Base.MongoDb.UpdateSteps($"Validate templates.");
            List<string> templatesList = null;

            List<string> templatesNamesList = templateNames.ToList().Select(x => x.ToString()).ToList();
            _browserHelper.ExecutUntillTrue(() =>
            {
                templatesList = _driver.FindElements(By.CssSelector(".templates li[class*='template-']")).Select(e => e.GetAttribute("class")).ToList();
                return templatesList.Count() == templateNames.Count();
            }, $"Expected {templateNames.Count()} templates but actul {templatesList} templates.");

            var errors = string.Empty;

            templatesNamesList.ForEach(n => {
                errors += templatesList.Contains(n) ? "" : $"Template {n} hasn't shown {Environment.NewLine}";
            });

            return errors;
        }
    }
}
