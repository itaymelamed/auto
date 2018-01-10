using static NUnit.Framework.TestContext;

namespace Automation.TestsObjects
{
    public class Result
    {
        public enum TestStatus
        {
            Passed,
            Failed,
            Running,
            SentToHub,
            Inconclusive,
            Skipped,
            Warning,
            None
        }

        public string Status { get; set; }
        public string StackTrace { get; set; }
        public string ErrorMessage { get; set; }
        public string ScreenShot { get; set; }
        public string Url { get; set; }

        public Result(ResultAdapter result, TestStatus status = TestStatus.None)
        {
            Status = status == TestStatus.None ? result.Outcome.Status.ToString() : status.ToString();
            StackTrace = result.StackTrace;
            ErrorMessage = result.Message;
        }
    }
}
