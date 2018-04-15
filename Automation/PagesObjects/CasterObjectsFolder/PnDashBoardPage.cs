using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class PnDashBoardPage :BaseObject
    {
        List<IWebElement> leagues => _browserHelper.FindElements("tbody");

        public PnDashBoardPage(Browser browser)
            :base(browser)
        {
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