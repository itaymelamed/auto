using System;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.EITestsFolder
{
    public class EditorTests
    {
        [TestFixture]
        [Parallelizable]
        public class EILogin : BaseUi
        {
            [Test]
            [Property("TestCaseId", "143")]
            [Category("EI")]
            [Retry(1)]
            public void EI_Login()
            {
                _browser.Navigate(_config.Url + "/management");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                ManagementPage managementPage = loginPage.LoginEI(_config.ConfigObject.Users.AdminUser);
                string url = _browser.GetUrl();
                Assert.True(url == _config.Url + "/management", $"User was not login to the management page. Expected:{_config.Url}/management. but actual:{url}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EIArticleFullFlow : BaseUi
        {
            [Test]
            [Property("TestCaseId", "144")]
            [Category("EI")]
            [Retry(1)]
            public void EI_Article_FullFlow()
            {
                _browser.Navigate(_config.Url + "/management");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                ManagementPage managementPage = loginPage.LoginEI(_config.ConfigObject.Users.AdminUser);


            }
        }
    }
}