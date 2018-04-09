using System.Threading;
using Automation.ApiFolder;
using Automation.Helpersobjects;
using Automation.PagesObjects.ExternalPagesobjects;
using NUnit.Framework;

namespace Automation.TestsFolder.DataTrafficTests
{
    public class AdsUnitTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseNetworkTraffic
        {
            static int retries;

            [Test]
            [Property("TestCaseId", "55")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(4)]
            public void AdsUnit_PostPage_Article()
            {
                retries++;
                string email = TestContext.Parameters.Get("Email", "itay.m@minutemedia.com");
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params[_config.BrowserT.ToString()]["ExJson"];
                var displyed = _params[_config.BrowserT.ToString()]["Displyed"].AsBsonArray;
                var notDisplyed = _params[_config.BrowserT.ToString()]["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                string experimentId = _browser.GetCookie("_ga").Value;
                experimentId = experimentId.Substring(experimentId.Length - 2);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed, experimentId);
                string errors = adsUnithelper.ValidateJsons();

                if (!string.IsNullOrEmpty(errors) && retries == 4)
                {
                    AdsUnitMailer adsUnitMailer = new AdsUnitMailer(email, "Article Template");
                    adsUnitMailer.SendEmail(errors);
                }

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseNetworkTraffic
        {
            static int retries;

            [Test]
            [Property("TestCaseId", "59")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(4)]
            public void AdsUnit_PostPage_List()
            {
                retries++;
                string email = TestContext.Parameters.Get("Email", "itay.m@minutemedia.com");
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params[_config.BrowserT.ToString()]["ExJson"];
                var displyed = _params[_config.BrowserT.ToString()]["Displyed"].AsBsonArray;
                var notDisplyed = _params[_config.BrowserT.ToString()]["NotDisplyed"].AsBsonArray;
                var ignor = _params["Ignor"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                string experimentId = _browser.GetCookie("_ga").Value;
                experimentId = experimentId.Substring(experimentId.Length - 2);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed, experimentId);
                string errors = adsUnithelper.ValidateJsons(ignor);

                if (!string.IsNullOrEmpty(errors) && retries == 4)
                {
                    AdsUnitMailer adsUnitMailer = new AdsUnitMailer(email, "List Template");
                    adsUnitMailer.SendEmail(errors);
                }

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseNetworkTraffic
        {
            static int retries;

            [Test]
            [Property("TestCaseId", "60")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("Ftb90")]
            [Category("90Min")]
            [Category("90MinIn")]
            [Category("90MinDe")]
            [Category("12Up")]
            [Category("DBLTAP")]
            [Retry(4)]
            public void AdsUnit_PostPage_SlideShow()
            {
                retries++;
                string email = TestContext.Parameters.Get("Email", "itay.m@minutemedia.com");
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params[_config.BrowserT.ToString()]["ExJson"];
                var displyed = _params[_config.BrowserT.ToString()]["Displyed"].AsBsonArray;
                var notDisplyed = _params[_config.BrowserT.ToString()]["NotDisplyed"].AsBsonArray;
                var ignor = _params["Ignor"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                string experimentId = _browser.GetCookie("_ga").Value;
                experimentId = experimentId.Substring(experimentId.Length - 2);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed, experimentId);
                string errors = adsUnithelper.ValidateJsons(ignor);

                if (!string.IsNullOrEmpty(errors) && retries == 4)
                {
                    AdsUnitMailer adsUnitMailer = new AdsUnitMailer(email, "Slide Show Template");
                    adsUnitMailer.SendEmail(errors);
                }

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseNetworkTraffic
        {
            static int retries;

            [Test]
            [Property("TestCaseId", "61")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("Ftb90")]
            [Category("90Min")]
            [Category("90MinIn")]
            [Category("90MinDe")]
            [Category("12Up")]            
            [Retry(4)]
            public void AdsUnit_PostPage_LineUp()
            {
                retries++;
                string email = TestContext.Parameters.Get("Email", "itay.m@minutemedia.com");
                var postUrl = _params["PostUrl"].AsString;
                var exJsons = _params[_config.BrowserT.ToString()]["ExJson"];
                var displyed = _params[_config.BrowserT.ToString()]["Displyed"].AsBsonArray;
                var notDisplyed = _params[_config.BrowserT.ToString()]["NotDisplyed"].AsBsonArray;

                _browser.ProxyApi.NewHar();
                _browser.Navigate(postUrl);
                Thread.Sleep(1000);
                string experimentId = _browser.GetCookie("_ga").Value;
                experimentId = experimentId.Substring(experimentId.Length - 2);
                //PostPage postPage = new PostPage(_browser);
                //string errors = postPage.ValidateAds(displyed);
                //Assert.True(string.IsNullOrEmpty(errors), errors);

                var requests = _browser.ProxyApi.GetRequests();
                AdsUnitHelper adsUnithelper = new AdsUnitHelper(requests, exJsons, displyed, notDisplyed, experimentId);
                string errors = adsUnithelper.ValidateJsons();

                if (!string.IsNullOrEmpty(errors) && retries == 4)
                {
                    AdsUnitMailer adsUnitMailer = new AdsUnitMailer(email, "LineUp Template");
                    adsUnitMailer.SendEmail(errors);
                }

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
                string email = TestContext.Parameters.Get("Email", "itay.m@minutemedia.com");
                var ignor = _params["Ignor"].AsBsonArray;

                AdsTxtValidator adsTxtValidator = new AdsTxtValidator($"{_config.Url}/ads.txt");
                _browser.Navigate($"https://adstxt.adnxs.com/?url={_config.Url.ToLower()}/ads.txt");
                AdsTxtValidatorPage adsTxtValidatorPage = new AdsTxtValidatorPage(_browser);
                var errors = adsTxtValidatorPage.GetErrors(ignor);
                errors += adsTxtValidator.Validate();

                if (!string.IsNullOrEmpty(errors))
                {
                    AdsTxtMailer adsTxtMailer = new AdsTxtMailer(email);
                    adsTxtMailer.SendEmail(errors);
                }

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}