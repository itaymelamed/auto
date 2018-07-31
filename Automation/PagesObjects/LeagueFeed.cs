using System;
using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class LeagueFeed : BaseObject
    {
        IWebElement groups => FindElement(".user-menu__link img");

        public LeagueFeed(Browser browser)
            : base(browser)
        {
        }
    }
}