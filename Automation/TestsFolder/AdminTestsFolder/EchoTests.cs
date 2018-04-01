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
            [Ignore("Aut-132")]
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
                distributionPage.WaitForPublishedSatatus();
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
            [Retry(3)]
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
        public class Test9Class : BaseUi
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
        public class Test10Class : BaseUi
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
        public class Test11Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "100")]
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
                distributionPage.UnpublishPost();
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                Assert.False(homePage.ValidateMoreNewsTitle(title), "The title was not appear on the grid section");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test12Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "102")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateOpenLink()
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
                distributionPage.ClickOnOpenLink();
                _browser.SwitchToLastTab();
                string url = _browser.GetUrl();
                var urlSplitted =  url.Split('/');
                var parseTitle = urlSplitted[4].Split('-');
                Assert.True(parseTitle[1].ToLower() == title.ToLower(), $"Expected title was {title} but actual is {parseTitle[1]}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test13Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "103")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateEditLink()
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
                distributionPage.ClickOnEditLink();
                _browser.SwitchToTab(2, 3);
                string editor = "editor";
                string url = _browser.GetUrl();
                var urlSplitted = url.Split('/');
                 var parseTitle = urlSplitted[4].Split('/');
                Assert.True(parseTitle[0].ToLower() == editor.ToLower(), $"Expected  was {editor} but actual is {parseTitle[0]}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test14Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "104")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateEditLinkInTheEchoPage()
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
                echoPage.ClickOnEditButtonInEcho(title);
                _browser.SwitchToTab(2, 3);
                string url = _browser.GetUrl();
                var urlSplitted = url.Split('/');
                var parseTitle = urlSplitted[4].Split('/');
                Assert.True(parseTitle[0].ToLower() == "editor", $"Expected was editor but actual is {parseTitle[0]}");

                ArticleBase article = new ArticleBase(_browser);
                var titleEditor = article.GetTitleValue().ToLower();
                Assert.True(titleEditor == title.ToLower(), $"Expected title was {title.ToLower()}. Actual: {titleEditor}");
            }  
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "106")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateLogoutLink()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                echoPage.ClickOnLogoutButton();
                loginPage = new Auth0LoginPage(_browser);
                Assert.True(loginPage.ValidateAuto0Page());
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test16Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "127")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_RepublishPostToNewAFeed()
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
                distributionPage.ClickOnPublishBtn();
                distributionPage.WaitForPublishedSatatus();
                _browser.OpenNewTab(_config.Url);
                HomePage homePage = new HomePage(_browser);
                Assert.True(homePage.ValidateMoreNewsTitle(title), $"Expected {title} was not found");

                _browser.SwitchToFirstTab();
                _browser.Refresh();
                 echoPage = new EchoPage(_browser);
                distributionPage.ClickOnNewButton();
                distributionPage.SelectChannelByIndex(channelIndex2);
                distributionPage.ClickOnPublishBtn();
                _browser.OpenNewTab($"{_config.Url}/channels/latest");
                FeedPage feedPage = new FeedPage(_browser);
                _browser.Refresh();
                Assert.True(feedPage.ValidatePostTitleInFeedPage(title), $"Expected {title} was not found");
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test17Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "128")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateLanguageFilterList()
            {
                var langunges = _params["LagnungesList"].AsBsonArray;
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                echoPage.ClickOnLangnugeFilter();
                Assert.True(echoPage.ValidateLanguageFilterList(langunges), "The languages in dropdown didn't match the expected result.");
              
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test18Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "134")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateStatusFilterList()
            {
                var status = _params["StatusList"].AsBsonArray;
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                echoPage.ClickOnSatusFilter();
                Assert.True(echoPage.ValidateStatusFilterList(status), "The status in dropdown didn't match the expected result.");
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test19Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "135")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateEnglishFilter()
            {
                var language = _params["Language"].ToString();
                var languageChannel = _params["LanguageChannel"].ToString();
                var channelIndex = _params["ChannelIndex"].AsInt32;
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                PostPage postPage = new PostPage(_browser);
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                echoPage = new EchoPage(_browser);
                DistributionPage distributionPage = echoPage.SelectPost(title);
                distributionPage.SelectChannelByIndex(channelIndex);
                distributionPage.ClickOnPublishBtn();
                echoPage = echoPage.ClickOnEchoBtn();
                echoPage.ClickOnLangnugeFilter();
                echoPage = echoPage.ClickOnLanguage(language);
                distributionPage = echoPage.SelectPost(title);
                Assert.True(distributionPage.ValidateChannelLanguage(languageChannel),$"The post {title} was not in {languageChannel}"); 
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test20Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "136")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidatePublishedFilter()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                echoPage.ClickOnSatusFilter();
                echoPage = echoPage.ClickOnStatus("Published");
                Assert.True(echoPage.ValidateStatusses("Published"), "The satatus of the post is wrong");
            }
        } 

        [TestFixture]
        [Parallelizable]
        public class Test21Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "138")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("Pluralist")]
            [Category("Floor8")]
            [Retry(2)]
            public void Echo_ValidateNewSatatusFilter()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EchoPage echoPage = new EchoPage(_browser);
                echoPage.ClickOnSatusFilter();
                echoPage = echoPage.ClickOnStatus("New");
                Assert.True(echoPage.ValidateStatusses("New"), "The satatus of the post is wrong");
            }
        } 
    }
}