using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Linq;

namespace Automation.TestsFolder.PostPagesFolder
{
    [TestFixture]
    public class HeaderTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test2Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("AllBrands")]
            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                var exCurLanguage = "EN";
                PostPage postPage = new PostPage(_browser);
                bool result = postPage.ValidateCurrentLangaugeDropDown(exCurLanguage);
                Assert.True(result);
            }
        }
        [TestFixture]
        [Parallelizable]
        public class Test101Class : BaseUi
        {
            [Test]
            [Property("TestCaseId", "101")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("12up")]
            [Category("90Min")]
            [Category("90MinIn")]
            [Category("Ftb90")]
            [Category("Floor8")]


            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
            {
                //click on lang
                // new header page
                // curl lang = en
                // curl =/ list
                // curl langs

                var languages = _params["languageDropDownNames"].AsBsonArray;
                PostPage postPage = new PostPage(_browser);
                bool result = postPage.ValidateLanguageDropDownLangauge(languages);
                Assert.True(result);
            }
        }
    }
}