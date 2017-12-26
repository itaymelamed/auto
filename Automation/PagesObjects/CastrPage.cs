﻿using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Automation.PagesObjects
{
    public class CastrPage
    {
        [FindsBy(How = How.CssSelector, Using = "[name='language']")]
        IWebElement languageDd { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[name='status']")]
        IWebElement statusDd { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".fetching")]
        IWebElement fetching { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> results { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> archivedPost { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".urls__input")]
        IWebElement postUrl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[value='article']")]
        IWebElement articleCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='types_all']")]
        IWebElement allCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".types-list input")]
        IList<IWebElement> types { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-type span")]
        IList<IWebElement> typesIcons { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.multiple-archive")]
        IWebElement archiveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".multiple-reset")]
        IWebElement resetBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish")]
        IWebElement publishBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".alert-success p")]
        IWebElement sucMsg { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".league_selected [type='checkbox']")]
        IList<IWebElement> leagueCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish-list__item input")]
        IList<IWebElement> pubishToCheckBox { get; set; }

        enum Languages
        {
            en,
            es,
            de,
            fr,
            it,
            id,
            ptbr,
            th,
            vn,
            tr
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

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public CastrPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidateCasterPage()
        {
            Base.MongoDb.UpdateSteps("Validate user is on Castr page.");
            return _browserHelper.WaitForUrlToChange($"{Base._config.Url}/castr");
        }

        public void FilterByLanguage(string language)
        {
            Base.MongoDb.UpdateSteps($"Filter posts by language: {language}");
            _browserHelper.WaitForElement(languageDd, nameof(languageDd));
            SelectElement select = new SelectElement(languageDd);
            select.SelectByValue(language);
        }

        public string ValidateFilterByLanguageEn()
        {
            Base.MongoDb.UpdateSteps($"Validate filter posts by en");
            _browserHelper.WaitForElementDiss(fetching);
            var errors = string.Empty;
            results.Where((x,i) => i < 5).ToList().ForEach(r =>
            {
                _browserHelper.Click(r, r.Text);
                _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
                var parsedUrl = postUrl.GetAttribute("value").Replace("http://", "").Split('/')[1];
                errors += parsedUrl == "posts" ? "" : $"Post {r.Text} is not in english {Environment.NewLine}";
            });

            return errors;
        }

        public void DeselectAllCheckBoxes()
        {
            SelectAllCheckBoxes();
            _browserHelper.WaitForElementDiss(fetching);
            Base.MongoDb.UpdateSteps($"Deselect all checkboxes.");
            SelectAllCheckBoxes();
        }

        public void SelectAllCheckBoxes()
        {
            Base.MongoDb.UpdateSteps($"Select all checkboxes.");
            _browserHelper.WaitForElement(allCbx, nameof(allCbx));
            _browserHelper.Click(allCbx, nameof(allCbx));
        }

        public void SelectType(Types type)
        {
            _browserHelper.WaitForElementDiss(fetching);
            Base.MongoDb.UpdateSteps($"Select {type}.");

            _browserHelper.WaitUntillTrue(() => {
                var typesCount = types.ToList().Count();
                return typesCount == 10;
            });

            _browserHelper.WaitUntillTrue(() =>
            {
                var elToClick = types.ToList().Where(t => t.GetAttribute("value") == type.ToString()).FirstOrDefault();
                elToClick.Click();
                return true;
            });
        }

        public bool ValidateFilterByType(Types type)
        {
            Base.MongoDb.UpdateSteps($"Validate {type} icon is next to each post.");
            _browserHelper.WaitForElementDiss(fetching);
            return _browserHelper.WaitUntillTrue(() => typesIcons.ToList().All(t => t.GetAttribute("class") == type.ToString()));
        }

        public string GetUrl()
        {
            Base.MongoDb.UpdateSteps($"Validate post url");
            _browserHelper.WaitForElement(postUrl, nameof(postUrl));
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
            return postUrl.GetAttribute("value");
        }

        public void ArchivePost()
        {
            Base.MongoDb.UpdateSteps($"Click on archive button.");
            _browserHelper.WaitForElement(archiveBtn, nameof(archiveBtn));
            _browserHelper.Click(archiveBtn, nameof(archiveBtn));
        }

        public void ResetPost()
        {
            Base.MongoDb.UpdateSteps($"Click on reset button.");
            _browserHelper.WaitForElement(resetBtn, nameof(resetBtn));
            _browserHelper.Click(resetBtn, nameof(resetBtn));
        }

        public void PublishPost()
        {
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));
        }

        public bool ValidateSucMsg()
        {
            Base.MongoDb.UpdateSteps($"Validate Post Archive.");
            return _browserHelper.WaitForElement(sucMsg, nameof(sucMsg), 60, false);
        }

        public void SelectStatus(Statuses status)
        {
            Base.MongoDb.UpdateSteps($"Select status {status}.");
            _browserHelper.WaitForElement(statusDd, nameof(statusDd));
            _browserHelper.SelectFromDropDown(statusDd, status.ToString().ToLower());
        }

        public void ClickOnPost(int index)
        {
            Base.MongoDb.UpdateSteps($"Click on post #{index}.");
            _browserHelper.WaitForElementDiss(fetching);
            _browserHelper.WaitUntillTrue(() => 
            {
                results = results.ToList();
                return results.Count() > 1;
            });

            IWebElement post = results.Where((r, i) => i == index).FirstOrDefault();
            _browserHelper.Click(post, $"post #{index}");
        }

        public void CheckPost(int index)
        {
            Base.MongoDb.UpdateSteps($"Check post #{index}.");
            _browserHelper.WaitForElementDiss(fetching);
            _browserHelper.WaitUntillTrue(() =>
            {
                results = results.ToList();
                return results.Count() > 1;
            }, "No posts found.");

            IWebElement postCheckBox = results.Where((r, i) => i == index).FirstOrDefault().FindElement(By.XPath(".//input"));
            _browserHelper.Click(postCheckBox, $"post #{index}");
        }

        public void CheckLeague(int i)
        {
            Base.MongoDb.UpdateSteps($"Check League #{i}.");
            _browserHelper.WaitUntillTrue(() => leagueCheckBox.ToList().Count() > 2);

            _browserHelper.WaitUntillTrue(() => 
            {
                var league = leagueCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(league);
                _browserHelper.Click(league, "League #{i}");
                return true;
            }, $"Failed to check league #{i}.", 180);
        }

        public void CheckPublishTo(int i)
        {
            Base.MongoDb.UpdateSteps($"Check Publish to check box #{i}.");
            _browserHelper.WaitUntillTrue(() => pubishToCheckBox.ToList().Count() > 2);

            _browserHelper.WaitUntillTrue(() =>
            {
                var publish = pubishToCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(publish);
                _browserHelper.Click(publish, "Publish to #{i}");
                return true;
            }, $"Failed to check Publish to #{i}.");
        }

        public bool ValidatePost(string post)
        {
            Base.MongoDb.UpdateSteps($"Validate post has moved after status changed");
            _browserHelper.WaitForElementDiss(fetching);
            bool result = false;

            _browserHelper.WaitUntillTrue(() => {
                foreach (var p in archivedPost)
                {
                    Base.MongoDb.UpdateSteps($"Click on post #{archivedPost.ToList().IndexOf(p)}");
                    p.Click();
                    _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
                    var sss = postUrl.GetAttribute("value");
                    if (postUrl.GetAttribute("value") == post)
                    {
                        result = true;
                        break;
                    }
                }

                return true;
            }, "Failed to find post under new status", 30, false);


            return result;
        }
    }
}