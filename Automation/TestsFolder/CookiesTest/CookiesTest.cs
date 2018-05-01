using Automation.BrowserFolder;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.CookiesTest
{
    public class CookiesTest
    {
        [TestFixture]
        [Parallelizable]
        public class ValidateAppears : BaseUi
        {
            [Test]
            [Property("TestCaseId", "175")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateApears()
            {
                _browser.Quit();
                _browser = new Browser(false, false); 
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                Assert.True(cookies.ValidateCookiesDisclaimerAppears(30), "Cookies disclaimer wasn't found");
            }
        }

        [TestFixture]
        public class ValidateAppearsInPost : BaseUi
        {
            [Test]
            [Property("TestCaseId", "176")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateApearsInPost()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string postURL = homePage.GetCoverStoryUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(postURL);
                Cookies cookies = new Cookies(_browser);
                Assert.True(cookies.ValidateCookiesDisclaimerAppears(30), "Cookies disclaimer wasn't found on post page");
            }
        }

        [TestFixture]
        public class ValidateAppearsInFeed : BaseUi
        {
            [Test]
            [Property("TestCaseId", "177")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateApearsInFeed()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string feedUrl = homePage.GetFeedUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(feedUrl);
                Cookies cookies = new Cookies(_browser);
                Assert.True(cookies.ValidateCookiesDisclaimerAppears(30), "Cookies disclaimer wasn't found on feed page");
            }
        }

        [TestFixture]
        public class ValidateDisappears : BaseUi
        {
            [Test]
            [Property("TestCaseId", "178")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisappears()
            {
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(10), "Cookies disclaimer didn't disappear");
            }
        }

        [TestFixture]
        public class ValidateDisappearsFromPost : BaseUi
        {
            [Test]
            [Property("TestCaseId", "179")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisappearsFromPost()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string postURL = homePage.GetCoverStoryUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(postURL);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(10), "Cookies disclaimer didn't disappear from post page");
            }
        }

        [TestFixture]
        public class ValidateDisappearsFromFeed : BaseUi
        {
            [Test]
            [Property("TestCaseId", "180")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisappearsFromFeed()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string feedUrl = homePage.GetFeedUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(feedUrl);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(10), "Cookies disclaimer didn't disappear from feed page");
            }
        }

        [TestFixture]
        public class ValidateXbtn : BaseUi
        {
            [Test]
            [Property("TestCaseId", "181")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateXbtn()
            {
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer didn't disappear");
            }
        }

        [TestFixture]
        public class ValidateXbtnPost : BaseUi
        {
            [Test]
            [Property("TestCaseId", "182")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateXbtnPost()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string postURL = homePage.GetCoverStoryUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(postURL);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer didn't disappear from post page");
            }
        }

        [TestFixture]
        public class ValidateXbtnFeed : BaseUi
        {
            [Test]
            [Property("TestCaseId", "183")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateXbtnFeed()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string feedUrl = homePage.GetFeedUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(feedUrl);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer didn't disappear from feed page");
            }
        }
    }  
}