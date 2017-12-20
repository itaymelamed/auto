using System.Collections.Generic;
using NUnit.Framework;

namespace LoggerProj
{
    public class Test
    {
        public string TestName { get; }
        public List<string> Steps { get; set; }
        public bool Status { get; set; }

        public Test()
        {
            Steps = new List<string>() {"Test has started."};
            TestName = TestContext.CurrentContext.Test.Name;
            Status = false;
        }
    }
}
