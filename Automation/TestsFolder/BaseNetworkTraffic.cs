using Automation.BrowserFolder;
using NUnit.Framework;
using static Automation.TestsObjects.Result;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class BaseNetworkTraffic : Base
    {
        protected Browser _browser { get; set; }

        [SetUp]
        public void InitTestUi()
        {
            _browser = new Browser(true);
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            _browser.Maximize();
        }

        [TearDown]
        public void CleanTestUi()
        {
            _test.Result.ScreenShot = !_config.Local ? _browser.GetScreenShot(_test) : "";
            _test.Result.Url = _browser.GetUrl();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result);
            _browser.Quit();
            _browser.ProxyApi.Close();
        }
    }
}