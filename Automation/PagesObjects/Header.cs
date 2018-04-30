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

        public bool LogoAndDropDown(BsonValue headerData)
        {
            UpdateStep("Validate logo and current drop down with the right local");
            var url = headerData["url"].ToString();
            _browser.Navigate($"{Base._config.Url}/{url}");
            var exDropDown = headerData["curDropDown"].ToString().ToLower();
            var acDropDown = dropdownCurLangauge.GetAttribute("innerHTML").ToLower();
            var sum = true;
            var exHref = $"{Base._config.Url}/{url}".ToLower();
            var curHref = logo.GetAttribute("href").ToLower();
            if (acDropDown == "es-latam")
            {
                curHref = $"{Base._config.Url}/{url}".ToLower();
            }

            if(curHref != exHref || exDropDown != acDropDown)
                {
                    sum = false;
                }
            return sum;
        }
    }
}