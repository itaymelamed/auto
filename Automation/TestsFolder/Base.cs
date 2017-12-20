using NUnit.Framework;
using Automation.BrowserFolder;
using Automation.ConfigurationFolder;
using MongoDB.Bson;
using Automation.TestsObjects;
using Automation.MongoDbObject;
using static Automation.TestsObjects.Result;
using Automation.TestsObject;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class Base
    {
        protected Browser _browser { get; set; }
        protected BsonValue _params { get; set; }    
        protected Test _test { get; set; }
        public static TestRun _testRun { get; set; }
        public static Configurations _config { get; set; }
        public static MongoDb MongoDb { get; set; }
        static readonly object _syncObject = new object();

        [SetUp]
        public void InitTest()
        {
            lock (_syncObject)
            {
                _config = _config ?? new Configurations();
                MongoDb = MongoDb ?? new MongoDb("TestRuns");
                _testRun = _testRun ?? new TestRun(_config);
            }

            _test = new Test(_config);
            _params = new Params(_test, _config).GetParams();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.SentToHub);
            _browser = new Browser(_config);
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            _browser.Maximize();
            _browser.Navigate(_config.Url);
        }

        [TearDown]
        public void CleanTest()
        {
            _test.Result.ScreenShot = _browser.GetScreenShot(_test);
            _test.Result.Url =  _browser.GetUrl();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result);
            _browser.Quit();
        }
    }
}