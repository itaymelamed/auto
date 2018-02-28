using System;
using System.Threading;
using Automation.ApiFolder;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.DataTrafficTests
{
    [TestFixture]
    public class GoogleAnaliticsTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "63")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoPlayerEmbed()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video player embed";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, false);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "64")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_FirstPlay()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video first play";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                Thread.Sleep(2000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, false);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "65")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_Complete()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video complete";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.Seek();
                videoPage.WaitForVideoComplete();
                Thread.Sleep(2000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "66")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_Mute()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video mute";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.Mute();

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "67")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_UnMute()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video unmute";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.Mute();
                Thread.Sleep(1000);
                videoPage.Mute();
                Thread.Sleep(2000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "68")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_FullScreenOn()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video fullscreen on";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.FullScreen();
                Thread.Sleep(1000);
                videoPage.FullScreen();
                Thread.Sleep(3000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "69")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_FullScreenOff()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video fullscreen off";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.FullScreen();
                videoPage.FullScreen();
                Thread.Sleep(2000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "70")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_PositionSeeked()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video position seeked";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.Seek();
                Thread.Sleep(1000);

                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test9Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "71")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_AdComplete()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad complete";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                Thread.Sleep(TimeSpan.FromSeconds(180));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test10Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "72")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_AdImpression()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad impression";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "73")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_AdViewableImpression()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad viewable impression";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test12Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "74")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoTwentyFivecompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video 25% complete";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.WaitUntillVideoPrecnent(25);
                Thread.Sleep(1000);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test13Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "75")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoFiftyCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video 50% complete";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.WaitUntillVideoPrecnent(50);
                Thread.Sleep(1000);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test14Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "76")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoSeventyFiveCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video 75% complete";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.ClickOnPlay();
                videoPage.WaitUntillVideoPrecnent(75);
                Thread.Sleep(TimeSpan.FromMinutes(1));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "77")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoNintyCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video 90% complete";

                _browser.ProxyApi.NewHar();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.WaitForVideoToPlay();
                videoPage.WaitUntillVideoPrecnent(90);
                Thread.Sleep(1000);
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test16Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "78")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoAdTwentyFiveCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad 25% complete";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.WaitForVideoToPlay();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test17Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "79")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoFiftycompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad 50% complete";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.WaitForVideoToPlay();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test18Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "80")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoAdSeventyFiveCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad 75% complete";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.WaitForVideoToPlay();
                Thread.Sleep(TimeSpan.FromSeconds(180));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test19Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "81")]
            [Category("PostPage")]
            [Category("GoogleAnalitics")]
            [Category("90Min")]
            [Retry(2)]
            public void GoogleAnalitics_Video_JwVideoAdNintyCompleted()
            {
                var url = _params["PostUrl"].ToString();
                var exJson = _params["ExJson"];
                var ignor = _params["Ignor"].AsBsonArray;
                var eventAction = "jw video ad 90% complete";

                _browser.ProxyApi.NewHarPost();
                _browser.Navigate(url);
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests);
                VideoPage videoPage = new VideoPage(_browser);
                videoPage.WaitForVideoToPlay();
                Thread.Sleep(TimeSpan.FromSeconds(120));
                string errors = googleAnalitics.ValidateEventRequest(eventAction, exJson, ignor, true);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}