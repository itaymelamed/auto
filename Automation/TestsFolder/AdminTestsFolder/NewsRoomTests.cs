using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    public class NewsRoomTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "14")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Auth0")]
            [Category("MMNews")]
            [Category("Pluralist")]
            [Retry(2)]
            public void Auth0_Login_Admin()
            {
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                Assert.True(newsRoomPage.ValidateEditorBtn(), "Login failed");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "56")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("MMNews")]
            [Category("FullFlow")]
            [Category("Pluralist")]
            [Retry(2)]
            public void Editor_Article_FullFlow()
            {
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = newsRoomPage.ClickOnEditorBtn();
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                string body = _params["Body"].ToString();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteTitle("VIDEO:test article template");
                articleBase.SearchImage("cats");
                CropImagePopUp cropImagePopUp = articleBase.DragImage(0);
                cropImagePopUp.ClickOnCropImageBtn();
                cropImagePopUp.ClickOnEditokBtn();
                articleBase.WriteDec(body);
                articleBase.WriteTags(tagExValue);
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                Assert.True(postPage.ValidatePostCreated("VIDEO:test article template"));
                var errors = postPage.ValidateComponents(_params["Components"].AsBsonArray);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "57")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("MMNews")]
            [Category("FullFlow")]
            [Category("Pluralist")]
            [Retry(2)]
            public void Editor_List_FullFlow()
            {
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = newsRoomPage.ClickOnEditorBtn();
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                BsonArray titles = _params["Titles"].AsBsonArray;
                string body = _params["Body"].ToString();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.WriteTitle("VIDEO:test list template");
                listsTemplate.SearchImage("cats");
                listsTemplate.DragImages();
                listsTemplate.SetSubTitles(titles);
                listsTemplate.SetBodyTextBoxsMmNews(body);
                listsTemplate.WriteTags(tagExValue);
                listsTemplate.ClickOnAscendingBtn();
                listsTemplate.ClickOnDscBtn();
                PreviewPage previewPage = listsTemplate.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                Assert.True(postPage.ValidatePostCreated("VIDEO:test list template"), "Post was not created");
                var errors = postPage.ValidateComponents(_params["Components"].AsBsonArray);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "58")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("MMNews")]
            [Category("FullFlow")]
            [Category("Pluralist")]
            [Retry(2)]
            public void Editor_FullFlow_SlideShow()
            {
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = newsRoomPage.ClickOnEditorBtn();
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                BsonArray titles = _params["Titles"].AsBsonArray;
                string body = _params["Body"].ToString();
                SlideShowPage slideShow = editorPage.ClickOnSlideShow();
                slideShow.WriteTitle("VIDEO:test slideshow template");
                slideShow.SearchImage("cats");
                slideShow.DragAndDropImages();
                slideShow.SetSubTitles(titles);
                slideShow.SetBodyTextBoxsSlide(body);
                slideShow.WriteTags(tagExValue);
                slideShow.ClickOnAscendingBtn();
                slideShow.ClickOnDscBtn();
                PreviewPage previewPage = slideShow.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                Assert.True(postPage.ValidatePostCreated("VIDEO:test slideshow template"), "Post was not created");
                var errors = postPage.ValidateComponents(_params["Components"].AsBsonArray);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}