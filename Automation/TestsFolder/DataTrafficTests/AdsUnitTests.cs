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
                var exJsons = _params["ExJsons"].AsBsonArray;
                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(_browser.ProxyApi.GetRequests() ,exJsons);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}