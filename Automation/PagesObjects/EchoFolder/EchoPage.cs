﻿using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.EchoFolder
{
    public class EchoPage : NewsRoomPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@role='listbox'][1]")]
        IWebElement langnugeDropDown { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@role='listbox'][2]")]
        IWebElement statusDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".text.medium.regular")]
        IList <IWebElement> postsTitles { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".tableBody [style='flex: 0 0 13%;']")]
        IList <IWebElement> authorNames { get; set; }   

        [FindsBy(How = How.CssSelector, Using = ".tableBody [style='flex: 1 0 97px;']")]
        IList <IWebElement> domains { get; set; }   

        [FindsBy(How = How.CssSelector, Using = ".oval")]
        IList<IWebElement> statuses { get; set; }

        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public EchoPage(Browser browser)
            :base(browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidatePostCreation(string title)
        {
            Base.MongoDb.UpdateSteps($"Search for post title: {title}.");
            return _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Any(t => t.Text == title)); 
        }

        public bool ValidateAuthor(string author, string title)
        {
            Base.MongoDb.UpdateSteps($"Validate author name: {author}.");
            _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Count() >= 2);
            int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
            string authorIndex = authorNames.ToList()[i].Text;
            return author == authorIndex;
        }

        public bool ValidateDomain(string domain, string title)
        {
            Base.MongoDb.UpdateSteps($"Validate the domain of the site: {domain}.");
            _browserHelper.WaitUntillTrue(() => domains.ToList().Count() >= 2);
            int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
            string domainIndex = domains.ToList()[i].Text;
            return domain.ToLower() == domainIndex;
        }

        public bool ValidateSatatus(string status, string title)
        {
            Base.MongoDb.UpdateSteps($"Validate the status of the post: {status}.");

            return _browserHelper.RefreshUntill(() => 
            {
                _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Any(t => t.Text == title));
                int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
                string statusIndex = statuses.ToList()[i].Text;
                return statusIndex == status;
            });
        }

        public DistributionPage SelectPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Select post {title} from the list.");
            _browserHelper.ExecuteUntill( () => postsTitles.Where(t => t.Text == title).FirstOrDefault().Click());

            return new DistributionPage(_browser);
        }
    }
}