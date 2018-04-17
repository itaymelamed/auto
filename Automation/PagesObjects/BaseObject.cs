using System.Collections.Generic;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class BaseObject
    {
        protected Browser _browser { get; }
        protected IWebDriver _driver => _browser.Driver;
        protected BrowserHelper _browserHelper => _browser.BrowserHelper;

        public BaseObject(Browser browser) => _browser = browser;

        protected IWebElement FindElement(string selector)
        {
            return _browserHelper.FindElement(selector);
        }

        protected List<IWebElement> FindElements(string selector)
        {
            return _browserHelper.FindElements(selector);
        }

        protected void UpdateStep(string step)
        {
            Base.MongoDb.UpdateSteps(step);
        }
    }
}