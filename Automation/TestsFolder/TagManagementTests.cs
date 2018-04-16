using System;
using NUnit.Framework;

namespace Automation.TestsFolder
{
    public class TagManagementTests
    {
        [TestFixture]
        [Parallelizable]
        public class EITest1 : BaseUi
        {
            [Test]
            [Property("TestCaseId", "145")]
            [Category("EI")]
            [Retry(1)]
            public void MMsport_Login()
            {
                _browser.Navigate("http://"+_config.Env+_config.ConfigObject.Echo);
            }
        }
    }
}
