using Automation.PagesObjects;
using NUnit.Framework;
using Automation.PagesObjects.EchoFolder;
using MongoDB.Bson;

namespace Automation.TestsFolder
{
    public class TagManagementTests
    {
        [TestFixture]
        [Parallelizable]
        public class EITest1 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "145")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void TagManagment_Login()
            {
                _browser.Navigate($"http://{_config.Env}.{_config.ConfigObject.Echo}");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                string url = _browser.GetUrl();
                var expectedUrl = "http://" + _config.Env + "." + _config.ConfigObject.Echo;
                Assert.True(url == expectedUrl, $"User was not login to the Tags management page. Expected:{expectedUrl} but actual:{url}.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest2 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "146")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void Tags_ValidateLangDropDownList()
            {
                _browser.Navigate($"http://{_config.Env}.{_config.ConfigObject.Echo}");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                Assert.True(tagManagementPage.ValidateDropDown(), "");
            }
        }

        //[TestFixture]
        //[Parallelizable]
        //public class EITest3 : BaseUi
        //{
        //    [Test]
        //    [Property("TestCaseId", "147")]
        //    [Category("platform")]
        //    [Retry(1)]
        //    public void Tags_ValidateLangDropDownListOpen()
        //    {
        //        _browser.Navigate("http://" + _config.Env + "." + _config.ConfigObject.Echo);
        //        Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
        //        NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
        //        TagManagementPage tagManagementPage = new TagManagementPage(_browser);
        //        Assert.True(tagManagementPage.ValidateDropDown());
        //    }
        //}

        [TestFixture]
        [Parallelizable]
        public class EITest4 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "166")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_GetLangList()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                BsonArray languagesList = _params["langnuges"].AsBsonArray;
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnLangBtn();
                Assert.True(tagManagementPage.ValidateLangList(languagesList));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest5 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "167")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_ValidateDefualtLang()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                string curLanguage = _params["langnuge"].ToString();
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                Assert.True(tagManagementPage.ValidateCurrntLang(curLanguage));
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest6 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "168")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_CreateNewTag_ValidateSucMsg()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                string popUpMessageText = _params["popupText"].ToString();
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);

                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnCreateBtn();
                string tagName = tagManagementPage.FillTheTagName();

                popUpMessageText = popUpMessageText.Replace("<tag>", tagName);
                tagManagementPage.InsertSynonyms();
                tagManagementPage.ClickOnDoneBtn();

                var actualResult = tagManagementPage.GetPopUpText().Replace("DISMISS", "").Replace("\n", "");
                var expectedResult = popUpMessageText;
                Assert.AreEqual(expectedResult, actualResult, $"Expected text was: {expectedResult} Actual: {actualResult}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest7 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "170")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_CreateNewTag()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);

                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnCreateBtn();
                string tagName = tagManagementPage.FillTheTagName();

                tagManagementPage.InsertSynonyms();
                tagManagementPage.ClickOnDoneBtn();
                tagManagementPage.SearchForTagName(tagName);

                var actualresult = tagManagementPage.GetTagText();
                var expctedResult = tagName;

                Assert.AreEqual(expctedResult, actualresult, $"Expected tag name was: {expctedResult} Actual: {actualresult}.");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest8 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "171")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_SearchforNonExistingTag()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                string NonExTag = _params["tag"].ToString();
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.SearchForTagName(NonExTag);
                string actualMsg = tagManagementPage.GetNoTagsFoundMsg();
                string expectedMsg = "No tags were found matching the search term.  ADD AS NEW TAG";
                Assert.AreEqual(expectedMsg, actualMsg, $"Expected tag name was: {expectedMsg} Actual: {actualMsg}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest9 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "172")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_EditTag()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnCreateBtn();
                string tagName = tagManagementPage.FillTheTagName();
                tagManagementPage.InsertSynonyms();
                tagManagementPage.ClickOnDoneBtn();
                tagManagementPage.SearchForTagName(tagName);
                tagManagementPage.ClickOnTag(tagName);
                tagManagementPage.ClickOnTagNameField();
                tagManagementPage.EditTag();
                string actualTagName = tagManagementPage.GetTagText();
                string expectedTagName = tagName + 3;
                Assert.AreEqual(expectedTagName, actualTagName, $"Expected tag name was: {expectedTagName} Actual: {actualTagName}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest10 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "173")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_RemoveTag()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnCreateBtn();
                string tagName = tagManagementPage.FillTheTagName();
                tagManagementPage.InsertSynonyms();
                tagManagementPage.ClickOnDoneBtn();
                tagManagementPage.SearchForTagName(tagName);
                tagManagementPage.ClickOnSelectedTag();
                tagManagementPage.ClickOnRemoveTagBtn();
                tagManagementPage.ClickOnYesBtn();
                Assert.True(tagManagementPage.VliadateTagRemoved(), "The Tag was not removed");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest11 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "174")]
            [Category("TagManagement")]
            [Category("Floor8")]
            [Retry(1)]
            public void platform_Tags_RemoveSynonym()
            {
                _browser.Navigate(_config.ConfigObject.Echo);
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.LoginNewsRoom(_config.ConfigObject.Users.AdminUser);
                TagManagementPage tagManagementPage = new TagManagementPage(_browser);
                tagManagementPage.ClickOnTags();
                tagManagementPage.ClickOnCreateBtn();
                string tagName = tagManagementPage.FillTheTagName();
                tagManagementPage.InsertSynonyms();
                tagManagementPage.ClickOnDoneBtn();
                tagManagementPage.SearchForTagName(tagName);
                tagManagementPage.ClickOnSelectedTag();
                tagManagementPage.ClickOnRemoveSyn();
                tagManagementPage.ClickOnDoneBtn();
                tagManagementPage.ClickOnSelectedTag();
                Assert.True(tagManagementPage.ValidateSynRemoved(), "The syn was not removed");
            }
        }
    }
}