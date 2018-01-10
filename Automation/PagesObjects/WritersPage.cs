﻿using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class WritersPage
    {
        [FindsBy(How = How.CssSelector, Using = "h2 a")]
        IWebElement WriteAnArticleBtn { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public WritersPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public EditorPage ClickonWriteAnArticleBtn()
        {
            _browser.SwitchToFirstTab();
            Thread.Sleep(1000);
            Base.MongoDb.UpdateSteps($"Click on Write an article Button.");
            if(_browserHelper.WaitForElement(WriteAnArticleBtn, nameof(WriteAnArticleBtn), 10, false))
                _browserHelper.Click(WriteAnArticleBtn, "Write new article button.", 0, false);

            return new EditorPage(_browser);
        }
    }
}
