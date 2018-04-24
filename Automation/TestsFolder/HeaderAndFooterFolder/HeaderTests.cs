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
        public class Test4Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "147")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("90Min")]
            [Retry(2)]
            public void LogoAndDropDownTest()
            {
                _browser.Navigate(_config.Url);
                Header header = new Header(_browser);
                var headerData = _params["headerData"].AsBsonValue;
                var result = header.SelectAndValidateLogo(headerData);
                Assert.True(result);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Test5Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "131")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void PerspectivesIcon()
            {                      
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Media : BaseUi
        {
            [Test]
            [Property("TestCaseId", "132")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void MediaIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

    }
}