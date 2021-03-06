﻿using System.Threading;
using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class WritersPage : BaseObject
    {
        IWebElement WriteAnArticleBtn => FindElement(".h2 a");

        public WritersPage(Browser browser)
            :base(browser)
        {
            
        }

        public EditorPage ClickOnWriteAnArticleBtn()
        {
            _browser.SwitchToFirstTab();
            Thread.Sleep(1000);
            UpdateStep($"Clicking on 'Write an article' button.");
            if(_browserHelper.WaitForElement(() => WriteAnArticleBtn, nameof(WriteAnArticleBtn), 10, false))
                _browserHelper.Click(WriteAnArticleBtn, "Write new article button.", 0, false);

            return new EditorPage(_browser);
        }
    }
}
