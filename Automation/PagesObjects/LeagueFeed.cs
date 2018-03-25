using System;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class LeagueFeed
    {
        [FindsBy(How = How.CssSelector, Using = ".user-menu__link img")]
        IWebElement groups { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public LeagueFeed(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }
    }
}
