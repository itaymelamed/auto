﻿using Automation.BrowserFolder;
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

        [FindsBy(How = How.CssSelector, Using = "[data-slot-id] iframe")]
        IList<IWebElement> ads  { get; set; }


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
            List<string> els = new List<string>();
            List<string> aaa = new List<string>();

            var el = _driver.FindElement(By.CssSelector("[data-slot-id='Top'] iframe"));
            _driver.SwitchTo().Frame(el);
            _browserHelper.ExecuteUntill(() => els = _driver.FindElements(By.TagName("html")).Select(d => d.Text).ToList());
            var adsList = ads.ToList();
            List<string> adsNames = adsArray.Select(a => a.ToString()).ToList();
            string errors = string.Empty;
            adsNames.ForEach(n => errors += ads.ToList().Any(a => a.Text == n)? "" : $"Ad {n} was not displyed. {Environment.NewLine}");

            return errors;
        }
    }
}