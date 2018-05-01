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
            [Property("TestCaseId", "1")]
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


        public class ValidateAppearsInPost : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
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

        public class ValidateAppearsInFeed : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateApearsInFeed()
            {


                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string feedUrl = homePage.GetFeedUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url + feedUrl);
                Cookies cookies = new Cookies(_browser);
                Assert.True(cookies.ValidateCookiesDisclaimerAppears(30), "Cookies disclaimer wasn't found on post page");


            }
        }

        public class ValidateDisappears : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisapears()
            {
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(10), "Cookies disclaimer wasn't found");
            }
        }

        public class ValidateDisappearsFromPost : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisapearsFromPost()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                string postURL = homePage.GetCoverStoryUrl();
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(postURL);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(10), "Cookies disclaimer wasn't found");
            }
        }

        public class ValidateDisappearsFromFeed : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateDisapearsFromPost()
            {
                _browser.Navigate(_config.Url);
            }
        }


        public class ValidateXbtn : BaseUi
        {

            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateXbtn()
            {
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer wasn't found");
            }
        }
        public class ValidateXbtnPost : BaseUi
        {

            [Test]
            [Property("TestCaseId", "1")]
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
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer wasn't found");
            }
        }

        public class ValidateXbtnFeed : BaseUi
        {

            [Test]
            [Property("TestCaseId", "1")]
            [Category("CookieDissclaimer")]
            public void CookiesDisclaimer_ValidateXbtnFeed()
            {
                _browser.Quit();
                _browser = new Browser(false, false);
                _browser.Navigate(_config.Url);
                Cookies cookies = new Cookies(_browser);
                cookies.ValidateCookiesDisclaimerAppears(30);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer wasn't found");
            }
        }
    }  
}