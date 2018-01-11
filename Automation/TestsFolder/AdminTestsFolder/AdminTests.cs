using Automation.PagesObjects;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;

namespace Automation.TestsFolder.AdminTestsFolder
{
    [TestFixture]
    public class AdminTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")][Category("Admin")][Category("AllBrands")]
            [Retry(2)]
            public void Admin_LoginAsAdmin()
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
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "2")]
            [Category("Sanity")][Category("Admin")][Category("AllBrands")]
            [Retry(2)]
            public void Admin_TemplatesValidateAdminTemplates()
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
