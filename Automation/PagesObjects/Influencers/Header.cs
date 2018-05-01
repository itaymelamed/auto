using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.Influencers
{
    public class Header : BaseObject
    {
        IWebElement _signOutBtn => FindElement(".sign-out");

        IWebElement _pendingAffiliateLink => FindElement("[href='/pending_affiliates']");

        IWebElement _browseAffiliateLink => FindElement("[href='/browse_affiliates']");

        IWebElement _paymentsLink => FindElement("[href='/payments']");

        IWebElement _userMangementLink => FindElement("[href='/user_managment']");

        public Header(Browser browsr)
            :base(browsr)
        {
        }

        public LoginPage LogOut()
        {
            UpdateStep("Clicking on 'Sign Out' button.");
            _browserHelper.WaitForElement(() => _signOutBtn);
            _browserHelper.Click(_signOutBtn);

            return new LoginPage(_browser);
        }
    }
}