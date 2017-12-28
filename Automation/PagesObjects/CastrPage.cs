using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        [FindsBy(How = How.CssSelector, Using = "tbody tr input")]
        IList<IWebElement> resultsInputs { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> archivedPost { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> resetPosts { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> publishedPosts { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tbody tr")]
        IList<IWebElement> newPosts { get; set; }

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

        [FindsBy(How = How.CssSelector, Using = "[name='publish_to_another_site']")]
        IWebElement ftb90CheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".save")]
        IWebElement savrForLaterBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".caption textarea")]
        IWebElement captionTxtBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".form-inline textarea")]
        IList<IWebElement> textAreas { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".form-inline input")]
        IList<IWebElement> inputs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".controls button")]
        IList<IWebElement> contorls { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ok")]
        IWebElement resetConfirmPopUpOkBtn { get; set; }

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
                errors += parsedUrl == "posts" ? "" : $"Post {r.Text} is not in english {Environment.NewLine}. {parsedUrl}";
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
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "", "Post url hasn't shown", 120);
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
            if (_browserHelper.WaitForElement(resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn), 10, false))
                _browserHelper.Click(resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn));
        }

        public void PublishPost()
        {
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
        }

        public bool ValidateSucMsg()
        {
            Base.MongoDb.UpdateSteps($"Validate action suc message.");
            return _browserHelper.WaitForElement(sucMsg, nameof(sucMsg), 60, false);
        }

        public void SelectStatus(Statuses status)
        {
            Base.MongoDb.UpdateSteps($"Select status {status}.");
            _browserHelper.WaitForElement(statusDd, nameof(statusDd));
            _browserHelper.SelectFromDropDown(statusDd, status.ToString().ToLower());
            Thread.Sleep(3000);
        }

        public void ClickOnPost(int index)
        {
            Base.MongoDb.UpdateSteps($"Click on post #{index}.");
            _browserHelper.WaitForElementDiss(fetching);
            _browserHelper.WaitUntillTrue(() => 
            {
                results = results.ToList();
                return results.Count() > 1;
            }, "No posts found.");

            _browserHelper.WaitUntillTrue(() => 
            {
                IWebElement post = results.Where((r, i) => i == index).FirstOrDefault();
                _browserHelper.Click(post, $"post #{index}");
                return true;
            }, "Failed to click on post.");
        }

        public void CheckPost(int index)
        {
            Base.MongoDb.UpdateSteps($"Check post #{index}.");
            _browserHelper.WaitForElementDiss(fetching);
            _browserHelper.WaitUntillTrue(() =>
            {
                results = results.ToList();
                return results.Count() >= index;
            }, "No posts found.", 30);

            _browserHelper.WaitUntillTrue(() => 
            {
                resultsInputs = resultsInputs.ToList();
                IWebElement postCheckBox = resultsInputs.Where((r, i) => i == index).FirstOrDefault();
                _browserHelper.Click(postCheckBox, $"post #{index}");
                return resultsInputs.ToList().Any(r => r.Selected);
            }, "Failed to check post.", 30);
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
                return leagueCheckBox.ToList().Any(x => x.Selected);
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

        public bool ValidatePostArchive(string post)
        {
            Base.MongoDb.UpdateSteps($"Validate post {post} has moved to archive after status changed");
            _browserHelper.WaitForElementDiss(fetching);
            bool result = false;

            _browserHelper.WaitUntillTrue(() => {
                foreach (var p in archivedPost.ToList().Where((x,i) => i < 20))
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
            }, "Failed to find post under archive status", 120, false);


            return result;
        }

        public bool ValidatePostReset(string post)
        {
            Base.MongoDb.UpdateSteps($"Validate post {post} has moved to new after status changed");
            _browserHelper.WaitForElementDiss(fetching);
            bool result = false;

            _browserHelper.WaitUntillTrue(() => {
                foreach (var p in resetPosts.ToList().Where((x, i) => i < 20))
                {
                    Base.MongoDb.UpdateSteps($"Click on post #{resetPosts.ToList().IndexOf(p)}");
                    p.Click();
                    _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
                    Base.MongoDb.UpdateSteps($"Post {postUrl.GetAttribute("value")}");
                    if (postUrl.GetAttribute("value") == post)
                    {
                        result = true;
                        break;
                    }
                }

                return true;
            }, "Failed to find post under new status", 120, false);


            return result;
        }

        public bool ValidatePostPublish(string post)
        {
            Base.MongoDb.UpdateSteps($"Validate post {post} has moved to publish after status changed");
            _browserHelper.WaitForElementDiss(fetching);
            bool result = false;

            _browserHelper.WaitUntillTrue(() => {
                foreach (var p in publishedPosts.ToList().Where((x, i) => i < 20))
                {
                    Base.MongoDb.UpdateSteps($"Click on post #{publishedPosts.ToList().IndexOf(p)}");
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
            }, "Failed to find post under publish status", 120, false);


            return result;
        }

        public bool ValidatePostNew(string post)
        {
            Base.MongoDb.UpdateSteps($"Validate post {post} has moved to publish after status changed");
            _browserHelper.WaitForElementDiss(fetching);
            bool result = false;

            _browserHelper.WaitUntillTrue(() => {
                foreach (var p in newPosts.ToList().Where((x, i) => i < 20))
                {
                    Base.MongoDb.UpdateSteps($"Click on post #{newPosts.ToList().IndexOf(p)}");
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
            }, "Failed to find post under new status", 120, false);


            return result;
        }

        public void UncheckPublishToFtb()
        {
            Base.MongoDb.UpdateSteps($"Uncheck publish to ftb90.");
            _browserHelper.WaitForElement(ftb90CheckBox, nameof(ftb90CheckBox));
            if (_browserHelper.CheckAttribute(ftb90CheckBox))
                _browserHelper.Click(ftb90CheckBox, nameof(ftb90CheckBox));
        }

        public bool ValidateTextAreasDissabled()
        {
            Base.MongoDb.UpdateSteps($"Validate text areas are dissabled.");
            _browserHelper.WaitUntillTrue(() => textAreas.ToList().Count() >= 3, "Not all text areas were loaded.");

            return _browserHelper.ValidateElsDissabled(textAreas.ToList());
        }

        public bool ValidateInputDissabled()
        {
            Base.MongoDb.UpdateSteps($"Validate inputs are dissabled.");
            _browserHelper.WaitUntillTrue(() => inputs.ToList().Count() >= 12, "Not all inputs were loaded.");

            return _browserHelper.ValidateElsDissabled(inputs.ToList());
        }

        public bool ValidateControlsDissabled()
        {
            Base.MongoDb.UpdateSteps($"Validate buttons are dissabled.");
            _browserHelper.WaitUntillTrue(() => contorls.ToList().Count() >= 5, "Not all buttons were loaded.");

            return _browserHelper.ValidateElsDissabled(contorls.ToList().Where(c => c.GetAttribute("class").Contains("archive") || c.GetAttribute("class").Contains("save") || c.GetAttribute("class").Contains("archive")).ToList());
        }
    }
}