
using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Linq;

namespace Automation.TestsFolder.PostPagesFolder
{
    [TestFixture]
    public class PostPageTests
    {
        [TestFixture]
        [Parallelizable]
        public class Test11Class : BaseNetworkTraffic
        {
            [Test]
            [Property("TestCaseId", "13")]
            [Category("Sanity")]
            [Category("Admin")]
            [Category("PostPage")]
            [Category("AllBrands")]
            [Retry(2)]
            public void PostPage_ValidateUiComponentsExistOnPage()
        [FindsBy(How = How.CssSelector, Using = ".post-admin-options__label")]
        IWebElement options { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href*='/castr']")]
        IWebElement openInCaster { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-article__post-title__title")]
        IWebElement title { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[id*='google_pubconsole'] iframe")]
        IList<IWebElement> iframes { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-metadata__author-name")]
        IWebElement authorName { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".next-post-button__texts")]
        IWebElement nextBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".transfer-news--item")]
        IList<IWebElement> transferNews { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".top-posts-side-bar__item__text")]
        IList<IWebElement> topArticles { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-side .trc_spotlight_item")]
        IList<IWebElement> taboolaRight { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-after .trc_spotlight_item")]
        IList<IWebElement> taboolaBtm { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".logo-img")]
        IWebElement logo { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[data-spotim-module='spotim-launcher']")]
        IWebElement spotim { get; set; }

        [FindsBy(How = How.ClassName, Using = "post-content")]
        IWebElement postContent { get; set; }

        [FindsBy(How = How.ClassName, Using = "reactions__list-item")]
        IList<IWebElement> reactions { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".cover-social-container [data-type='facebook']")]
        IWebElement faceBookTop { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".cover-social-container [data-type='twitter']")]
        IWebElement twitterTop { get; set; }

        public VideoPlayer VideoPlayer { get; }

        public PostPage(Browser browser)
            :base(browser)
        {
            VideoPlayer = new VideoPlayer(_browser);
        }

        public string ValidateComponents(BsonArray components)
        {
            var errors = string.Empty;
            List<string> componentsstrings = components.ToList().Select(x => x.ToString()).ToList();
            componentsstrings.ForEach(c =>
            {
                IWebElement el = null;
                Base.MongoDb.UpdateSteps($"Validate Component {c}.");
                if (_browserHelper.ExecutUntillTrue(() => el = _driver.FindElement(By.CssSelector(c)), "", 0, false) == null)
                    errors += $"Component {c} does not exsist {Environment.NewLine}";
            });

            return errors;
        }

        public string ValidateTagsOnSourcePage(BsonArray tags)
        {
            Base.MongoDb.UpdateSteps("Validating Tags On Source Page.");
            string errors = string.Empty;
            List<string> tagsList = tags.AsBsonArray.ToList().Select(t => t.ToString()).ToList();
            _browser.Navigate(_browser.GetUrl()+"?test=test");
            tagsList.ForEach(t => {
                errors += !_browser.GetSource().Contains(t) ? $"Tag {t} does not exsist on page source. {Environment.NewLine}" : "";
            });

            return errors;
        }

        public bool ValidatePostCreated(string postTitle)
        {
            Base.MongoDb.UpdateSteps("Validating Post creation.");
            _browserHelper.WaitUntillTrue(() => _browser.GetUrl().Contains("posts"), "User has not redirected to posts page.");
            //_browserHelper.WaitUntillTrue(() => _browser.GetUrl().Replace("-", " ").Contains(postTitle.ToLower().Replace(":", " ")), "Post title is not shown on url.");
            return true;
        }

        public void HoverOverOptions()
        {
            Base.MongoDb.UpdateSteps("Hovering over the 'Options'.");
            _browserHelper.WaitForElement(options, nameof(options));
            _browserHelper.Hover(options);
        }

        public CastrPage ClickOnOpenInCaster()
        {
            _browserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitForElement(title, nameof(title));
                _browserHelper.MoveToEl(title);
                HoverOverOptions();
                return _browserHelper.WaitForElement(openInCaster, nameof(openInCaster));
            }, "Failed to hover over options.");

            Base.MongoDb.UpdateSteps("Clicking on 'Open In Caster'.");
            _browserHelper.Click(openInCaster, nameof(openInCaster));

            return new CastrPage(_browser);
        }

        public string GetPostId()
        {
            var postParsedUrl = _browser.GetUrl().Split('/').Last();
            var postId =new string(postParsedUrl.Where(c => Char.IsDigit(c)).ToArray());

            return postId;
        }

        public override CastrPage GoToCastr()
        {
            HoverOverUserProfilePic();
            AdminPage adminPage = ClickOnAdmin();
            return adminPage.ClickOnCasterLink();
        }

        public string ValidateAds(BsonArray adsArray)
        {
            var errors = string.Empty;
            _browserHelper.WaitUntillTrue(() =>
            {
                errors = string.Empty;
                errors = IframesHandeler(adsArray);
                return string.IsNullOrEmpty(errors);
            },  "" ,30 ,false);

            return errors;
        }

        string IframesHandeler(BsonArray adsArray)
        {
            var errors = string.Empty;
            var adsNames = adsArray.Select(x => x.ToString()).ToList();

            List<string> adsUi = new List<string>();
            iframes.ToList().ForEach(f =>
            {
                var postTitle = "VIDEO:Test post article";
                BsonArray components = _params["Components"].AsBsonArray;

                _browser.Navigate(_config.Url);
                HomePage homePage = new HomePage(_browser);
                FaceBookconnectPage faceBookconnectPage = homePage.ClickOnConnectBtn();
                HomePage homePageConnected = faceBookconnectPage.Login(_config.ConfigObject.Users.AdminUser);
                homePageConnected.ValidateUserProfilePic();
                EditorPage editorPage = homePageConnected.ClickOnAddArticle();
                ArticleBase articleBase = editorPage.ClickOnArticle();
                articleBase.ClickOnMagicStick(2);
                articleBase.WriteTitle(postTitle);
                PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
                _browser.ProxyApi.NewHar();
                PostPage postPage = previewPage.ClickOnPublishBtn();
                var postId = postPage.GetPostId();
                var errors = postPage.ValidateComponents(components);
                Assert.True(string.IsNullOrEmpty(errors), errors);

                var counterRequest = _browser.ProxyApi.GetRequests().Where(r => r.Url.Contains("counter") && r.Url.Contains("reads") && r.Url.Contains(postId));
                Assert.True(counterRequest.Count() != 0, "A request to counter reads service was not sent.");
            }
        }
    }
}