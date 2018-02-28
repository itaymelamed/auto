using System;
using Automation.Helpersobjects;
using Automation.PagesObjects;
using Automation.PagesObjects.EchoFolder;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    public class EchoTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_ValidatePostCreation()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                Assert.True(echoPage.ValidatePostCreation(title), $"post {title} is not shown on echo page.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_ValidateAuthorName()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                string authorName = postPage.GetAuthorName();
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                Assert.True(echoPage.ValidateAuthor(authorName, title), $"The author name for {title} was diffrent than expected."); 
            }
        }

         [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_ValidateDomain()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                Assert.True(echoPage.ValidateDomain($"{_config.SiteName}.com", title), $"The domain for {title} was diffrent than expected."); 
            }

        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_ValidateStatusNew()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                Assert.True(echoPage.ValidateSatatus("New", title), $"The status for {title} was diffrent then New.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_ValidateStatusPublished()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                DistributionPage distributionPage = echoPage.SelectPost(title);
                distributionPage.PublishPost(0);
                echoPage = echoPage.ClickOnEchoBtn();
                Assert.True(echoPage.ValidateSatatus("Published", title),$"The status for {title} was diffrent then Published."); 
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Echo_FeaturePostToCoverStory()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                DistributionPage distributionPage = echoPage.SelectPost(title);
                distributionPage.PublishPost(0);
                echoPage = echoPage.ClickOnEchoBtn();
                _browser.OpenNewTab(_config.Url);
                Assert.True(echoPage.GetTitleText() == title, $"Expected title was {title} but actual is {echoPage.GetTitleText()}");
            }
        }
    }
}