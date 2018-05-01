using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class Cookies : BaseObject
    {   
        IWebElement cookies => FindElement(".cookie-disclaimer__message label");

        IWebElement xBtn=> FindElement(".cookie-disclaimer__message__close-button");

        public Cookies(Browser browser)
            : base(browser)
        {
        }

        public bool ValidateCookiesDisclaimerAppears(int timeOut)
        {
            UpdateStep($"Validating cookies disclaimer appears.");
            return _browserHelper.WaitUntillTrue(() => cookies.Text.ToLower() != "cookies", "Cookie dissclaimer", timeOut);
        }

        public bool ValidateCookiesDisclaimerDisappears(int timeOut)
        {
            UpdateStep($"Validating cookies disclaimer disappears.");
            return _browserHelper.WaitForElementDiss(() => cookies, timeOut);
        }

        public void ClickOnX()
        {
            UpdateStep($"Clicking on X button.");
            _browserHelper.WaitForElement(() => xBtn);
            _browserHelper.Click(xBtn, "X button");
        }
    }
}