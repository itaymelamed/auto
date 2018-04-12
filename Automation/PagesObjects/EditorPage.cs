using MongoDB.Bson;
using System.Linq;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
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

        [FindsBy(How = How.CssSelector, Using = ".templates li a")]
        IList<IWebElement> templates { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-template='Write an Article']")]
        IWebElement editorTitle { get; set; }

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
            Base.MongoDb.UpdateSteps($"Clicking on Article template.");
            _browserHelper.WaitForElement(article, nameof(article));
            _browserHelper.Click(article, nameof(article));

            return new ArticleBase(_browser);
        }

        public ListsTemplate ClickOnList()
        {
            Base.MongoDb.UpdateSteps($"Clicking on list template.");
            _browserHelper.WaitForElement(list, nameof(list));
            _browserHelper.Click(list, nameof(list));

            return new ListsTemplate(_browser);
        }

        public string Validatetemplates(BsonArray templateNames)
        {
            Base.MongoDb.UpdateSteps($"Validating templates.");
            List<string> templatesList = null;
            List<string> templatesNamesList = null;

            _browserHelper.WaitUntillTrue(() =>
            {
                templatesNamesList = templateNames.ToList().Select(x => x.ToString()).ToList();
                templatesList = _driver.FindElements(By.CssSelector(".templates li[class*='template-']")).Select(e => e.GetAttribute("class")).ToList();
                return templatesList.Count() == templateNames.Count();
            }, $"Expected {templateNames.Count()} templates but actul templates: {templatesList.Count}.");

            var errors = string.Empty;

            templatesNamesList.ForEach(n => {
                errors += templatesList.Contains(n) ? "" : $"Template {n} hasn't shown {Environment.NewLine}";
            });

            return errors;
        }

        public ArticleBase ClickOnTemplate(int i)
        {
            Base.MongoDb.UpdateSteps($"Clicking on template number {i}.");
            _browserHelper.WaitUntillTrue(() => templates.ToList().Count() > 2);
            IWebElement temp = templates.Where((t, j) => j == i).FirstOrDefault();
            _browserHelper.Click(temp, $"template {i}");

            return new ArticleBase(_browser);
        }

        public TVPage ClickOnTVTemplate()
        {
            Base.MongoDb.UpdateSteps($"Clicking on tv template.");
            _browserHelper.WaitForElement(tv, nameof(tv));
            _browserHelper.Click(tv, nameof(tv));

            return new TVPage(_browser);
        }

        public SlideShowPage ClickOnSlideShow()
        {
            Base.MongoDb.UpdateSteps($"Clicking on slideShow template.");
            _browserHelper.WaitForElement(slideShow,nameof(slideShow));
            _browserHelper.Click(slideShow,nameof(slideShow));

            return new SlideShowPage(_browser); 
        }

        public bool ValidateEditorTitle()
        {
            Base.MongoDb.UpdateSteps($"Validatting editor title.");
            return _browserHelper.WaitForElement(editorTitle,nameof(editorTitle));
        }
    }
}