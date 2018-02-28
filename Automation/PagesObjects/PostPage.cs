using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Automation.PagesObjects
{
    public class PostPage : HomePage
    {
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

        public PostPage(Browser browser)
            :base(browser)
        {

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
            Base.MongoDb.UpdateSteps("Validate Tags On Source Page.");
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
            Base.MongoDb.UpdateSteps("Validate Post creation.");
            _browserHelper.WaitUntillTrue(() => _browser.GetUrl().Contains("posts"), "User has not redirected to posts page.");
            //_browserHelper.WaitUntillTrue(() => _browser.GetUrl().Replace("-", " ").Contains(postTitle.ToLower().Replace(":", " ")), "Post title is not shown on url.");
            return true;
        }

        public void HoverOverOptions()
        {
            Base.MongoDb.UpdateSteps("Hover over 'Options'.");
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

            Base.MongoDb.UpdateSteps("Click on 'Open In Caster'.");
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
                    Base.MongoDb.UpdateSteps($"Validate {curAd} displyed.");
                    adsUi.Add(curAd);
                }
                _browser.SwitchToFirstTab();
            });

            adsNames.Except(adsUi).ToList().ForEach(a => errors += $"*) Ad '{a}' does not displyed. {Environment.NewLine}");

            return errors;
        }

       public string GetAuthorName()
        {
            Base.MongoDb.UpdateSteps("Get the author name from the post.");
            _browserHelper.WaitForElement(authorName,nameof(authorName));
            string authorNameText = authorName.Text;
            authorNameText = authorNameText.Replace("By", string.Empty);
            return authorNameText;
            
        }
    }
}