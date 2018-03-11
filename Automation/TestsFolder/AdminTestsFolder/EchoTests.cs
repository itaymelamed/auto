using System;
using Automation.Helpersobjects;
using Automation.PagesObjects;
using Automation.PagesObjects.EchoFolder;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    [TestFixture]
    public class EchoTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "89")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
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
        //this test will fail until we will fix the https in pluralist that return error page
        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "90")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
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
            [Property("TestCaseId", "91")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
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
                Assert.True(echoPage.ValidateDomain($"{_config.SiteName.ToLower()}.com", title), $"The domain for {title} was diffrent than expected."); 
            }

        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "92")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
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
            [Property("TestCaseId", "93")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateStatusPublished()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                echoPage = echoPage.ClickOnEchoBtn();
                Assert.True(echoPage.ValidateSatatus("Published", title),$"The status for {title} was diffrent then Published."); 
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "94")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_FeaturePostToCoverStory()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                echoPage = distributionPage.ClickOnEchoBtn();
                _browser.OpenNewTab(_config.Url);
                HomePage homePage = new HomePage(_browser); 
                Assert.True(homePage.GetCoverText() == title, $"Expected title was {title} but actual is {homePage.GetCoverText()}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "95")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_FeaturePostToTopStories()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                _browser.OpenNewTab(_config.Url);
                HomePage homePage = new HomePage(_browser); 
                Assert.True(homePage.ValidateTopStoriesTitle(title), $"Expected {title} was not found");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "98")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_FeaturePostToMoreNews()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                distributionPage.WaitForPublishedSatatus();
                _browser.OpenNewTab(_config.Url);
                HomePage homePage = new HomePage(_browser); 
                Assert.True(homePage.ValidateMoreNewsTitle(title), $"Expected {title} was not found");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test10Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "96")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_FeaturePostToCategory()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                distributionPage.WaitForPublishedSatatus();
                _browser.OpenNewTab($"{_config.Url}/channels/latest");
                FeedPage feedPage = new FeedPage(_browser); 
                Assert.True(feedPage.ValidatePostTitleInFeedPage(title), $"Expected {title} was not found");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "97")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_FeaturePostToTwoCategories()
            {
                var channelIndex1 = _params["ChannelIndex1"].AsInt32;
                var channelIndex2 = _params["ChannelIndex2"].AsInt32;

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
                distributionPage.SelectChannelByIndex(channelIndex1);
                distributionPage.SelectChannelDPOpen(channelIndex2);
                distributionPage.ClickOnPublishBtn();
                distributionPage.WaitForPublishedSatatus();
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                Assert.True(homePage.ValidateTitleApearsInGrid(title),$"Expected {title} was not found");
                Assert.True(homePage.ValidateMoreNewsTitle(title),"The title was not appear on the grid section");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test12Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "99")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_UnpblishPost()
            {
                var channelIndex = _params["ChannelIndex"].AsInt32;
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
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                distributionPage.WaitForPublishedSatatus();
               // _browser.Navigate(_config.Url);
               // HomePage homePage = new HomePage(_browser);
              //  Assert.True(homePage.ValidateTitleApearsInGrid(title), $"Expected {title} was not found");
                distributionPage.UnpublishPost();
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                Assert.False(homePage.ValidateMoreNewsTitle(title), "The title was not appear on the grid section");

            }
        }
    }
}