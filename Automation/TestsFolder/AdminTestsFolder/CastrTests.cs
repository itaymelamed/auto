using Automation.PagesObjects;
using NUnit.Framework;
using static Automation.PagesObjects.CastrPage;
using Automation.Helpersobjects;

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

        [TestFixture]
        [Parallelizable]
        public class Test3Class : Base
        {
            [Test]
            [Property("TestCaseId", "16")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_FilterByType_Article()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                homePageConnected.HoverOverUserProfilePic();
                AdminPage adminPage = homePageConnected.ClickOnAdmin();
                CastrPage castrPage = adminPage.ClickOnCasterLink();
                castrPage.DeselectAllCheckBoxes();
                castrPage.SelectType(Types.article);

                Assert.True(castrPage.ValidateFilterByType(Types.article), "Not all posts was Article type");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : Base
        {
            [Test]
            [Property("TestCaseId", "17")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_ValidatePostUrl()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                string ecUrl = _browser.GetUrl();
                postPage.HoverOverOptions();
                CastrPage CasterPage = postPage.ClickOnOpenInCaster();
                _browser.SwitchToLastTab();
                string acUrl = CasterPage.GetUrl();

                Assert.AreEqual(ecUrl, acUrl);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : Base
        {
            [Test]
            [Property("TestCaseId", "20")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_CheckArchiveStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                CastrPage castrPage = homePage.GoToCastr();
                castrPage.ClickOnPost(0);
                var postUrl = castrPage.GetUrl();
                castrPage.CheckPost(0);
                castrPage.ArchivePost();

                Assert.True(castrPage.ValidateSucMsg(), "Post archive suc message hasn't shown");

                castrPage.SelectStatus(Statuses.archived);
                Assert.True(castrPage.ValidatePost(postUrl), "Post was not shown under 'Archive' after archived.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : Base
        {
            
            [Test]
            [Property("TestCaseId", "22")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_CheckReset()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                CastrPage castrPage = homePage.GoToCastr();
                castrPage.SelectStatus(Statuses.published);
                castrPage.ClickOnPost(0);
                var postUrl = castrPage.GetUrl();
                castrPage.CheckPost(0);
                castrPage.ResetPost();
                Assert.True(castrPage.ValidateSucMsg(), "Post reset suc message hasn't shown");

                castrPage.SelectStatus(Statuses.New);
                Assert.True(castrPage.ValidatePost(postUrl), "Post was not shown under 'New' after rested.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : Base
        {
            [Test]
            [Property("TestCaseId", "23")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_CheckNewStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                var postUrl = _browser.GetUrl();
                CastrPage castrPage = homePage.GoToCastr();
                castrPage.SelectStatus(Statuses.New);
                Assert.True(castrPage.ValidatePost(postUrl), "Post was not shown under 'New' after created.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : Base
        {
            [Test]
            [Property("TestCaseId", "25")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            public void Castr_CheckPublishStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                CastrPage castrPage = homePage.GoToCastr();
                castrPage.SelectStatus(Statuses.New);
                castrPage.ClickOnPost(0);
                var postUrl = castrPage.GetUrl();
                castrPage.CheckLeague(0);
                castrPage.CheckPublishTo(1);
                castrPage.UncheckPublishToFtb();
                castrPage.PublishPost();

                Assert.True(castrPage.ValidateSucMsg(), "Post reset suc message hasn't shown");

                castrPage.SelectStatus(Statuses.published);
                Assert.True(castrPage.ValidatePost(postUrl), "Post was not shown under 'published' after publish.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test9Class : Base
        {
            [Test]
            [Property("TestCaseId", "26")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            public void Castr_CheckPublishStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                CastrPage castrPage = homePage.GoToCastr();
                castrPage.SelectStatus(Statuses.New);
                castrPage.ClickOnPost(0);
                var postUrl = castrPage.GetUrl();
                castrPage.CheckLeague(0);
                castrPage.CheckPublishTo(1);
                castrPage.PublishPost();

                Assert.True(castrPage.ValidateSucMsg(), "Post reset suc message hasn't shown");

                castrPage.SelectStatus(Statuses.published);
                Assert.True(castrPage.ValidatePost(postUrl), "Post was not shown under 'published' after publish.");
            }
        }
    }
}
