using Automation.BrowserFolder;
using OpenQA.Selenium;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Automation.PagesObjects
{
    public class PostPage : HomePage
    {
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
                UpdateStep($"Validate Component {c}.");
                if (_browserHelper.ExecutUntillTrue(() => el = _driver.FindElement(By.CssSelector(c)), "", 0, false) == null)
                    errors += $"Component {c} does not exsist {Environment.NewLine}";
            });

            return errors;
        }

        public string ValidateTagsOnSourcePage(BsonArray tags)
        {
            UpdateStep("Validating Tags On Source Page.");
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
            UpdateStep("Validating Post creation.");
            _browserHelper.WaitUntillTrue(() => _browser.GetUrl().Contains("posts"), "User has not redirected to posts page.");
            //_browserHelper.WaitUntillTrue(() => _browser.GetUrl().Replace("-", " ").Contains(postTitle.ToLower().Replace(":", " ")), "Post title is not shown on url.");
            return true;
        }

        public void HoverOverOptions()
        {
            UpdateStep("Hovering over the 'Options'.");
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

            UpdateStep("Clicking on 'Open In Caster'.");
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
                _driver.SwitchTo().Frame(f);
                var curAd = adsNames.Intersect(_driver.FindElements(By.ClassName("primary")).Select(e => e.Text).ToList()).FirstOrDefault();
                if (curAd != null)
                {
                    UpdateStep($"Validating {curAd} displyed.");
                    adsUi.Add(curAd);
                }
                _browser.SwitchToFirstTab();
            });

            adsNames.Except(adsUi).ToList().ForEach(a => errors += $"*) Ad '{a}' does not displyed. {Environment.NewLine}");

            return errors;
        }

        public string GetAuthorName()
        {
            UpdateStep("Getting the author name from the post.");
            _browserHelper.WaitForElement(() => authorName,nameof(authorName));
            string authorNameText = authorName.Text;
            authorNameText = authorNameText.Replace("By", string.Empty);

            return authorNameText;
        }

        public PostPage ClickOnNextBtn()
        {
            UpdateStep("Clicking on next button.");
            _browserHelper.WaitForElement(() => nextBtn, nameof(nextBtn));
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(nextBtn));

            return new PostPage(_browser);
        }

        public PostPage ClickOnTransferNews(int i)
        {
            UpdateStep($"Clicking on a post in the Transfer News section  {i}");
            _browserHelper.ExecuteUntill(() => transferNews.ToList()[i].Click());
            return new PostPage(_browser);
        }

        public PostPage ClickOnTopArticle(int i)
        {
            UpdateStep($"Clicking on post in the Top Article section {i}");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(topArticles.ToList()[i]));
            return new PostPage(_browser);
        }

        public void ClickTaboolaSide(int i)
        {
            UpdateStep("Clicking on a post in the taboola side section.");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(taboolaRight.ToList()[i]));
        }

        public void ClickTaboolaBtm(int i)
        {
            UpdateStep("Clicking on a post in the taboola bottom section.");
            _browserHelper.ExecuteUntill(() => _browserHelper.ClickJavaScript(taboolaBtm.ToList()[i]));
        }

        public HomePage ClickOnLogo()
        {
            UpdateStep("Clicking on top logo.");
            _browserHelper.WaitForElement(() => logo, nameof(logo));
            _browserHelper.Click(logo, nameof(logo));

            return new HomePage(_browser);
        }

        public void ClickOnSpotim()
        {
            UpdateStep("Clicking on Spotim.");
            _browserHelper.Click(spotim, nameof(spotim));
        }

        public void ScrollToTitle()
        {
            UpdateStep("Scrolling to title.");
            _browserHelper.Click(spotim, nameof(spotim));
            _browserHelper.Hover(title);
        }

        public void ClickOnReaction(int i)
        {
            UpdateStep($"Clicking on reaction #{i}.");
            _browserHelper.WaitUntillTrue(() => reactions.ToList().Count() > 2);
            _browserHelper.Click(reactions.ToList()[i], $"Reaction #{i}");
        }

        public void ClickOnFacebookTopBtn()
        {
            UpdateStep($"Clicking on Facebook top button");
            _browserHelper.WaitForElement(() => faceBookTop, nameof(faceBookTop));
            _browserHelper.Click(faceBookTop, nameof(faceBookTop));
        }

        public void ClickOnTwitterTopBtn()
        {
            UpdateStep($"Clicking on Twitter top button");
            _browserHelper.WaitForElement(() => twitterTop, nameof(twitterTop));
            _browserHelper.Click(twitterTop, nameof(twitterTop));
        }
    }
}