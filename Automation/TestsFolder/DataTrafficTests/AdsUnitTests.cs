using System.Threading;
using Automation.ApiFolder;
using Automation.Helpersobjects;
using Automation.PagesObjects.ExternalPagesobjects;
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
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(2)]
            public void AdsUnit_PostPage_Article()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "59")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(2)]
            public void AdsUnit_PostPage_List()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "60")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(2)]
            public void AdsUnit_PostPage_SlideShow()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "61")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(2)]
            public void AdsUnit_PostPage_LineUp()
            {
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params["ExJson"];
                var displyed = _params["Displyed"].AsBsonArray;
                var notDisplyed = _params["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed);
                string errors = adsUnithelper.ValidateJsons();
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "82")]
            [Category("Production")]
            [Category("AdsTxt")]
            [Category("AllBrands")]
            public void Ads_ValidateAdsTxtFile()
            {
                const string Email = "automationads@minutemedia.com";
                var ignor = _params["Ignor"].AsBsonArray;
                AdsTxtValidator adsTxtValidator = new AdsTxtValidator($"{_config.Url}/ads.txt");
                _browser.Navigate($"https://adstxt.adnxs.com/?url={_config.Url.ToLower()}/ads.txt");
                AdsTxtValidatorPage adsTxtValidatorPage = new AdsTxtValidatorPage(_browser);
                var errors = adsTxtValidatorPage.GetErrors(ignor);
                errors += adsTxtValidator.Validate();

                ApiObject apiObject = new ApiObject();
                if(!string.IsNullOrEmpty(errors))
                    apiObject.GetRequestVoid($"http://{_config.Host}:32002/users/send?msg=<div><b><h2 style='color: red;'>{_config.SiteName} - The%20following%20errors%20were%20found%20in%20the%20ads.txt%20file:</h2></b></div>{errors}</br></br><img src='{_test.Result.ScreenShot}'></img>&to={Email}&subject={_config.SiteName} | Automation%20Google%20Ads.txt%20file%20-Tests%20failed");
                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}