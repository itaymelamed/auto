using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
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

        public bool ValidateLangagueDropDownAppears()
        {
            UpdateStep("Validate language dropdown appear");
            bool sum = false;
            sum = _browserHelper.WaitForElement(() => dropdownCurLangauge, nameof(dropdownCurLangauge), 0, true);
            return sum;

        }

        public bool ValidateLangagueDropDownDoesntAppear()
        {
            UpdateStep("Validate language dropdown doesnt appear");
            bool sum = false;
            sum = _browserHelper.WaitForElement(() => dropdownCurLangauge, nameof(dropdownCurLangauge), 0 ,false);
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