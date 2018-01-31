using Automation.ApiFolder;
using NUnit.Framework;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class AdsUnitTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "55")]
            [Category("Sanity")]
            [Category("PostPage")]
            //[Category("AllBrands")]
            [Category("Ads")]
            [Retry(2)]
            public void AdsUnit_PostPage_Article_HeaderAd()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var adNames = _params["AdNames"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, adNames);
                string errors = adsUnithelper.ValidateJsons();

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}