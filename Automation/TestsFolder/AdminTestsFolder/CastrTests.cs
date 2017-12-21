using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    [TestFixture]
    public class CastrTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : Base
        {
            [Test]
            [Property("TestCaseId", "14")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_NavigateToCastrFromAdminPage()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                homePageConnected.HoverOverUserProfilePic();
                AdminPage adminPage = homePageConnected.ClickOnAdmin();
                CastrPage castrPage = adminPage.ClickOnCasterLink();

                Assert.True(castrPage.ValidateCasterPage());
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : Base
        {
            [Test]
            [Property("TestCaseId", "15")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_FilterByLanguageEn()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                homePageConnected.HoverOverUserProfilePic();
                AdminPage adminPage = homePageConnected.ClickOnAdmin();
                CastrPage castrPage = adminPage.ClickOnCasterLink();
                castrPage.FilterByLanguage("en");
                var errors = castrPage.ValidateFilterByLanguageEn();

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}
