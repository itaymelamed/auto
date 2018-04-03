using Automation.PagesObjects;
using NUnit.Framework;
using static Automation.PagesObjects.CastrPage;
using Automation.Helpersobjects;
using Automation.PagesObjects.CasterObjectsFolder;
using static Automation.PagesObjects.CasterObjectsFolder.CastrPost;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Collections.Generic;
using Automation.ApiFolder;
using Automation.ApiFolder.FacebookApi;
using System.Threading;
using System;

namespace Automation.TestsFolder.AdminTestsFolder
{
    public class CastrTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "14")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_NavigateToCastrFromAdminPage()
            {
                _browser.Navigate(_config.Url);
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
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "15")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("90Min")]
            [Retry(2)]
            public void Castr_FilterByLanguageEn()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                homePage.HoverOverUserProfilePic();
                AdminPage adminPage = homePage.ClickOnAdmin();
                CastrPage castrPage = adminPage.ClickOnCasterLink();
                castrPage.FilterByLanguage("en");
                CastrPage englishPosts = new CastrPage(_browser);
                var errors = englishPosts.CheckAllPostsInEnglish();

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "16")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_FilterByType_Article()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                homePageConnected.HoverOverUserProfilePic();
                AdminPage adminPage = homePageConnected.ClickOnAdmin();
                CastrPage castrPage = adminPage.ClickOnCasterLink();
                castrPage.DeselectAllCheckBoxes();
                CastrPage ariclesPosts = castrPage.SelectType(Types.article);

                Assert.True(ariclesPosts.ValidateFilterByType(Types.article), "Not all posts was Article type");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "17")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_ValidatePostUrl()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                string exUrl = _browser.GetUrl();
                postPage.HoverOverOptions();
                CastrPage CasterPage = postPage.ClickOnOpenInCaster();
                _browser.SwitchToLastTab();
                CastrPost post = new CastrPost(_browser);
                string acUrl = post.GetPostUrl().Replace("https", "http");

                Assert.AreEqual(exUrl, acUrl);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "20")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Ignore("Bug")]
            [Retry(2)]
            public void Castr_CheckArchiveStatus()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.CheckPost(postCreator.Title);
                post.ArchivePost();
                Thread.Sleep(5000);
                CastrPage archivedPosts = newPosts.SelectStatus(Statuses.archived);

                Assert.True(archivedPosts.SearchPostByTitle(postCreator.Title), "Post was not shown under 'Archive' after archived.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "22")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_CheckReset()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPost();
                CastrPage publishedPosts = castrPage.SelectStatus(Statuses.published);
                post = publishedPosts.ClickOnPost(postCreator.Title);
                post.ResetPost();
                Assert.True(castrPage.ValidateSucMsg(), "Post reset suc message hasn't shown");

                CastrPage archivedPosts = publishedPosts.SelectStatus(Statuses.New);
                Assert.True(archivedPosts.SearchPostByTitle(postCreator.Title), "Post was not shown under 'New' after reseted.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "23")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_CheckNewStatus()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);

                Assert.True(newPosts.SearchPostByTitle(postCreator.Title), "Post was not shown under 'New' after posted.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "25")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_CheckPublishStatus()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPost();
                CastrPage publishedPosts = castrPage.SelectStatus(Statuses.published);

                Assert.True(publishedPosts.SearchPostByTitle(postCreator.Title), "Was was not shown under 'Published' after published.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test10Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "28")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Castr_CheckControlsAreDissabledOnPublishPost()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPost();
                CastrPage publishedPosts = newPosts.SelectStatus(Statuses.published);
                post = publishedPosts.ClickOnPost(postCreator.Title);

                Assert.True(post.ValidateTextAreasDissabled() && post.ValidateInputDissabled() && post.ValidateControlsDissabled(), "Controls were not dissabled.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "40")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("90Min")]
            [Retry(2)]
            public void Castr_ResetAndFeatureAPost()
            {
                var feedUrl = _params["PremierLeague"].ToString();
                var feedUrl2 = _params["ChampionLeague"].ToString();

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);

                post.UnCheckLeague(0);
                post.PublishPostToFeed(Platforms.ftbpro, 1);
                CastrPage publishedPosts = newPosts.SelectStatus(Statuses.published);
                post = publishedPosts.ClickOnPost(postCreator.Title);
                post.ResetPost();
                newPosts = newPosts.SelectStatus(Statuses.New);
                post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPostToFeed(Platforms.ftbpro, 0);

                _browser.Navigate($"{_config.Url}/{feedUrl}");
                FeedPage feedPage = new FeedPage(_browser);
                Assert.True(feedPage.ValidateArticleByTitle(postCreator.Title), $"Post {postCreator.Title} was not shown on {feedUrl} after it was reseted.");

                feedPage = new FeedPage(_browser);
                _browser.Navigate($"{_config.Url}/{feedUrl2}");
                Assert.True(feedPage.ValidateArticleByTitle(postCreator.Title), $"Post {postCreator.Title} was not shown on {feedUrl2} after it was reseted.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test12Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "41")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("90Min")]
            [Retry(2)]
            public void Castr_Social_Networks_Facebook()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetwork(0, 2, 0);
                FacebookClient facebookClient = new FacebookClient();
                Assert.True(facebookClient.SearchPost(postCreator.Title), $"Post {postCreator.Title} was not created");
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Test13Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "42")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("90Min")]
            [Ignore("Bug")]
            public void Castr_Social_Networks_Twitter()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetwork(0, 2, 1);
                _browser.Navigate(_config.ConfigObject.TwitterUrl);
                TwitterAppPage twitterAppPage = new TwitterAppPage(_browser);
                PostPage postPage2 = twitterAppPage.ClickOnTweetLink(postCreator.Title);
                Assert.True(postPage2.ValidatePostCreated(postCreator.Title), "User has not redircted to post after click on twitter link.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "45")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Ftb90")]
            [Retry(2)]
            public void Castr_Ftb90_PublishIdPost()
            {
                _browser.Navigate("http://" + _config.Env + "." +_config.GlobalConfigObject["90Min"]["Url"]);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.RegularUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.CreateToDomain("90Min");
                var url = $"http://{_config.Env}.{_config.GlobalConfigObject["Ftb90"]["Url"]}";
                _browser.Navigate(url);
                HomePage homePageFtb = new HomePage(_browser);
                homePage.ClickOnConnectBtnWithCoockies();
                CastrPage castrPage = homePageFtb.GoToCastr();
                CastrPage newPosts = new CastrPage(_browser);
                Assert.True(newPosts.SearchPostByTitle(postCreator.Title), "Post was not found under status new after published through editor-Id.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test16Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "46")]
            [Category("Sanity")][Category("Admin")][Category("Castr")][Category("Ftb90")]
            [Retry(2)]
            public void Castr_FTB90_CheckIDPostsFromDiffrentDomains()
            {
                _browser.Navigate(_config.GlobalConfigObject["90Min"]["Url"].ToString());
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                var url = $"http://{_config.Env}.{_config.GlobalConfigObject["Ftb90"]["Url"]}";
                _browser.Navigate(url);
                HomePage homePageFtb = new HomePage(_browser);
                homePage.ClickOnConnectBtnWithCoockies();
                PostCreator postCreatorFtb90 = new PostCreator(_browser);
                PostPage postPageFTb90 = postCreatorFtb90.Create(typeof(ArticleBase));
                CastrPage castrPage = homePageFtb.GoToCastr();
                CastrPage newPosts = new CastrPage(_browser);
                CastrPost castrPostFTB90 = newPosts.ClickOnPost(postCreatorFtb90.Title);
                Assert.True(castrPostFTB90.ValidatePublishAlsoTo());
                CastrPost castrPost = newPosts.ClickOnPost(postCreator.Title);
                Assert.True(castrPostFTB90.ValidatePublishAlsoTo());
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test17Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "47")]
            [Category("Sanity")][Category("Admin")][Category("Castr")][Category("Ftb90")]
            [Retry(2)]
            public void Castr_FTB90_PublishAnIdPostFromFTB90Caster()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = new CastrPage(_browser);
                newPosts.ClickOnPost(postCreator.Title);
                FtbPost castrPostFTB90 = new FtbPost(_browser);
                castrPostFTB90.CheckpublishToCategoryCb();
                castrPostFTB90.PublishPostToTeam(1, 0, new List<int> { 0, 1 }, "l");
                castrPostFTB90.ValidateSucMsg();

                JsonHelper jsonHelper = new JsonHelper(179, postCreator.Title);
                Assert.True(jsonHelper.SearchArticleInFeed(), $"Post '{postCreator.Title}' was not published to team's feed.");

                jsonHelper = new JsonHelper(1, postCreator.Title);
                Assert.False(jsonHelper.SearchArticleInFeed(0), "Post apeared on an unchecked team's feed.");

                jsonHelper = new JsonHelper(2, postCreator.Title);
                Assert.False(jsonHelper.SearchArticleInFeed(0), "Post apeared on an unchecked team's feed.");

                jsonHelper = new JsonHelper("id", 179, "90Min", postCreator.Title);
                Assert.False(jsonHelper.SearchArticleInFeed(0), "Post apeared on 90Min team's feed.");

                jsonHelper = new JsonHelper("en", 179, "90Min", postCreator.Title);
                Assert.False(jsonHelper.SearchArticleInFeed(0), "Post apeared on 90Min team's feed.");

                jsonHelper = new JsonHelper("lists", postCreator.Title);
                Assert.True(jsonHelper.SearchArticleInFeed(), $"Post '{postCreator.Title}' was not published to List catageory feed.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test18Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "49")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("AllBrands")]
            [Retry(3)]
            public void Castr_PnPost()
            {
                string urbanAirShipUrl = _params["Url"].ToString();
                var user = _params["Credentials"];

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);

                post.SendPn(Platforms.mobile, 0);
                _browser.Navigate($"{_config.Url}/management/push_notifications?test=test");
                PnDashBoardPage pnDashBoardPage = new PnDashBoardPage(_browser);
                Assert.True(pnDashBoardPage.ValidatePost(0, postCreator.Title));

                _browser.Navigate(urbanAirShipUrl);
                UrbanAirShipLoginPage urbanAirShipLoginPage = new UrbanAirShipLoginPage(_browser);
                urbanAirShipLoginPage.Login(user);
                Assert.True(urbanAirShipLoginPage.SearchPost(postCreator.Title));
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Test19Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "50")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Caster_Schedulr_ValidateTime()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                homePage.HoverOverUserProfilePic();
                AdminPage adminPage = homePage.ClickOnAdmin();
                SchedulrPage schedulrPage =  adminPage.ClickOnSchedulrLink();
                var errors = schedulrPage.ValidateTime();

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test20Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "51")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Caster_Schedulr_ValidateLeagues()
            {
                _browser.Navigate(_config.Url);
                var leagues = _params["Leagues"].AsBsonArray;
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                homePage.HoverOverUserProfilePic();
                AdminPage adminPage = homePage.ClickOnAdmin();
                SchedulrPage schedulrPage = adminPage.ClickOnSchedulrLink();

                Assert.True(schedulrPage.ValidateLeagues(leagues));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test21Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "52")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Caster_Schedulr_SocialNetworks()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetworks(0, 2);
                _browser.Navigate($"{ _config.Url}/management/schedulr");
                SchedulrPage schdulrPage = new SchedulrPage(_browser);

                Assert.True(schdulrPage.ValidatePostFacebook(postCreator.Title) && schdulrPage.ValidatePostTwitter(postCreator.Title));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test22Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "53")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Caster_Schedulr_ValidateDate()
            {
                _browser.Navigate(_config.Url);
                var curYear = DateTime.Now.Year;
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetworks(0, 2);
                _browser.Navigate($"{ _config.Url}/management/schedulr");
                SchedulrPage schdulrPage = new SchedulrPage(_browser);
                schdulrPage.SelectYear(curYear + 1);
                SchedulrPage schedulrPageNewDate = schdulrPage.ClickOnGoBtn();

                Assert.False(schedulrPageNewDate.ValidatePostFacebook(postCreator.Title, 0, false) && schedulrPageNewDate.ValidatePostTwitter(postCreator.Title, 0, false));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test23Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "54")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Caster_Schedulr_ValidateLeaguesFilter()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetworks(0, 2);
                _browser.Navigate($"{ _config.Url}/management/schedulr");
                SchedulrPage schdulrPage = new SchedulrPage(_browser);
                schdulrPage = schdulrPage.SelectLeague(1);

                Assert.False(schdulrPage.ValidatePostFacebook(postCreator.Title, 0, false) && schdulrPage.ValidatePostTwitter(postCreator.Title, 0, false));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test24Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "52")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Castr")]
            [Category("Scedulr")]
            [Retry(2)]
            public void Caster_Schedulr_PostBox()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                postCreator.Create();
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetworksSchedul(0, 2, "16");
                _browser.Navigate($"{ _config.Url}/management/schedulr");
                SchedulrPage schdulrPage = new SchedulrPage(_browser);

                Assert.False(schdulrPage.ValidatePostFacebook(postCreator.Title) && schdulrPage.ValidatePostTwitter(postCreator.Title));
            }
        }
    }
}