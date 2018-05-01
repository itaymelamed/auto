using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Linq;

namespace Automation.TestsFolder.HeaderAndFooterFolder
{
    [TestFixture]
    public class HeaderTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test1Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "101")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("12up")]
            [Category("90Min")]
            [Category("90MinIn")]
            [Category("Ftb90")]
            [Category("Floor8")]
            [Retry(2)]
            public void HeaderAndFooter_ValidateLangauageDropdownExistOnPage()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                bool result = header.ValidateLangagueDropDownAppears();
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "139")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("DBLTAP")]
            [Category("Pluralist")]
            [Category("90MinDE")]

            [Retry(2)]
            public void HeaderAndFooter_ValidateLangauageDropdownDontExistOnPage()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                bool result = header.ValidateLangagueDropDownDoesntAppear();
                Assert.False(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test3Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "147")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestEn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "148")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestEs()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "149")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestEsLatam()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result,"");
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test6Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "150")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestFr()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test7Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "151")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestIt()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test8Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "152")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestTr()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test9Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "153")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestId()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test10Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "154")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestTh()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "155")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestVn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test12Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "156")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTestPtBr()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test13Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "157")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90MinIn")]
            [Retry(2)]
            public void LogoAndDropDownTestEn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test14Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "158")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("12Up")]
            [Retry(2)]
            public void LogoAndDropDownTestEn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test15Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "159")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("12Up")]
            [Retry(2)]
            public void LogoAndDropDownTestEs()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test16Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "160")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Floor8")]
            [Retry(2)]
            public void LogoAndDropDownTestEn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test17Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "161")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Floor8")]
            [Retry(2)]
            public void LogoAndDropDownTestEs()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test18Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "162")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Ftb90")]
            [Retry(2)]
            public void LogoAndDropDownTestEn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test19Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "163")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Ftb90")]
            [Retry(2)]
            public void LogoAndDropDownTestId()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test20Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "164")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Ftb90")]
            [Retry(2)]
            public void LogoAndDropDownTestTh()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test21Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "165")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Ftb90")]
            [Retry(2)]
            public void LogoAndDropDownTestVn()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
      
        [TestFixture]
        [Parallelizable]
        public class Test22Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "169")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90MinIn")]
            [Retry(2)]
            public void LogoAndDropDownTestHi()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.LogoAndDropDown(headerData);
                Assert.True(result);
            }
        }
    }
}