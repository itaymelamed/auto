using Automation.PagesObjects;
using NUnit.Framework;
using static Automation.PagesObjects.CastrPage;
using Automation.Helpersobjects;
using Automation.PagesObjects.CasterObjectsFolder;
using static Automation.PagesObjects.CasterObjectsFolder.CastrPost;
using Automation.PagesObjects.ExternalPagesobjects;

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
            [Retry(2)]
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
            [Retry(2)]
            public void Castr_FilterByLanguageEn()
            {
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
        public class Test3Class : Base
        {
            [Test]
            [Property("TestCaseId", "16")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
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
                CastrPage ariclesPosts = castrPage.SelectType(Types.article);

                Assert.True(ariclesPosts.ValidateFilterByType(Types.article), "Not all posts was Article type");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : Base
        {
            [Test]
            [Property("TestCaseId", "17")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
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
                CastrPost post = new CastrPost(_browser);
                string acUrl = post.GetPostUrl();

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
            [Retry(2)]
            public void Castr_CheckArchiveStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser); 
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.ArchivePost();
                CastrPage archivedPosts = newPosts.SelectStatus(Statuses.archived);

                Assert.True(archivedPosts.SearchPostByTitle(postCreator.Title), "Post was not shown under 'Archive' after archived.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : Base
        {
            [Test]
            [Property("TestCaseId", "22")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_CheckReset()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
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
        public class Test7Class : Base
        {
            [Test]
            [Property("TestCaseId", "23")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_CheckNewStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);

                Assert.True(newPosts.SearchPostByTitle(postCreator.Title), "Post was not shown under 'New' after posted.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : Base
        {
            [Test]
            [Property("TestCaseId", "25")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_CheckPublishStatus()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
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
        public class Test10Class : Base
        {
            [Test]
            [Property("TestCaseId", "28")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_CheckControlsAreDissabledOnPublishPost()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
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
        public class Test11Class : Base
        {
            [Test]
            [Property("TestCaseId", "40")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_ResetAndFeatureAPost()
            {
                var feedUrl = _params["PremierLeague"].ToString();
                var feedUrl2 = _params["ChampionLeague"].ToString();

                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPostToFeed(LeaguePages.ftbpro, 1);
                CastrPage publishedPosts = newPosts.SelectStatus(Statuses.published);
                post = publishedPosts.ClickOnPost(postCreator.Title);
                post.ResetPost();
                newPosts = newPosts.SelectStatus(Statuses.New);
                post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishPostToFeed(LeaguePages.ftbpro, 0);
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
        public class Test12Class : Base
        {
            [Test]
            [Property("TestCaseId", "41")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Retry(2)]
            public void Castr_Social_Networks_Facebook()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GoToCastr();
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                CastrPost post = newPosts.ClickOnPost(postCreator.Title);
                post.PublishToSocialNetwork(0, 2, 0);
                _browser.Navigate(_config.ConfigObject.FacebookUrl);
                FacebookAppPage facebookAppPage = new FacebookAppPage(_browser);
                facebookAppPage.ScrollToPost();

                Assert.True(facebookAppPage.ValidatePostTitle(postCreator.Title));
                Assert.True(facebookAppPage.VlidatePostDetails(postCreator.Title), "Post title was not shown in facebook post body.");

                PostPage postPage2 = facebookAppPage.ClickOnPost(postCreator.Title);
                Assert.True(postPage2.ValidatePostCreated(postCreator.Title), "User has not redirected to post.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test13Class : Base
        {
            [Test]
            [Property("TestCaseId", "42")]
            [Category("Sanity")][Category("Admin")][Category("Castr")]
            [Ignore("Bug")]
            public void Castr_Social_Networks_Twitter()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
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
        public class Test14Class : Base
        {
            [Test]
            [Property("TestCaseId", "43")]
            [Category("Sanity")][Category("Admin")][Category("Castr")][Category("Ftb90")]
            public void Castr_Ftb90_PublishPostInEditor()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GotoCastrByUrl("");
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);
                Assert.True(newPosts.SearchPostByTitle(postCreator.Title), "Post was not found under status new after published through editor.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : Base
        {
            [Test]
            [Property("TestCaseId", "44")]
            [Category("Sanity")][Category("Admin")][Category("Castr")][Category("Ftb90")]
            public void Castr_Ftb90_PublishPostInEditor()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                PostPage postPage = postCreator.Create(typeof(ArticleBase));
                CastrPage castrPage = homePage.GotoCastrByUrl("");
                CastrPage newPosts = castrPage.SelectStatus(Statuses.New);

            }
        }
    }
}
