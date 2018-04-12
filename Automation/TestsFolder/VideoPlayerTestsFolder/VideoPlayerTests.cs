using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.VideoPlayerTestsFolder
{
    public class VideoPlayerTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "1")]
            [Category("Sanity")]
            [Category("Video")]
            [Category("AllBrands")]
            [Retry(1)]
            public void Test()
            { 
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                PostPage postPage = homePage.ClickOnCoverStory();

                Assert.True(postPage.VideoPlayer.DataLayer.WaitForEvent("jw viddeo first play"), "Video was not played.");
            }
        }
    }
}