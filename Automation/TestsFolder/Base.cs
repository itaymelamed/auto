﻿using NUnit.Framework;
using Automation.BrowserFolder;
using Automation.ConfigurationFolder;
using MongoDB.Bson;
using Automation.TestsObjects;
using Automation.MongoDbObject;
using static Automation.TestsObjects.Result;
using Automation.TestsObject;
using Automation.ApiFolder;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class Base
    {
        HubLoadBalancer _hubLoadBalancer;
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
            _hubLoadBalancer = !_config.Local? new HubLoadBalancer(_config) : null;
            _browser = !_config.Local ? new Browser(_hubLoadBalancer) : new Browser();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.Running);
            _browser.Maximize();
            _browser.Navigate(_config.Url);
        }

        [TearDown]
        public void CleanTest()
        {
            _test.Result.ScreenShot = !_config.Local ?_browser.GetScreenShot(_test) : "";
            _test.Result.Url =  _browser.GetUrl();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result);
            _browser.Quit();
        }
    }
}