using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class Cookies : BaseObject
    {   IWebElement cookies => FindElement(".cookie-disclaimer__message");
        IWebElement xBtn=> FindElement(".cookie-disclaimer__message__close-button");
        public Cookies(Browser browser)
            : base(browser)
        {
          
        }

        public bool ValidateCookiesDisclaimerAppears(int timeOut)
        {
            UpdateStep("balbla");
            return _browserHelper.WaitForElement(() => cookies, "Cookie dissclaimer", timeOut);
        }

        public bool ValidateCookiesDisclaimerDisappears(int timeOut)
        {
            return _browserHelper.WaitForElementDiss(() => cookies, timeOut);
        }



        public void ClickOnX()
        {
            _browserHelper.WaitForElement(() => xBtn);
            _browserHelper.Click(xBtn, "X button");
            
        }






    }
}
