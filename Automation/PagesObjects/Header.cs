﻿using Automation.BrowserFolder;
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
        
        [FindsBy(How = How.CssSelector, Using = ".edition-component.has-dropdown")]
        IWebElement dropdownCurLangauge { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".dropdown-comp__item")]
        IList<IWebElement> dropdownLangauges { get; set; }

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
    }
}