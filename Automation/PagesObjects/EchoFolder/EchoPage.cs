using System;
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

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

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
            Base.MongoDb.UpdateSteps($"Search for post title: {title}");
            return _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Any(t => t.Text == title)); 
        }
    }
}
