﻿using Automation.BrowserFolder;
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

        public bool SelectAndValidateLogo(BsonValue headerData)
        {
            var url = headerData["url"].ToString();
            var exDropDown = headerData["curDropDown"].ToString();
            var acDropDown = dropdownCurLangauge.GetAttribute("innerHTML");
            var sum = true;
            var exHref = $"{Base._config.Url}/{url}".ToLower();
<<<<<<< HEAD
=======
                _browser.Navigate($"{Base._config.Url}/{url}");
>>>>>>> ce132eb328e91be0aad057875e270370879d67d1
            var curHref = logo.GetAttribute("href").ToLower();
            if(curHref != exHref || exDropDown != acDropDown)
                {
                    sum = false;
                }
            return sum;
        }
    }
}