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
                //HomePage homePage = new HomePage(_browser);
                //PostPage postpage = homePage.ClickOnCoverStory();
            }
        }

        public class ValidateDisappears : BaseUi
        {
            [Test]
            public void CookiesDisclaimer_ValidateDisapears()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                PostPage postpage = homePage.ClickOnCoverStory();
                Cookies cookies = new Cookies(_browser);
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(5), "Cookies disclaimer wasn't found");
            }
        }

        public class ValidateXbtn : BaseUi
        {

            [Test]
            public void CookiesDisclaimer_ValidateXbtn()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                PostPage postpage = homePage.ClickOnCoverStory();
                Cookies cookies = new Cookies(_browser);
                cookies.ClickOnX();
                Assert.True(cookies.ValidateCookiesDisclaimerDisappears(1), "Cookies disclaimer wasn't found");
            }
        }
    }  
}