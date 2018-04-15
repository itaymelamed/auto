using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class PartnersPage : BaseObject
    {
        IWebElement cover => FindElement(".partners-topic__excerpt h3");

        List<IWebElement> latestStories => FindElements(".grid-container h3");

        List<IWebElement> moreStories => FindElements(".feedpage-article__title");

        IWebElement bottomReadMore => FindElement(".button-extra-margin-black__link");

        public PartnersPage(Browser browser)
            : base(browser)
        {
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