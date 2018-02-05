using Automation.ApiFolder;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class AdsUnitTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "55")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_Article()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);

                PostPage postPage = new PostPage(_browser);
                errors = postPage.ValidateAds(displyed);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "59")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_List()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);

                PostPage postPage = new PostPage(_browser);
                errors = postPage.ValidateAds(displyed);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "60")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_SlideShow()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);

                PostPage postPage = new PostPage(_browser);
                errors = postPage.ValidateAds(displyed);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "61")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_LineUp()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);

                PostPage postPage = new PostPage(_browser);
                errors = postPage.ValidateAds(displyed);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "62")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_TimeOut()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);

                PostPage postPage = new PostPage(_browser);
                errors = postPage.ValidateAds(displyed);
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}