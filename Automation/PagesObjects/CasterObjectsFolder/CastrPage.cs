using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using Automation.PagesObjects.CasterObjectsFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.PagesObjects
{
    public class CastrPage : BaseObject
    {
        protected IWebElement languageDd => FindElement("[name='language']");

        protected IWebElement statusDd => FindElement("select[name='status']");

        protected IWebElement fetching => FindElement(".fetching");

        protected IWebElement articleCbx => FindElement("[value='article']");

        protected IWebElement allCbx => FindElement("[name='types_all']");

        protected IWebElement sucMsg => FindElement(".alert-success p");

        protected IWebElement captionTxtBox => FindElement(".caption textarea");

        protected IWebElement sucMsgXBtn => FindElement(".close");

        protected List<IWebElement> resultsInputs => FindElements("tbody tr input");

        protected List<IWebElement> posts => FindElements("tbody tr");

        protected List<IWebElement> types => FindElements(".types-list input");

        protected List<IWebElement> typesIcons => FindElements(".post-type span");

        protected List<IWebElement> postsTitles => FindElements("tbody span[title]");

        public enum Languages
        {
            en,
            es,
            latam,
            de,
            it,
            fr,
            tr,
            br,
            vn,
            id,
            th
        }

        public enum Types
        {
            all,
            article,
            slideShow,
            topX,
            lineUp,
            pundit,
            tv,
            timeout,
        }

        public enum Statuses
        {
            New,
            saved,
            archived,
            published,
            failed,
            publishing,
            lowQuality
        }

        public CastrPage(Browser browser)
            :base(browser)
        {
            _browserHelper.WaitForElementDiss(() => fetching);
        }

        public bool ValidateCasterPage()
        {
            Base.MongoDb.UpdateSteps("Validating user is on Castr page.");
            return _browserHelper.WaitForUrlToChange($"{Base._config.Url}/management/castr");
        }

        public void FilterByLanguage(string language)
        {
            Base.MongoDb.UpdateSteps($"Filtering posts by language: {language}");
            _browserHelper.WaitForElement(() => languageDd, nameof(languageDd));
            SelectElement select = new SelectElement(languageDd);
            select.SelectByValue(language);
        }

        public string ValidatePostLanguage(CastrPost castrPost, Languages lang)
        {
            Base.MongoDb.UpdateSteps($"Validating post's language");
            var postUrl = castrPost.GetPostUrl();
            return castrPost.GetPostUrl().ToLower().Contains(lang.ToString()) ? "" : $"{postUrl} is not in {lang}";
        }

        public string ValidatePostInEnglish(CastrPost castrPost, Languages lang)
        {
            Base.MongoDb.UpdateSteps($"Validating post's language");
            var langs = Enum.GetValues(typeof(Languages))
                .Cast<Languages>()
                .Select(v => v.ToString())
                .ToList();
            return !castrPost.GetPostUrl().Contains(lang.ToString())? "" : "Post was not in english.";
        }

        public string ValidateEnglishPosts(string postsUrl)
        {
            var errors = string.Empty;
            var langs = Enum.GetValues(typeof(Languages))
                .Cast<Languages>()
                .Select(v => v.ToString())
                .ToList();
            _browserHelper.WaitUntillTrue(() => 
            {
                errors = string.Empty;
                langs.ForEach(l => errors += !postsUrl.Contains("/" + l)? "" : $"Post {postsUrl} is not in english.");
                return true;
            });

            return errors;
        }

        public void DeselectAllCheckBoxes()
        {
            SelectAllCheckBoxes();
            _browserHelper.WaitForElementDiss(() => fetching);
            Base.MongoDb.UpdateSteps($"Deselecting all checkboxes.");
            SelectAllCheckBoxes();
        }

        public void SelectAllCheckBoxes()
        {
            Base.MongoDb.UpdateSteps($"Selecting all checkboxes.");
            _browserHelper.WaitForElement(() => allCbx, nameof(allCbx));
            _browserHelper.Click(allCbx, nameof(allCbx));
        }

        public CastrPage SelectType(Types type)
        {
            _browserHelper.WaitForElementDiss(() => fetching);
            Base.MongoDb.UpdateSteps($"Selecting {type}.");

            _browserHelper.WaitUntillTrue(() => {
                var typesCount = types.ToList().Count();
                return typesCount == 9;
            });

            _browserHelper.WaitUntillTrue(() =>
            {
                var elToClick = types.ToList().Where(t => t.GetAttribute("value") == type.ToString()).FirstOrDefault();
                elToClick.Click();
                return true;
            });

            return new CastrPage(_browser);
        }

        public bool ValidateFilterByType(Types type)
        {
            Base.MongoDb.UpdateSteps($"Validating {type} icon is next to each post.");
            _browserHelper.WaitForElementDiss(() => fetching);
            return _browserHelper.WaitUntillTrue(() => typesIcons.ToList().All(t => t.GetAttribute("class") == type.ToString()));
        }


        public bool ValidateSucMsg()
        {
            Base.MongoDb.UpdateSteps($"Validating action succsses message.");
            return _browserHelper.WaitForElement(() => sucMsg, nameof(sucMsg), 60);
        }

        public CastrPage SelectStatus(Statuses status)
        {
            Base.MongoDb.UpdateSteps($"Selecting status {status}.");
            _browserHelper.WaitForElement(() => statusDd, nameof(statusDd));
            _browserHelper.SelectFromDropDown(statusDd, status.ToString().ToLower());

            return new CastrPage(_browser);
        }

        public IWebElement SerachPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Searching for post: {title}.");
            _browserHelper.WaitUntillTrue(() => posts.ToList().Count() > 0, "No posts");
            var i = postsTitles.Select(p => Regex.Replace(p.Text.Replace('-', ' ').ToLower(), @"[\d-]", string.Empty)).ToList().FindIndex(t => t.Contains(title) || title.Contains(t));
            return _browserHelper.ExecutUntillTrue(() => postsTitles.ToList()[i], $"Could not find post {title}.", 0);
        }

        public CastrPost ClickOnPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Clicking on post: {title}.");
            _browserHelper.WaitUntillTrue(() => 
            {
                _browserHelper.Click(SerachPost(title), $"Post title {title}");
                return posts.ToList().Any(p => p.GetAttribute("class") == "selected");
            });

            return new CastrPost(_browser);
        }

        public CastrPost CheckPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Clicking on post: {title}.");
            _browserHelper.ExecuteUntill(() => 
            {
                var checkBx = postsTitles.IndexOf(SerachPost(title));
                resultsInputs[checkBx].Click();
            });

            return new CastrPost(_browser, false);
        }

        public string CheckAllPostsInEnglish()
        {
            var errors = string.Empty;
            foreach (var post in posts.ToList().Where((x, i) => i < 10))
            {
                _browserHelper.Click(post, nameof(post));
                CastrPost castrPost = new CastrPost(_browser);
                errors += ValidateEnglishPosts(castrPost.GetPostUrl());
            }

            return errors;
        }

        public bool SearchPostByTitle(string title)
        {
            Base.MongoDb.UpdateSteps($"Searching for post: {title}.");
            return _browserHelper.WaitUntillTrue(() => postsTitles.Any(p => Regex.Replace(p.Text.Replace('-', ' ').ToLower(), @"[\d-]", string.Empty) == title));
        }

        public void PublishToCategoryMultiple(List<string> titles, string category)
        {
            Base.MongoDb.UpdateSteps("Publishing multiple posts to category.");
            titles.ForEach(t => 
            {
                CastrPost post = ClickOnPost(t);
                post.PublishToCategory(category);
                _browserHelper.Click(sucMsgXBtn, nameof(sucMsgXBtn));
            });
        }
    }
}