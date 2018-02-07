using System.Threading;
using Automation.ApiFolder;
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
            [Property("TestCaseId", "55")]
            [Category("Sanity")]
            [Category("PostPage")]
            [Category("Production")]
            [Category("Ads")]
            [Category("AllBrands")]
            [Retry(2)]
            public void GoogleAnalitics_Video_Pause()
            {
                _browser.ProxyApi.NewHar();
                _browser.Navigate("http://www.90min.com/posts/5968945-birthday-banter-wishing-neymar-ronaldo-a-very-feliz-aniversario");
                GoogleAnalitics googleAnalitics = new GoogleAnalitics(_browser.ProxyApi.GetRequests()); 

                var requests = _browser.ProxyApi.GetRequests();
 
            }
        }
    }
}