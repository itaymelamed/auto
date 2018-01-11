using NUnit.Framework;
using Automation.BrowserFolder;
using static Automation.TestsObjects.Result;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class BaseUi : Base
    {
        HubLoadBalancer _hubLoadBalancer;
        protected Browser _browser { get; set; }   

        [SetUp]
        public void InitTestUi()
        {
            _hubLoadBalancer = !_config.Local? new HubLoadBalancer(_config) : null;
            _browser = !_config.Local ? new Browser(_hubLoadBalancer) : new Browser();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            _browser.Maximize();
            _browser.Navigate(_config.Url);
        }

        [TearDown]
        public void CleanTestUi()
        {
            _test.Result.ScreenShot = !_config.Local ?_browser.GetScreenShot(_test) : "";
            _test.Result.Url =  _browser.GetUrl();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result);
            _browser.Quit();
        }
    }
}