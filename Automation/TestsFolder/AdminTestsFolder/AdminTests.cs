using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    [TestFixture]
    public class AdminTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : Base
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")][Category("Admin")]
            public void LoginAsAdmin()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                homePageConnected.HoverOverUserProfilePic();

                Assert.IsTrue(homePageConnected.ValidateAdminAppears(), "Admin button does not apear.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : Base
        {
            [Test]
            [Property("TestCaseId", "2")]
            [Category("Sanity")][Category("Admin")]
            public void ValidateAdminTemplates()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                var errors = editorPage.Validatetemplates(_params["templatesNames"].AsBsonArray);

                Assert.IsTrue(errors == "", errors);
            }
        }
    }
}
