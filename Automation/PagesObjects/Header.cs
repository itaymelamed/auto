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
    public class Header : HomePage
    {
        
        [FindsBy(How = How.CssSelector, Using = ".edition-component.has-dropdown")]
        IWebElement dropdownCurLangauge { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".dropdown-comp__item")]
        IList<IWebElement> dropdownLangauges { get; set; }


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
            var acCurLanguage = dropdownCurLangauge.Text;
            return acCurLanguage == exCurLanguage;

        }
        public bool ValidateLanguageDropDownLangauge(BsonArray exCurDropDown)
        {
            bool sum = false;
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
    }
}