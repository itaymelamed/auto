using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class CastrPost : CastrPage
    {
        IWebElement postUrl => FindElement(".urls__input");

        List<IWebElement> textAreas => FindElements(".form-inline textarea");

        protected List<IWebElement> inputs => FindElements(".form-inline input");

        List<IWebElement> contorls => FindElements(".controls button");

        IWebElement ftb90CheckBox => FindElement("[name='publish_to_another_site']");

        protected List<IWebElement> leagueCheckBox => FindElements(".league_selected [type='checkbox']");

        protected IWebElement archiveBtn => FindElement(".multiple-archive");

        IWebElement resetBtn => FindElement(".reset");

        protected IWebElement publishBtn => FindElement(".publish");

        List<IWebElement> pubishToCheckBox => FindElements(".publish-list__item input");

        IWebElement saveForLaterBtn => FindElement(".save");

        IWebElement resetConfirmPopUpOkBtn => FindElement(".alert-warning .ok");

        IWebElement leaguePage => FindElement(".league-branch-container .collapse");

        List<IWebElement> leaguePageInputs => FindElements(".league-branch-container input");

        List<IWebElement> publisToTeams => FindElements(".subject .collapse");

        List<IWebElement> socialMediaCbx => FindElements(".social-networks-leafs li input");

        List<IWebElement> socialNetworksArrows => FindElements(".social-networks .collapse");

        IWebElement publishAlsoToCb => FindElement("[name=publish_to_another_site]");

        List<IWebElement> teamsArrows => FindElements(".subject span.collapse");

        List<IWebElement> publishToCbx => FindElements(".branchs .leaf [type=checkbox]");

        IWebElement publishToCatgoryCb => FindElement("[name='publish_to_category']");

        IWebElement categoryTextbox => FindElement("[data-mountpoint-name=category-container] .chosen-choices input");

        IWebElement activeResult => FindElement(".active-result");

        IWebElement leaguePageLink => FindElement(".league-branch-container .subject .tag");

        IWebElement pnCheckBox => FindElement("[name='mobile_notification']");

        IWebElement pnHourCbx => FindElement("[name='pinned_on_mobile_ttl']");

        IWebElement socialNetworksChangeTimeRadio => FindElement("[data-mountpoint-name='social-networks-schedule'] [value='at']");

        IWebElement datePicker => FindElement("[data-mountpoint-name='social-networks-schedule'] #schedule-datetime-picker [name='date']");

        IWebElement dateNxtBtn => FindElement("[title='Next']");

        public enum Platforms
        {
            ftbpro,
            category,
            mobile
        }  

        public CastrPost(Browser browser, bool postOpen = true)
            :base(browser)
        {
            if(postOpen)
                _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
        }

        public bool ValidateTextAreasDissabled()
        {
            UpdateStep($"Validating text areas are dissabled.");
            _browserHelper.WaitUntillTrue(() => textAreas.ToList().Count() >= 3, "Not all text areas were loaded.");

            return _browserHelper.ValidateElsDissabled(textAreas.ToList());
        }

        public bool ValidateInputDissabled()
        {
            UpdateStep($"Validating inputs are dissabled.");
            _browserHelper.WaitUntillTrue(() => inputs.ToList().Count() >= 12, "Not all inputs were loaded.");

            return _browserHelper.ValidateElsDissabled(inputs.ToList());
        }

        public bool ValidateControlsDissabled()
        {
            UpdateStep($"Validating buttons are dissabled.");
            _browserHelper.WaitUntillTrue(() => contorls.ToList().Count() >= 5, "Not all buttons were loaded.");

            return _browserHelper.ValidateElsDissabled(contorls.ToList().Where(c => c.GetAttribute("class").Contains("archive") || c.GetAttribute("class").Contains("save") || c.GetAttribute("class").Contains("archive")).ToList());
        }

        public void UncheckPublishToFtb()
        {
            UpdateStep($"Unchecking publish to ftb90.");
            if (!_browserHelper.WaitForElement(() => ftb90CheckBox, nameof(ftb90CheckBox), 10, false))
                return;
            _browserHelper.WaitUntillTrue(() => 
            {
                if (_browserHelper.CheckAttribute(ftb90CheckBox) && ftb90CheckBox.Displayed)
                {
                    _browserHelper.ClickJavaScript(ftb90CheckBox);
                    return ftb90CheckBox.Selected;
                }

                return true;
            });
        }

        public string GetPostUrl()
        {
            UpdateStep($"Validating post url");
            _browserHelper.WaitForElement(() => postUrl, nameof(postUrl));
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "", "Post url hasn't shown", 60);
            return postUrl.GetAttribute("value");
        }

        public void CheckLeague(int i)
        {
            UpdateStep($"Checking League #{i}.");

            _browserHelper.WaitUntillTrue(() =>
            {
                var league = leagueCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(league);
                _browserHelper.ClickJavaScript(league);
                return leagueCheckBox.ToList().Any(x => x.Selected);
            }, $"Failed to check league #{i}.", 30);
        }

        public void UnCheckLeague(int i)
        {
            UpdateStep($"Checking League #{i}.");
            if (!saveForLaterBtn.Enabled)
                ResetPost();

            _browserHelper.WaitUntillTrue(() =>
            {
                var league = leagueCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(league);
                _browserHelper.ClickJavaScript(league);
                return leagueCheckBox.ToList().Any(x => !x.Selected);
            }, $"Failed to check league #{i}.", 30);
        }

        public void ArchivePost()
        {
            UpdateStep($"Clicking on archive button.");
            _browserHelper.WaitForElement(() => archiveBtn, nameof(archiveBtn));
            if (!archiveBtn.Enabled)
                ResetPost();
            _browserHelper.Click(archiveBtn, nameof(archiveBtn));
            ValidateSucMsg();
        }

        public void ResetPost()
        {
            UpdateStep($"Clicking on reset button.");
            _browserHelper.WaitForElement(() => resetBtn, nameof(resetBtn));
            _browserHelper.Click(resetBtn, nameof(resetBtn));
            if (_browserHelper.WaitForElement(() => resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn), 10, false))
                _browserHelper.Click(resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn));
            Thread.Sleep(5000);
        }

        public void CheckPublishTo(int i)
        {
            UpdateStep($"Checking Publish to check box #{i}.");
            _browserHelper.WaitUntillTrue(() => pubishToCheckBox.ToList().Count() > 2);

            _browserHelper.WaitUntillTrue(() =>
            {
                var publish = pubishToCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(publish);
                _browserHelper.ClickJavaScript(publish);
                return pubishToCheckBox.Any(x => x.GetAttribute("checked") == "true");
            }, $"Failed to check Publish to #{i}.");
        }

        public void ClickOnPublishBtn()
        {
             UpdateStep($"Clicking on Publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));
        }

        public void ChoosePlatform(Platforms platforms)
        {
            UpdateStep($"Checking League page check box #{platforms}.");
            _browserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitForElement(() => leaguePage, nameof(leaguePage));
                leaguePage.Click();
                var leagueCb = leaguePageInputs.ToList().Where(c => c.GetAttribute("value") == platforms.ToString()).FirstOrDefault();
                _browserHelper.ClickJavaScript(leagueCb);
                Thread.Sleep(1000);
                return leaguePageInputs.ToList().Where(c => c.GetAttribute("value") == platforms.ToString()).FirstOrDefault().GetAttribute("checked") == "true";
            }, $"Failed to click on league page {platforms}.");
        }

        public void SelectPublishToTeam(int team)
        {
            UpdateStep($"Selecting publish to team #{team}.");
            _browserHelper.WaitUntillTrue(() => publisToTeams.ToList().Count() >= 21, "Not all teams were loaded");
            _browserHelper.WaitUntillTrue(() =>
            {
                var teamEl = publisToTeams.ToList().Where((t, i) => i == team).FirstOrDefault();
                _browserHelper.ClickJavaScript(teamEl);
                return !teamEl.GetAttribute("class").Contains("close");
            });
        }

        public void CheckSmCbx(int socialNet)
        {
            UpdateStep($"Checking Social Media CheckBox.");
            _browserHelper.WaitUntillTrue(() => socialMediaCbx.ToList().Count() >= 2, "Social media check boxes were not loaded.");
            var cbx = _browserHelper.ExecutUntillTrue(() => socialMediaCbx.ToList().Where(c => c.Displayed).ToList()[socialNet]);
            _browserHelper.ClickJavaScript(cbx);
        }

        public void ClickOnSmArrow()
        {
            UpdateStep($"Selecting arsenal social network arrow.");
            _browserHelper.WaitUntillTrue(() => socialNetworksArrows.ToList().Count() >= 20);
            var cb =_browserHelper.ExecutUntillTrue(() => socialNetworksArrows.ToList().Where(c => c.Displayed).FirstOrDefault());
            _browserHelper.ClickJavaScript(cb);
        }

        public void PublishToSocialNetwork(int league, int team, int socialNetwork)
        {
            CheckLeague(league);
            SelectPublishToTeam(team);
            ClickOnSmArrow();
            CheckSmCbx(socialNetwork);
            UncheckPublishToFtb();
            ClickOnPublishBtn();
            _browserHelper.ConfirmAlarem();
            ValidateSucMsg();
        }

        public void PublishToSocialNetworks(int league, int team)
        {
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            SelectPublishToTeam(team);
            ClickOnSmArrow();
            CheckSmCbx(0);
            CheckSmCbx(1);
            UncheckPublishToFtb();
            ClickOnPublishBtn();
            _browserHelper.ConfirmAlarem();
            ValidateSucMsg();
        }

        public void PublishToSocialNetworksSchedul(int league, int team, string date)
        {
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            SelectPublishToTeam(team);
            ClickOnSmArrow();
            CheckSmCbx(0);
            CheckSmCbx(1);
            UncheckPublishToFtb();
            _browserHelper.WaitForElement(() => datePicker, nameof(datePicker));
            _browserHelper.ClickJavaScript(socialNetworksChangeTimeRadio);
            _browserHelper.Click(datePicker, nameof(datePicker));
            _browserHelper.Click(dateNxtBtn, nameof(dateNxtBtn));
            _browserHelper.SelectDate(date);
            ClickOnPublishBtn();
            _browserHelper.ConfirmAlarem();
            ValidateSucMsg();
        }

        public virtual void PublishPost(int league = 0)
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            CheckPublishTo(1);
            UncheckPublishToFtb();
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed);
        }

        public virtual void PublishPostToFeed(Platforms leaguePage, int league)
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            UncheckPublishToFtb();
            ChoosePlatform(leaguePage);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            //_browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public void SendPn(Platforms platform, int league)
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            UncheckPublishToFtb();
            ChoosePlatform(platform);
            UpdateStep($"Clicking on League.");
            _browserHelper.Click(leaguePageLink, "");
            Thread.Sleep(4000);
            UpdateStep($"Checking PN checkbox.");
            _browserHelper.WaitForElement(() => pnCheckBox, nameof(pnCheckBox));
            _browserHelper.Click(pnCheckBox, "");
            Thread.Sleep(4000);
            UpdateStep($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            Thread.Sleep(4000);
            _browserHelper.WaitUntillTrue(() => pnHourCbx.Displayed);
            UpdateStep($"Clicking on publish button.");
            _browserHelper.Click(publishBtn, "");
            Thread.Sleep(4000);
            UpdateStep($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            Thread.Sleep(4000);
            UpdateStep($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public bool ValidatePublishAlsoTo()
        {
            UpdateStep($"Validating 'Publish Also To'.");
            _browserHelper.WaitForElement(() => publishAlsoToCb, nameof(publishAlsoToCb));
            _browserHelper.ScrollToEl(publishAlsoToCb);
            return !_browserHelper.WaitUntillTrue(() => publishAlsoToCb.GetAttribute("checked") == "true", "Check box is checked", 5, false);
        }

        public void ClickOnTeamArrow(int team)
        {
            UpdateStep($"Clicking on team arrow #{team}.");
            _browserHelper.WaitUntillTrue(() => teamsArrows.ToList().Count() >= 21);
            var cb = _browserHelper.ExecutUntillTrue(() => teamsArrows.Where((x,i) => i == team).ToList().FirstOrDefault());
            _browserHelper.ClickJavaScript(cb);
        }

        public void SelectPublishTo(List<int> cats)
        {
            UpdateStep($"Checking category.");
            _browserHelper.WaitUntillTrue(() => publishToCbx.ToList().Count() >= 2);
            var cbxs = publishToCbx.ToList().Where(c => c.Displayed).ToList();
            cats.ForEach(c => _browserHelper.ClickJavaScript(publishToCbx.ToList()[c]));
        }

        public void CheckpublishToCategoryCb()
        {
            UpdateStep($"Checking publish to category check box.");
            _browserHelper.WaitForElement(() => publishToCatgoryCb, nameof(publishToCatgoryCb));
            _browserHelper.ClickJavaScript(publishToCatgoryCb);
        }

        public void InsertCategory(string category)
        {
            UpdateStep($"Inserting category {category}.");
            _browserHelper.ScrollToTop();
            _browserHelper.WaitForElement(() => categoryTextbox, nameof(categoryTextbox));
            _browserHelper.Click(categoryTextbox, nameof(categoryTextbox));
            _browserHelper.SetText(categoryTextbox, category);
            _browserHelper.WaitForElement(() => activeResult, nameof(activeResult));
            _browserHelper.Click(activeResult, nameof(activeResult));
        }

        public void PublishToCategory(string category)
        {
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            CheckpublishToCategoryCb();
            InsertCategory(category);
            CheckLeague(0);
            ClickOnPublishBtn();
            ValidateSucMsg();
        }
    }
}