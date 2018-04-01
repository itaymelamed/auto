using Automation.BrowserFolder;
using NUnit.Framework;
using static Automation.ConfigurationFolder.Configurations;
using static Automation.TestsObjects.Result;

namespace Automation.TestsFolder
{
    public class BaseNetworkTraffic : Base
    {
        protected Browser _browser { get; set; }

        [SetUp]
        public void InitTestUi()
        {
            _browser = new Browser(true);
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            if (_config.BrowserT == BrowserType.Desktop)
                _browser.Maximize();
            else
                _browser.SetBrowserSize(375, 812);
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