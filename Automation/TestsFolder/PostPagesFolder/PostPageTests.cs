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
        public class Test1Class : Base
        {
            [Test]
            [Property("TestCaseId","3")]
            [Category("Sanity")][Category("Admin")][Category("editPage")]
            public void ArticleValidateInputFields()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();

                var errors = articleBase.ValidateFildes();
                Assert.IsTrue(errors == "", errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : Base
        {
            [Test]
            [Property("TestCaseId", "4")]
            [Category("Sanity")][Category("Admin")][Category("editPage")]
            public void ValidateTitleTextfieled()
            {
                var titleExValue = _params["titleExValue"].ToString();

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteTitle(titleExValue);
                var titleAcValue = articleBase.GetTitleValue();

                Assert.AreEqual(titleExValue, titleAcValue);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : Base
        {
            [Test]
            [Property("TestCaseId", "5")]
            [Category("Sanity")][Category("Admin")][Category("editPage")]
            public void ValidateBodyTextfieled()
            {
                var bodyExValue = _params["bodyExValue"].ToString();

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteDec(bodyExValue);
                var bodyAcValue = articleBase.GetBodyValue();

                Assert.AreEqual(bodyExValue, bodyAcValue);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : Base
        {
            [Test]
            [Property("TestCaseId", "6")]
            [Category("Sanity")][Category("Admin")][Category("editPage")][Category("Tags")]
            public void ValidateTagsTextBoxInsertSingleTag()
            {
                BsonArray tagExValue = _params["Tag"].AsBsonArray;

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteTags(tagExValue);
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle("VIDEO:Title Title Title");
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                string errors = postPage.ValidateTagsOnSourcePage(tagExValue);

                Assert.IsTrue(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : Base
        {
            [Test]
            [Property("TestCaseId", "7")]
            [Category("Sanity")][Category("Admin")][Category("editPage")][Category("Tags")]
            public void ValidateTagsTextBoxInsertMultipleTags()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteTags(tagExValue);
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle("VIDEO:Title Title Title");
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                string errors = postPage.ValidateTagsOnSourcePage(tagExValue);

                Assert.IsTrue(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : Base
        {
            [Test]
            [Property("TestCaseId", "8")]
            [Category("Sanity")][Category("Admin")][Category("editPage")][Category("Tags")]
            public void ValidateTagsTextBoxSugestionsTags()
            {
                string shortTag = _params["ShortTag"].ToString();
                string tag = _params["Tag"].ToString();

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteShortTags(shortTag);

                Assert.True(articleBase.ValidateAutoComplete(tag), $"Expected Tag {tag} was not shown on the suggestions.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : Base
        {
            [Test]
            [Property("TestCaseId", "9")]
            [Category("Sanity")][Category("Admin")][Category("editPage")]
            public void DragAndDropAcoverImage()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                CropImagePopUp cropImagePopUp = articleBase.DragImage(0);
                cropImagePopUp.ClickOnCropImageBtn();
                cropImagePopUp.ClickOnEditokBtn();

                Assert.IsTrue(articleBase.ValidateContainerImage(), "Container image was not exsists on image fieled after dragging.");
                Assert.IsTrue(articleBase.ValidateDeleteButtonCoverimage(), "Delete button on cover image was not exsist.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : Base
        {
            [Test]
            [Property("TestCaseId", "10")]
            [Category("Sanity")][Category("Admin")][Category("editPage")]
            public void ImageSearchValidateMaxSearchResultsAndContent()
            {
                int maxResults = _params["MaxResults"].ToInt32();
                string search = _params["Search"].ToString();

                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.SearchImage(search);

                Assert.AreEqual(articleBase.ValidateImageSearchResults(maxResults), maxResults);
                Assert.True(articleBase.ValidateImageContenet(search), "Results weren't related to the srearch");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test9Class : Base
        {
            [Test]
            [Property("TestCaseId", "11")]
            [Category("Sanity")][Category("Admin")][Category("PreviewPage")]
            public void PreviewButtonCheckEditButton()
            {
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle("VIDEO:Title Title Title");
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                ArticleBase articleBaseEdit = previewPage.ClickOnEditButton();

                Assert.IsTrue(articleBaseEdit.ValidateEditMode());
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test10Class : Base
        {
            [Test]
            [Property("TestCaseId", "12")]
            [Category("Sanity")][Category("Admin")][Category("PreviewPage")]
            public void PublishButton()
            {
                var postTitle = "VIDEO:Test post article";
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

                Assert.IsTrue(postPage.ValidatePostCreated(postTitle));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test11Class : Base
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")][Category("Admin")][Category("PostPage")]
            public void ValidateUiComponentsExistOnPage()
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
