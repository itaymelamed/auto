using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class PartnersPage
    {
        [FindsBy(How = How.CssSelector, Using = ".partners-topic__excerpt h3")]
        IWebElement cover { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".grid-container h3")]
        IList<IWebElement> latestStories { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".feedpage-article__title")]
        IList<IWebElement> moreStories { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".button-extra-margin-black__link")]
        IWebElement bottomReadMore { get; set; }

        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public PartnersPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public string GetCoverTitle()
        {   Base.MongoDb.UpdateSteps("Getting the title of the cover post.");
            return Regex.Replace(cover.Text.Replace("-"," "), @"[\d-]", string.Empty).Trim().ToLower();
        }

        public List<string> GetTitles()
        {   
            Base.MongoDb.UpdateSteps("Getting the titles of all posts in partners page.");
            List<string> titles = new List<string>();
            titles.Add(GetCoverTitle());
            titles.AddRange(latestStories.Select(t => Regex.Replace(t.Text.Replace("-"," ").ToLower(), @"[\d-]", string.Empty).Trim()).ToList());
            titles.AddRange(moreStories.Select(t => Regex.Replace(t.Text.Replace("-"," ").ToLower(), @"[\d-]", string.Empty).Trim()).ToList());

            return titles;
        }
    }
}