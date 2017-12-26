using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.TestsFolder.PostPagesFolder
{
    [TestFixture]
    public class PostPageTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test11Class : Base
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")][Category("Admin")][Category("PostPage")]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                var postTitle = "VIDEO:Test post article";
                BsonArray components = _params["Components"].AsBsonArray;

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle(postTitle);
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();

                var errors = postPage.ValidateComponents(components);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}
