using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Automation.PagesObjects
{
    public class Header
    {
        
        [FindsBy(How = How.CssSelector, Using = ".edition-component__current")]
        IWebElement dropdownCurLangauge { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".dropdown-comp__item")]
        IList<IWebElement> dropdownLangauges { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".logo")]
        IWebElement logo { get; set; }


        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public Header(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void HoverLanguage()
        {
            _browserHelper.Hover(dropdownCurLangauge);
        }

        public bool ValidateCurrentLangaugeDropDown(string exCurLanguage)
        {
            Base.MongoDb.UpdateSteps("");
            var acCurLanguage = dropdownCurLangauge.Text;
            return acCurLanguage == exCurLanguage;

        }
        public bool ValidateLanguageDropDownLangauge(BsonArray exCurDropDown)
        {
            Base.MongoDb.UpdateSteps("");
            bool sum = false;
            _browserHelper.WaitForElement(dropdownCurLangauge, nameof(dropdownLangauges));
            var actualCurrentlanguage = dropdownCurLangauge.Text;
            var exCurDropDownList = exCurDropDown.Select(x => x.ToString()).ToList();
            var acDropDown = dropdownLangauges.ToList().Select(e => e.GetAttribute("innerHTML").ToUpper()).ToList();

            HoverLanguage();
            Thread.Sleep(2000);
            for (int i = 0; i <= exCurDropDownList.Count - 1; i++)
            {
                if (actualCurrentlanguage == exCurDropDownList[i])
                {
                    exCurDropDownList.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i <= exCurDropDownList.Count - 1; i++)
            {
                if ((acDropDown.Contains(exCurDropDownList[i])))
                {
                    sum = true;
                }
                else
                {
                    sum = false;
                    break;
                }
            }
            return sum;
        }

        public bool ValidateLangagueDropDownDoesntAppear()
        {
            bool sum = false;
            Base.MongoDb.UpdateSteps("Check if langague dropdown does not appear");
            sum = _browserHelper.WaitForElement(dropdownCurLangauge, nameof(dropdownCurLangauge), 0 ,false);
            return sum;
        }

        public string SelectAndValidateCurLanguageDropDown(BsonArray languages, BsonArray urls)
        {
            string errors = string.Empty;
            List<string> urlsList = urls.Select(u => u.ToString()).ToList();
            List<string> languagesList = languages.Select(l => l.ToString()).ToList();
           // List<string> dropDownLangague = dropdownLangauges.Select(s => s.GetAttribute("innerHTML").ToString()).ToList();
           

            languagesList.ForEach(l => 
            {
                HoverLanguage();
                Thread.Sleep(2000);
                var xxx = dropdownLangauges.ToList();
                dropdownLangauges.ToList().Where(ld => ld.GetAttribute("innerHTML") == l).First().Click();
                PageFactory.InitElements(_driver, this);
                var url = _browser.GetUrl();
                int index = languagesList.FindIndex(i => i == l);
                var exUrl = urlsList[index];

                errors += url == $"{Base._config.Url}/{exUrl}";

            });
            return errors;
        }

        public bool SelectAndValidateLogo(BsonArray href, BsonArray urls)
        {
            logo.Click();
            PageFactory.InitElements(_driver, this);
            List<string> urlsList = urls.Select(u => u.ToString()).ToList();
            List<string> hrefList = href.Select(u => u.ToString()).ToList();
            var curHref = logo.GetAttribute("href");
            var sum =false;
            for (int i = 0; i < urlsList.Count - 1; i++)
            {
                if (curHref == $"{Base._config.Url}/{urlsList[i]}")
                {
                    sum = true;
                }
            }
            return sum;


        }
    }
}