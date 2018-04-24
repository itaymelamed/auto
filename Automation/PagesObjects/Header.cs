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
    public class Header : BaseObject
    {

        IWebElement dropdownCurLangauge => _browserHelper.FindElement(".edition-component__current");

        IList<IWebElement> dropdownLangauges => _browserHelper.FindElements(".dropdown-comp__item");

        IWebElement logo => _browserHelper.FindElement(".logo");

        public Header(Browser browser)
            : base(browser)
        {
        }

        public void HoverLanguage()
        {
            _browserHelper.Hover(dropdownCurLangauge);
        }

        public bool ValidateCurrentLangaugeDropDown(string exCurLanguage)
        {
            UpdateStep("");
            var acCurLanguage = dropdownCurLangauge.Text;
            return acCurLanguage == exCurLanguage;

        }
        public bool ValidateLanguageDropDownLangauge(BsonArray exCurDropDown)
        {
            UpdateStep("Validate language dropdown");
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
            UpdateStep("Check if langague dropdown does not appear");
            sum = _browserHelper.WaitForElement(dropdownCurLangauge, nameof(dropdownCurLangauge), 0 ,false);
            return sum;
        }

        public bool SelectAndValidateLogo(BsonArray href, BsonArray urls)
        {
            List<string> urlsList = urls.Select(u => u.ToString()).ToList();
            List<string> hrefList = href.Select(u => u.ToString()).ToList();
            var sum = true;

            urlsList.ForEach(l =>
            {
                _browser.Navigate($"{Base._config.Url}/{l}");
                var curHref = logo.GetAttribute("href");
                if(curHref != $"{Base._config.Url}/{l}")
                {
                    sum = false;
                }

            });
            return sum;
        }
    }
}