using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Helpersobjects;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.FeedPageTests
{
    public class PartnersPageTests
    {
        [TestFixture]
        [Parallelizable]
        public class ValidatePostsQueueTest : BaseUi
        {
            [Test]
            [Property("TestCaseId", "137")]
            [Category("90Min")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void PartnersPage_ValidatePostsQueue()
            {
                var category = _params["Category"].ToString();
                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                homePage.Login(_config.ConfigObject.Users.AdminUser);
                PostCreator postCreator = new PostCreator(_browser);
                List<string> titles = postCreator.Create(15).Select(t => t.Trim()).ToList();
                //titles.Reverse();
                _browser.Navigate($"{_config.Url}/management/castr");
                CastrPage castrPage = new CastrPage(_browser);
                castrPage.PublishToCategoryMultiple(titles, category);

                _browser.Navigate($"{_config.Url}/hubs/{category}");
                PartnersPage partnersPage = new PartnersPage(_browser);
                var titlesInPage = partnersPage.GetTitles().Where((t, i) => i <=15).ToList();
                titlesInPage.Reverse();
                Assert.True(titles.SequenceEqual(titlesInPage), $"Expected: {string.Join(",", titles)} {Environment.NewLine} Actual: {string.Join(",", titlesInPage)}");
            }
        }
    }
}