using NUnit.Framework;
using Automation.ConfigurationFolder;
using MongoDB.Bson;
using Automation.TestsObjects;
using Automation.MongoDbObject;
using static Automation.TestsObjects.Result;
using Automation.TestsObject;
using System;

namespace Automation.TestsFolder
{
    public class Base
    {
        protected BsonValue _params { get; set; }
        protected Test _test { get; set; }
        public static TestRun _testRun { get; set; }
        public static Configurations _config { get; set; }
        public static MongoDb MongoDb { get; set; }
        static readonly object _syncObject = new object();

        [SetUp]
        public void InitTest()
        {
            _config = new Lazy<Configurations>().Value;
            MongoDb = new Lazy<MongoDb>(() => new MongoDb("TestRuns")).Value;
            _testRun = new Lazy<TestRun>(() => new TestRun(_config)).Value;
            _test = new Test(_config);
            _params = new Params(_test).GetParams();
            _test.UpdateTestStatus(TestContext.CurrentContext.Result, TestStatus.SentToHub);
        }

        [TearDown]
        public void CleanTest()
        {
            _test.UpdateTestStatus(TestContext.CurrentContext.Result);
        }
    }
}