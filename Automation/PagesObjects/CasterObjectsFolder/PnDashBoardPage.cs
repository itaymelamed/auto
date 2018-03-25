using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class PnDashBoardPage
    {
        [FindsBy(How = How.TagName, Using = "tbody")]
        IList<IWebElement> leagues { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public PnDashBoardPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidatePost(int league, string title)
        {
            return _browserHelper.RefreshUntill(() =>
            {
                _browserHelper.WaitUntillTrue(() => leagues.ToList().Count > 1);
                return leagues.ToList()[league].FindElements(By.XPath(".//td"))
                       .Any(t => Regex.Replace(t.Text.ToLower().Replace('-', ' '), "", string.Empty) == title);
            }, $"Post {title} was not found on PN Dasboard.", 60);
        }
    }
}