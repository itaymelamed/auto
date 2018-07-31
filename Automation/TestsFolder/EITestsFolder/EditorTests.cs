using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.TestsFolder.EITestsFolder
{
    public class EditorTests
    {
        [TestFixture]
        [Parallelizable]
        public class EITest1 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "143")]
            [Category("EI")]
            [Retry(1)]
            public void EI_Login()
            {
                _browser.Navigate(_config.Url + "/management");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                ManagementPage managementPage = loginPage.LoginEI(_config.ConfigObject.Users.AdminUser);
                string url = _browser.GetUrl();
                Assert.True(url == _config.Url + "/management", $"User was not login to the management page. Expected:{_config.Url}/management. but actual:{url}");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class EITest2 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "144")]
            [Category("EI")]
            [Retry(1)]
            public void EI_Article_FullFlow()
            {
                _browser.Navigate(_config.Url + "/management");
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                ManagementPage managementPage = loginPage.LoginEI(_config.ConfigObject.Users.AdminUser);
                managementPage.ClickOnEditorButton();
                EditorPage editorPage = new EditorPage(_browser);
                editorPage.ClickOnArticle();
                ArticleBase articleBase = new ArticleBase(_browser);
                articleBase.FillArticleTemplate();
                PostPage postPage = new PostPage(_browser);
                postPage.ValidateComponents(new BsonArray{".logo-img",".post-cover__media img",".post-article__post-title__title",".post-content",".post-side-content","transfer-news--container",".post-side-content__external-widget.external-widget.external-widget--taboola",".post-metadata",".reactions__list",".share-component--post-cover",".share-component--post-bottom",".trc_rbox.thumbnails-b.trc-content-sponsored",".post-after .trc-content-sponsored",".user-menu__link",".edition-component.has-dropdown",".new-article",".main-sidenav-toggle","#site-header"});

            }
        }
    }
}