using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class Paa
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : Base
        {
            [Test]
            [Property("TestCaseId", "1")] [Category("test")]
            public void PostAnArticle()
            {
                //HomePage homePage = new HomePage(_browser);
                //FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                //HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                //EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                //ArticleBase articleBase = editorPage.ClickOnTemplate(_params["Template"].ToString());
                //articleBase.WriteTitle(_params["Title"].ToString());
                //CropImagePopUp cropImagePopUp = articleBase.DragImage(0);

                //cropImagePopUp.ClickOnCropImageBtn();
                //cropImagePopUp.ClickOnEditokBtn();
                //articleBase.WriteDec(_params["Desc"].ToString());
                //articleBase.AddYoutubeVideo(_params["YouTubeLink"].ToString(), 0);
                //Assert.IsTrue(articleBase.CheckYoutubeVideoInPost(), "Failed to add video to post.");

                //PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                //PostPage postPage = previewPage.ClickOnPublishBtn();

                //Assert.IsTrue(postPage.CheckLogo(), "Logo is missing.");
                //Assert.IsTrue(postPage.CheckPostContent(), "Post component is missing.");
                //Assert.IsTrue(postPage.CheckPostImage(), "Post conver component is missing.");
                //Assert.IsTrue(postPage.CheckPostSideContent(), "Post Side-content component is missing.");
                //Assert.IsTrue(postPage.CheckPostTitle(), "Post title component is missing.");
            }
        }
    }
}
