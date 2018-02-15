using System;
using Automation.Helpersobjects;
using Automation.PagesObjects;
using Automation.PagesObjects.EchoFolder;
using NUnit.Framework;

namespace Automation.TestsFolder.AdminTestsFolder
{
    public class EchoTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("Echo")]
            [Category("AllBrands")]
            [Retry(1)]
           // [Repeat(50)]
            public void Echo_ValidatePostCreation()
            {
                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
                string title = postCreatorEcho.CreatePost();
                _browser.SwitchToFirstTab();
                _browser.Refresh();
                EchoPage echoPage = new EchoPage(_browser);
                Assert.True(echoPage.ValidatePostCreation(title),$"post {title} is not shown on echo page.");
            }
        }

//        [TestFixture]
//        [Parallelizable]
//        public class Test2Class : BaseUi
//        {
//            [Test]
//            [Property("TestCaseId", "2")]
//            [Category("Sanity")]
//            [Category("Admin")]
//            [Category("Echo")]
//            [Category("AllBrands")]
//            [Retry(1)]
//            public void Echo_ValidateAuthorName()
//            {
//                Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
//                NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
//                PostCreatorEcho postCreatorEcho = new PostCreatorEcho(_browser);
//                string title = postCreatorEcho.CreatePost();
//                 postPage = new PostPage(_browser);
//                postPage
//                _browser.SwitchToFirstTab();
//                _browser.Refresh();
//                EchoPage echoPage = new EchoPage(_browser);
//                echoPage.ValidateAuthor();
//            }
//        }
  }

}