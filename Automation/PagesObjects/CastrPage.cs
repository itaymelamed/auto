using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Automation.PagesObjects
{
    public class CastrPage
    {
        [FindsBy(How = How.CssSelector, Using = "[name='language']")]
        IWebElement languageDd { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".fetching")]
        IWebElement fetching { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> results { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".urls__input")]
        IWebElement postUrl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[value='article']")]
        IWebElement articleCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='types_all']")]
        IWebElement allCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".types-list input")]
        IList<IWebElement> types { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-type span")]
        IList<IWebElement> typesIcons { get; set; }

        enum Languages
        {
            en,
            es,
            de,
            fr,
            it,
            id,
            ptbr,
            th,
            vn,
            tr
        }

        public enum Types
        {
            all,
            article,
            slideShow,
            topX,
            lineUp,
            pundit,
            tv,
            timeout,
        }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public CastrPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidateCasterPage()
        {
            Base.MongoDb.UpdateSteps("Validate user is on Castr page.");
            return _browserHelper.WaitForUrlToChange($"{Base._config.Url}/castr");
        }

        public void FilterByLanguage(string language)
        {
            Base.MongoDb.UpdateSteps($"Filter posts by language: {language}");
            _browserHelper.WaitForElement(languageDd, nameof(languageDd));
            SelectElement select = new SelectElement(languageDd);
            select.SelectByValue(language);
        }

        public string ValidateFilterByLanguageEn()
        {
            Base.MongoDb.UpdateSteps($"Validate filter posts by en");
            _browserHelper.WaitForElementDiss(fetching);
            var errors = string.Empty;
            results.Where((x,i) => i < 5).ToList().ForEach(r =>
            {
                _browserHelper.Click(r, r.Text);
                _browserHelper.ExecutUntillTrue(() => postUrl.GetAttribute("value") != "");
                var parsedUrl = postUrl.GetAttribute("value").Replace("http://", "").Split('/')[1];
                errors += parsedUrl == "posts" ? "" : $"Post {r.Text} is not in english {Environment.NewLine}";
            });

            return errors;
        }

        public void DeselectAllCheckBoxes()
        {
            SelectAllCheckBoxes();
            _browserHelper.WaitForElementDiss(fetching);
            Base.MongoDb.UpdateSteps($"Deselect all checkboxes.");
            SelectAllCheckBoxes();
        }

        public void SelectAllCheckBoxes()
        {
            Base.MongoDb.UpdateSteps($"Select all checkboxes.");
            _browserHelper.WaitForElement(allCbx, nameof(allCbx));
            _browserHelper.Click(allCbx, nameof(allCbx));
        }

        public void SelectType(Types type)
        {
            _browserHelper.WaitForElementDiss(fetching);
            Base.MongoDb.UpdateSteps($"Select {type}.");
            var typesCount = types.ToList().Count();
            _browserHelper.ExecutUntillTrue(() => typesCount == 10);
            _browserHelper.Click(types.ToList().Where(t => t.GetAttribute("value") == type.ToString()).FirstOrDefault(), type.ToString());
        }

        public bool ValidateFilterByType(Types type)
        {
            Base.MongoDb.UpdateSteps($"Validate {type} icon is next to each post.");
            _browserHelper.WaitForElementDiss(fetching);
            return typesIcons.ToList().All(t => t.GetAttribute("class") == type.ToString());
        }
    }
}