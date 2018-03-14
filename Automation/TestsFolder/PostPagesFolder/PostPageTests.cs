using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Linq;

namespace Automation.TestsFolder.PostPagesFolder
{
    [TestFixture]
    public class PostPageTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("AllBrands")]
            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                var postTitle = "VIDEO:Test post article";
                BsonArray components = _params["Components"].AsBsonArray;

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle(postTitle);
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                _browser.ProxyApi.NewHar();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                var postId = postPage.GetPostId();
                var errors = postPage.ValidateComponents(components);
                Assert.True(string.IsNullOrEmpty(errors), errors);

                var counterRequest = _browser.ProxyApi.GetRequests().Where(r => r.Url.Contains("counter") && r.Url.Contains("reads") && r.Url.Contains(postId));
                Assert.True(counterRequest.Count() != 0, "A request to counter reads service was not sent.");
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("AllBrands")]
            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                var exCurLanguage = "EN";
                PostPage postPage = new PostPage(_browser);
                bool result = postPage.ValidateCurrentLangaugeDropDown(exCurLanguage);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test101Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "101")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("12up")]
            [Category("90Min")]
            [Category("90MinIn")]
            [Category("Ftb90")]
            [Category("Floor8")]


            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                //click on lang
                // new header page
                // curl lang = en
                // curl =/ list
                // curl langs

                var languages = _params["languageDropDownNames"].AsBsonArray;
                PostPage postPage = new PostPage(_browser);
                bool result = postPage.ValidateLanguageDropDownLangauge(languages);
                Assert.True(result);
            }
        }
    }
}