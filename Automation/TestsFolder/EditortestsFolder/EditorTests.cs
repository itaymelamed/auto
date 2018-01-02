﻿using System;
using System.Collections.Generic;
using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.TestsFolder.EditortestsFolder
{
    [TestFixture]
    public class EditorTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : Base
        {
            [Test]
            [Property("TestCaseId", "3")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Retry(2)]
            public void Editor_Article_ValidateInputFields()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Retry(2)]
            public void Editor_Article_ValidateTitleTextfieled()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Retry(2)]
            public void Editor_Article_ValidateBodyTextfieled()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Category("Tags")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxInsertSingleTag()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Category("Tags")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxInsertMultipleTags()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Category("Tags")]
            [Retry(2)]
            public void Editor_Article_ValidateTagsTextBoxSugestionsTags()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("editPage")]
            [Retry(2)]
            public void Editor_Article_DragAndDropAcoverImage()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            [Retry(2)]
            public void Editor_Article_ImageSearch_ValidateMaxSearchResultsAndContent()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PreviewPage")]
            [Category("EditPage")]
            [Retry(2)]
            public void Editor_Article_PreviewButtonCheckEditButton()
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
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PreviewPage")]
            [Category("EditPage")]
            [Retry(2)]
            public void PostPage_Article_PublishButton()
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
        public class Test13Class : Base
        {
            [Test]
            [Property("TestCaseId", "29")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Retry(2)]
            public void Editor_List_ValidateTtitle()
            {
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
        public class Test14Class : Base
        {
            [Test]
            [Property("TestCaseId", "30")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            [Retry(2)]
            public void Editor_List_ValidateTextBoxsBodys()
            {
                string text = _params["text"].ToString();
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
        public class Test15Class : Base
        {
            [Test]
            [Property("TestCaseId", "31")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            [Retry(2)]
            public void Editor_List_ValidateSubTitelsFields()
            {
                BsonArray titles = _params["Titles"].AsBsonArray;
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
        public class Test16Class : Base
        {
            [Test]
            [Property("TestCaseId", "33")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            [Retry(2)]
            public void Editor_List_ValidateDragImages()
            {
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
        public class Test17Class : Base
        {
            [Test]
            [Property("TestCaseId", "35")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            public void Editor_List_ValidateAscendingOrder()
            {
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
        public class Test18Class : Base
        {
            [Test]
            [Property("TestCaseId", "36")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            public void Editor_List_ValidateDescendingOrder()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ListsTemplate listsTemplate = editorPage.ClickOnList();
                List<string> before = listsTemplate.GetItemsIndex();
                listsTemplate.ClickOnDscBtn();
                List<string> after = listsTemplate.GetItemsIndex();

                Assert.True(listsTemplate.ValidateAscDesc(before, after), "The counter is not in descending order.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test19Class : Base
        {
            [Test]
            [Property("TestCaseId", "37")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            public void Editor_List_FullFlow()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                BsonArray titles = _params["Titles"].AsBsonArray;
                string body = _params["Body"].ToString();

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
        public class Test20Class : Base
        {
            [Test]
            [Property("TestCaseId", "38")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            public void Editor_Article_FullFlow()
            {
                BsonArray tagExValue = _params["Tags"].AsBsonArray;
                string body = _params["Body"].ToString();
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
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test21Class : Base
        {
            [Test]
            [Property("TestCaseId", "39")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("EditPage")]
            public void Editor_Article_ValidatePlayBuzzComponenet()
            {
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                EditorPage editorPage = homePage.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnPlayBuzzCBX();
                articleBase.SetPlayBuzzURL("http://www.playbuzz.com/meliak10/when-and-where-in-time-should-you-live");
                Assert.True(articleBase.ValidatePlayBuzzImageAppears(), "layBuzz not appears");
            }
        }
    }
}
