<<<<<<< HEAD
﻿
using Automation.PagesObjects;
=======
﻿using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
>>>>>>> f0a8ecf68d8dccee29fe9929b251ebf3164d1162
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
        IWebElement options => FindElement(".post-admin-options__label");

        IWebElement openInCaster => FindElement("[href*='/castr']");

        IWebElement title => FindElement(".post-article__post-title__title");

        List<IWebElement> iframes => FindElements("div[id*='google_pubconsole'] iframe");

        IWebElement authorName => FindElement(".post-metadata__author-name");

        IWebElement nextBtn => FindElement(".next-post-button__texts");

        List<IWebElement> transferNews => FindElements(".transfer-news--item");

        List<IWebElement> topArticles => FindElements(".top-posts-side-bar__item__text");

        List<IWebElement> taboolaRight => FindElements(".post-side .trc_spotlight_item");

        List<IWebElement> taboolaBtm => FindElements(".post-after .trc_spotlight_item");

        IWebElement logo => FindElement(".logo-img");

        IWebElement spotim => FindElement("div[data-spotim-module='spotim-launcher']");

        IWebElement postContent => FindElement(".post-content");

        List<IWebElement> reactions => FindElements(".reactions__list-item");

        IWebElement faceBookTop => FindElement(".cover-social-container [data-type='facebook']");

        IWebElement twitterTop => FindElement(".cover-social-container [data-type='twitter']");

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
            _browserHelper.WaitForElement(() => options, nameof(options));
            _browserHelper.Hover(options);
        } 

        public CastrPage ClickOnOpenInCaster()
        {
            _browserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitForElement(() => title, nameof(title));
                _browserHelper.MoveToEl(title);
                HoverOverOptions();
                return _browserHelper.WaitForElement(() => openInCaster, nameof(openInCaster));
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
<<<<<<< HEAD
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
=======
                _driver.SwitchTo().Frame(f);
                var curAd = adsNames.Intersect(_driver.FindElements(By.ClassName("primary")).Select(e => e.Text).ToList()).FirstOrDefault();
                if (curAd != null)
                {
                    Base.MongoDb.UpdateSteps($"Validating {curAd} displyed.");
                    adsUi.Add(curAd);
                }
                _browser.SwitchToFirstTab();
            });

            adsNames.Except(adsUi).ToList().ForEach(a => errors += $"*) Ad '{a}' does not displyed. {Environment.NewLine}");

            return errors;
        }

        public string GetAuthorName()
        {
            Base.MongoDb.UpdateSteps("Getting the author name from the post.");
            _browserHelper.WaitForElement(() => authorName,nameof(authorName));
            string authorNameText = authorName.Text;
            authorNameText = authorNameText.Replace("By", string.Empty);

            return authorNameText;
        }

        public PostPage ClickOnNextBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on next button.");
            _browserHelper.WaitForElement(() => nextBtn, nameof(nextBtn));
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(nextBtn));

            return new PostPage(_browser);
        }

        public PostPage ClickOnTransferNews(int i)
        {
            Base.MongoDb.UpdateSteps($"Clicking on a post in the Transfer News section  {i}");
            _browserHelper.ExecuteUntill(() => transferNews.ToList()[i].Click());
            return new PostPage(_browser);
        }

        public PostPage ClickOnTopArticle(int i)
        {
            Base.MongoDb.UpdateSteps($"Clicking on post in the Top Article section {i}");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(topArticles.ToList()[i]));
            return new PostPage(_browser);
        }

        public void ClickTaboolaSide(int i)
        {
            Base.MongoDb.UpdateSteps("Clicking on a post in the taboola side section.");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(taboolaRight.ToList()[i]));
        }

        public void ClickTaboolaBtm(int i)
        {
            Base.MongoDb.UpdateSteps("Clicking on a post in the taboola bottom section.");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(taboolaBtm.ToList()[i]));
        }

        public HomePage ClickOnLogo()
        {
            Base.MongoDb.UpdateSteps("Clicking on top logo.");
            _browserHelper.WaitForElement(() => logo, nameof(logo));
            _browserHelper.Click(logo, nameof(logo));

            return new HomePage(_browser);
        }

        public void ClickOnSpotim()
        {
            Base.MongoDb.UpdateSteps("Clicking on Spotim.");
            _browserHelper.Click(spotim, nameof(spotim));
        }

        public void ScrollToTitle()
        {
            Base.MongoDb.UpdateSteps("Scrolling to title.");
            _browserHelper.Click(spotim, nameof(spotim));
            _browserHelper.Hover(title);
        }

        public void ClickOnReaction(int i)
        {
            Base.MongoDb.UpdateSteps($"Clicking on reaction #{i}.");
            _browserHelper.WaitUntillTrue(() => reactions.ToList().Count() > 2);
            _browserHelper.Click(reactions.ToList()[i], $"Reaction #{i}");
        }

        public void ClickOnFacebookTopBtn()
        {
            Base.MongoDb.UpdateSteps($"Clicking on Facebook top button");
            _browserHelper.WaitForElement(() => faceBookTop, nameof(faceBookTop));
            _browserHelper.Click(faceBookTop, nameof(faceBookTop));
        }

        public void ClickOnTwitterTopBtn()
        {
            Base.MongoDb.UpdateSteps($"Clicking on Twitter top button");
            _browserHelper.WaitForElement(() => twitterTop, nameof(twitterTop));
            _browserHelper.Click(twitterTop, nameof(twitterTop));
>>>>>>> f0a8ecf68d8dccee29fe9929b251ebf3164d1162
        }
    }
}