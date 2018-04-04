using NUnit.Framework;
using Automation.BrowserFolder;
using static Automation.TestsObjects.Result;

namespace Automation.TestsFolder
{
    public class BaseUi : Base
    {
        protected Browser _browser { get; set; }   

        [SetUp]
        public void InitTestUi()
        {
            _browser = new Browser();
            _test.UpdateSessionId(_browser.SessionId);
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            _browser.Maximize();
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