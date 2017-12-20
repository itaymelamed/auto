using System;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;
using static Automation.TestsObjects.Result;

namespace Automation.TestsObjects
{
    public class TestRun
    {
        public string TestRunId { get; }
        public string Env { get; }
        public string Date { get; }
        public Results Results { get; set; }
        public string Duration { get; set; }
        public String SiteName { get; }

        public TestRun(Configurations config)
        {
            Results = new Results();
            TestRunId = (Base.MongoDb.GetAllDocuments("Runs").Count + 1).ToString();
            Env = config.Env.ToString();
            Date = DateTime.Now.AddHours(2).ToString("MM/dd/yy HH:mm:ss");
            SiteName = config.SiteName;

            Base.MongoDb.InserTestRun(this);
        }

        public void UpdataResults()
        {
            var docs = Base.MongoDb.GetAllDocuments($"testRun{TestRunId}");

            Results.Passed = docs.FindAll(x => x["Result"]["Status"] == TestStatus.Passed.ToString()).Count;
            Results.Failed = docs.FindAll(x => x["Result"]["Status"] == TestStatus.Failed.ToString()).Count;
            Results.SentToHub = docs.FindAll(x => x["Result"]["Status"] == TestStatus.SentToHub.ToString()).Count;
            Results.Running = docs.FindAll(x => x["Result"]["Status"] == TestStatus.Running.ToString()).Count;

            Base.MongoDb.UpdateTestRunResults(this);
            Base.MongoDb.UpdateDuration(this);
        }
    }
}
