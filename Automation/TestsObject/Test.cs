using System;
using System.Collections.Generic;
using System.Linq;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using static Automation.TestsObjects.Result;

namespace Automation.TestsObjects
{
    public class Test
    {
        public string TestNumber { get; }

        public string TestName { get; }

        public List<string> Steps { get; set; }

        public Result Result { get; set; }

        public string TestRunId { get; }

        public string Date { get; }

        public string EnvironmentType { get; }

        public string SessionId { get; set; }

        public Test(Configurations config)
        {
            Result = new Result(TestContext.CurrentContext.Result, TestStatus.SentToHub);
            TestNumber = TestContext.CurrentContext.Test.Properties.Get("TestCaseId").ToString();
            TestRunId = Base._testRun.TestRunId;
            TestName = TestNumber + " | " + string.Concat(TestContext.CurrentContext.Test.Name.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ').
                             Replace("_", " | ");
            Steps = new List<string>();
            EnvironmentType = config.Env.ToString();
            Date = DateTime.Now.ToString("dd/MM/yyyy H:mm");
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Set("Test", this);
            SessionId = string.Empty;

            Base.MongoDb.InsertTest(this);
        }

        public void UpdateTestStatus(TestContext.ResultAdapter result, TestStatus status = TestStatus.None)
        {
            if (status == TestStatus.None)
            {
                Result.Status = result.Outcome.Status.ToString();
                Result.StackTrace = result.StackTrace;
                Result.ErrorMessage = result.Message;
            }
            else
                Result.Status = status.ToString();
            
            Base.MongoDb.UpdateResult(this);
            Base._testRun.UpdataResults();
        }

        public void UpdateTestStep(string step)
        {
            Steps.Add(step);

            Base.MongoDb.UpdateSteps(this);
        }

        public void UpdateSessionId(string sessionId)
        {
            SessionId = sessionId;
            Base.MongoDb.InsertTest(this);
        }
    }
}