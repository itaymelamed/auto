using Automation.PagesObjects.ExternalPagesobjects;
using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.TestsFolder.EditortestsFolder
{
    public class EditorTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "3")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateInputFields()
            {
                _browser.Navigate(_config.Url);
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
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "4")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateTitleTextfieled()
            {
                var titleExValue = _params["titleExValue"].ToString();

                _browser.Navigate(_config.Url);
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
        public class Test3Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "5")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateBodyTextfieled()
            {
                var bodyExValue = _params["bodyExValue"].ToString();

                _browser.Navigate(_config.Url);
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
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "6")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("Tags")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxInsertSingleTag()
            {
                BsonArray tagExValue = _params["Tag"].AsBsonArray;

                _browser.Navigate(_config.Url);
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
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "7")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("Tags")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxInsertMultipleTags()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;

                _browser.Navigate(_config.Url);
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
        public class Test6Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "8")][Category("Sanity")][Category("Admin")][Category("Editor")][Category("Tags")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxSugestionsTags()
            {
                string shortTag = _params["ShortTag"].ToString();
                string tag = _params["Tag"].ToString();

                _browser.Navigate(_config.Url);
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
        public class Test7Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "9")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")]
            [Category("90Min")]
            [Retry(2)]
            public void Editor_Article_DragAndDropAcoverImage()
            {
                _browser.Navigate(_config.Url);
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
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "10")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ImageSearch_ValidateMaxSearchResultsAndContent()
            {
                int maxResults = _params["MaxResults"].ToInt32();
                string search = _params["Search"].ToString();

                _browser.Navigate(_config.Url);
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
        public class Test9Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "11")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PreviewPage")]
            [Category("Editor")]
            [Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_PreviewButtonCheckEditButton()
            {
                _browser.Navigate(_config.Url);
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
        public class Test10Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "12")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PreviewPage")]
            [Category("Editor")]
            [Category("AllBrands")]
            [Retry(2)]
            public void PostPage_Article_PublishButton()
            {
                var postTitle = "VIDEO:Test post article";

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
                PostPage postPage = previewPage.ClickOnPublishBtn();

                Assert.IsTrue(postPage.ValidatePostCreated(postTitle));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test13Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "29")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("AllBrands")]
            [Category("Editor")]
            [Retry(2)]
            public void Editor_List_ValidateTtitle()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.WriteTitle("Test Title Lists Template");

                Assert.True(listsTemplate.ValidateTitle(), "Title in text box was not as inserted.");
            }

        }


        [TestFixture]
        [Parallelizable]
        public class Test14Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "30")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_List_ValidateTextBoxsBodys()
            {
                string text = _params["text"].ToString();
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.SetBodyTextBoxs(text);
                List<string> acValues = listsTemplate.GetBodyTextBoxesValue();
                var errors = listsTemplate.ValidateBodyTextBoxes(acValues, text);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "31")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_List_ValidateSubTitelsFields()
            {
                BsonArray titles = _params["Titles"].AsBsonArray;
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.SetSubTitles(titles);
                List<string> acValues = listsTemplate.GetSubTitelsValues();

                Assert.True(listsTemplate.ValidateSubTitlesFields(acValues, titles), "Actual values are not as expected values");
            }

        }

        [TestFixture]
        [Parallelizable]
        public class Test16Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "33")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(3)]
            public void Editor_List_ValidateDragImages()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.DragImages();
                var listEditorImages = listsTemplate.GetImagesUrl();
                Assert.True(listEditorImages.Count == 4, $"Expected 4 images, but actual {listEditorImages.Count}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test17Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "35")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_List_ValidateAscendingOrder()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                List<string> before = listsTemplate.GetItemsIndex();
                listsTemplate.ClickOnAscendingBtn();
                List<string> after = listsTemplate.GetItemsIndex();

                Assert.True(listsTemplate.ValidateAscDesc(before, after), "The counter is not in ascending order.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test18Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "36")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_List_ValidateDescendingOrder()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.ClickOnAscendingBtn();
                List<string> before = listsTemplate.GetItemsIndex();
                listsTemplate.ClickOnDscBtn();
                List<string> after = listsTemplate.GetItemsIndex();

                Assert.True(listsTemplate.ValidateAscDesc(before, after), "The counter is not in descending order.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test19Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "37")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")]
            [Category("AllBrands")]
            [Category("FullFlow")]
            [Retry(2)]
            public void Editor_List_FullFlow()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                BsonArray titles = _params["Titles"].AsBsonArray;
                string body = _params["Body"].ToString();

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                listsTemplate.WriteTitle("VIDEO:test list template");
                listsTemplate.DragImages();
                listsTemplate.SetSubTitles(titles);
                listsTemplate.SetBodyTextBoxs(body);
                listsTemplate.WriteTags(tagExValue);
                listsTemplate.ClickOnAscendingBtn();
                listsTemplate.ClickOnDscBtn();
                PreviewPage previewPage = listsTemplate.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                Assert.True(postPage.ValidatePostCreated("VIDEO:test list template"), "Post was not created");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test20Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "38")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("AllBrands")]
            [Category("FullFlow")]
            [Retry(2)]
            public void Editor_Article_FullFlow()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                string body = _params["Body"].ToString();

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.WriteTitle("VIDEO:test article template");
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
        public class Test21Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "39")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")][Category("AllBrands")]
            [Retry(2)]
            public void Editor_Article_ValidatePlayBuzzComponenet()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnPlayBuzzCBX();
                articleBase.SetPlayBuzzURL("http://www.playbuzz.com/meliak10/when-and-where-in-time-should-you-live");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test23Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "44")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")]
            [Category("12Min")]
            [Category("FullFlow")]
            [Retry(2)]
            public void Editor_TV_FullFlow()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                TVPage tVPage = editorPage.ClickOnTVTemplate();
                tVPage.WriteTitle("Test TV Template");
                tVPage.SetEmbedCode(_params["JWembedCode"].ToString());
                tVPage.DragVideo();
                tVPage.ClickOnOkBtn();
                tVPage.SetSeoDesc();
                tVPage.WriteTags(new BsonArray(new List<string>() { "Atest", "BTest", "CTest", "DTest" }));
                PreviewPage previewPage = tVPage.ClickOnPreviewBtn();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                Assert.True(postPage.ValidatePostCreated("Test TV Template"), "The post was not created.");
                var errors = postPage.ValidateComponents(_params["Components"].AsBsonArray);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test24Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "43")]
            [Category("Sanity")][Category("Admin")][Category("Editor")][Category("90Min")]
            [Retry(2)]
            public void Editor_TV_ValidateEmbedCode()
            {
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                TVPage tVPage = editorPage.ClickOnTVTemplate();
                tVPage.SetEmbedCode(_params["JWembedCode"].ToString());
                tVPage.DragVideo();
                tVPage.ClickOnOkBtn();
                Assert.True(tVPage.ValidateVideoAppear(), "The video was not appear after dragNdrop video.");
            }
        }

        [TestFixture]
        [Parallelizable]
         public class Test25Class : BaseUi
         {
            [Test]
            [Property("TestCaseId", "48")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Editor")]
            [Category("AllBrands")]
            [Category("FullFlow")]
            [Retry(2)]
            public void Editor_SlideShow_FullFlow()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                BsonArray titles = _params["Titles"].AsBsonArray;
                string body = _params["Body"].ToString();

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                SlideShowPage slideShow = editorPage.ClickOnSlideShow();
                slideShow.WriteTitle("VIDEO:test slideshow template");
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