using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class CastrPost : CastrPage
    {
        [FindsBy(How = How.CssSelector, Using = ".urls__input")]
        IWebElement postUrl { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".form-inline textarea")]
        IList<IWebElement> textAreas { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".form-inline input")]
        IList<IWebElement> inputs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".controls button")]
        IList<IWebElement> contorls { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='publish_to_another_site']")]
        IWebElement ftb90CheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".league_selected [type='checkbox']")]
        IList<IWebElement> leagueCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".multiple-archive")]
        protected IWebElement archiveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".reset")]
        IWebElement resetBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish")]
        protected IWebElement publishBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish-list__item input")]
        IList<IWebElement> pubishToCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".save")]
        IWebElement saveForLaterBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".alert-warning .ok")]
        IWebElement resetConfirmPopUpOkBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".league-branch-container .collapse")]
        IWebElement leaguePage { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".league-branch-container input")]
        IList<IWebElement> leaguePageInputs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".subject .collapse")]
        IList<IWebElement> publisToTeams { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".social-networks-leafs li input")]
        IList<IWebElement> socialMediaCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".social-networks .collapse")]
        IList<IWebElement> socialNetworksArrows { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name=publish_to_another_site]")]
        IWebElement publishAlsoToCb { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".subject span.collapse")]
        IList<IWebElement> teamsArrows { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".branchs .leaf [type=checkbox]")]
        IList<IWebElement> publishToCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='publish_to_category']")]
        IWebElement publishToCatgoryCb { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-mountpoint-name=category-container] .chosen-choices input")]
        IWebElement categoryTextbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".active-result")]
        IWebElement activeResult { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".league-branch-container .subject .tag")]
        IWebElement leaguePageLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='mobile_notification']")]
        IWebElement pnCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='pinned_on_mobile_ttl']")]
        IWebElement pnHourCbx { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-mountpoint-name='social-networks-schedule'] [value='at']")]
        IWebElement socialNetworksChangeTimeRadio { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-mountpoint-name='social-networks-schedule'] #schedule-datetime-picker [name='date']")]
        IWebElement datePicker { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[title='Next']")]
        IWebElement dateNxtBtn { get; set; }

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
            Base.MongoDb.UpdateSteps($"Validating text areas are dissabled.");
            _browserHelper.WaitUntillTrue(() => textAreas.ToList().Count() >= 3, "Not all text areas were loaded.");

            return _browserHelper.ValidateElsDissabled(textAreas.ToList());
        }

        public bool ValidateInputDissabled()
        {
            Base.MongoDb.UpdateSteps($"Validating inputs are dissabled.");
            _browserHelper.WaitUntillTrue(() => inputs.ToList().Count() >= 12, "Not all inputs were loaded.");

            return _browserHelper.ValidateElsDissabled(inputs.ToList());
        }

        public bool ValidateControlsDissabled()
        {
            Base.MongoDb.UpdateSteps($"Validating buttons are dissabled.");
            _browserHelper.WaitUntillTrue(() => contorls.ToList().Count() >= 5, "Not all buttons were loaded.");

            return _browserHelper.ValidateElsDissabled(contorls.ToList().Where(c => c.GetAttribute("class").Contains("archive") || c.GetAttribute("class").Contains("save") || c.GetAttribute("class").Contains("archive")).ToList());
        }

        public void UncheckPublishToFtb()
        {
            Base.MongoDb.UpdateSteps($"Unchecking publish to ftb90.");
            if (!_browserHelper.WaitForElement(ftb90CheckBox, nameof(ftb90CheckBox), 10, false))
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
            Base.MongoDb.UpdateSteps($"Validating post url");
            _browserHelper.WaitForElement(postUrl, nameof(postUrl));
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "", "Post url hasn't shown", 60);
            return postUrl.GetAttribute("value");
        }

        public void CheckLeague(int i)
        {
            Base.MongoDb.UpdateSteps($"Checking League #{i}.");

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
            Base.MongoDb.UpdateSteps($"Checking League #{i}.");
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
            Base.MongoDb.UpdateSteps($"Clicking on archive button.");
            _browserHelper.WaitForElement(archiveBtn, nameof(archiveBtn));
            if (!archiveBtn.Enabled)
                ResetPost();
            _browserHelper.Click(archiveBtn, nameof(archiveBtn));
            ValidateSucMsg();
        }

        public void ResetPost()
        {
            Base.MongoDb.UpdateSteps($"Clicking on reset button.");
            _browserHelper.WaitForElement(resetBtn, nameof(resetBtn));
            _browserHelper.Click(resetBtn, nameof(resetBtn));
            if (_browserHelper.WaitForElement(resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn), 10, false))
                _browserHelper.Click(resetConfirmPopUpOkBtn, nameof(resetConfirmPopUpOkBtn));
            Thread.Sleep(5000);
        }

        public void CheckPublishTo(int i)
        {
            Base.MongoDb.UpdateSteps($"Checking Publish to check box #{i}.");
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
            Base.MongoDb.UpdateSteps($"Clicking on Publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));
        }

        public void ChoosePlatform(Platforms platforms)
        {
            Base.MongoDb.UpdateSteps($"Checking League page check box #{platforms}.");
            _browserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitForElement(leaguePage, nameof(leaguePage));
                leaguePage.Click();
                var leagueCb = leaguePageInputs.ToList().Where(c => c.GetAttribute("value") == platforms.ToString()).FirstOrDefault();
                _browserHelper.ClickJavaScript(leagueCb);
                Thread.Sleep(1000);
                return leaguePageInputs.ToList().Where(c => c.GetAttribute("value") == platforms.ToString()).FirstOrDefault().GetAttribute("checked") == "true";
            }, $"Failed to click on league page {platforms}.");
        }

        public void SelectPublishToTeam(int team)
        {
            Base.MongoDb.UpdateSteps($"Selecting publish to team #{team}.");
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
            Base.MongoDb.UpdateSteps($"Checking Social Media CheckBox.");
            _browserHelper.WaitUntillTrue(() => socialMediaCbx.ToList().Count() >= 2, "Social media check boxes were not loaded.");
            var cbx = _browserHelper.ExecutUntillTrue(() => socialMediaCbx.ToList().Where(c => c.Displayed).ToList()[socialNet]);
            _browserHelper.ClickJavaScript(cbx);
        }

        public void ClickOnSmArrow()
        {
            Base.MongoDb.UpdateSteps($"Selecting arsenal social network arrow.");
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
            _browserHelper.WaitForElement(datePicker, nameof(datePicker));
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
            Base.MongoDb.UpdateSteps($"Clicking on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
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
            Base.MongoDb.UpdateSteps($"Clicking on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
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
            Base.MongoDb.UpdateSteps($"Clicking on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            if (!saveForLaterBtn.Enabled)
                ResetPost();
            UncheckPublishToFtb();
            ChoosePlatform(platform);
            Base.MongoDb.UpdateSteps($"Clicking on League.");
            _browserHelper.Click(leaguePageLink, "");
            Thread.Sleep(4000);
            Base.MongoDb.UpdateSteps($"Checking PN checkbox.");
            _browserHelper.WaitForElement(pnCheckBox, nameof(pnCheckBox));
            _browserHelper.Click(pnCheckBox, "");
            Thread.Sleep(4000);
            Base.MongoDb.UpdateSteps($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            Thread.Sleep(4000);
            _browserHelper.WaitUntillTrue(() => pnHourCbx.Displayed);
            Base.MongoDb.UpdateSteps($"Clicking on publish button.");
            _browserHelper.Click(publishBtn, "");
            Thread.Sleep(4000);
            Base.MongoDb.UpdateSteps($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            Thread.Sleep(4000);
            Base.MongoDb.UpdateSteps($"Confirm alarm.");
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public bool ValidatePublishAlsoTo()
        {
            Base.MongoDb.UpdateSteps($"Validating 'Publish Also To'.");
            _browserHelper.WaitForElement(publishAlsoToCb, nameof(publishAlsoToCb));
            _browserHelper.ScrollToEl(publishAlsoToCb);
            return !_browserHelper.WaitUntillTrue(() => publishAlsoToCb.GetAttribute("checked") == "true", "Check box is checked", 5, false);
        }

        public void ClickOnTeamArrow(int team)
        {
            Base.MongoDb.UpdateSteps($"Clicking on team arrow #{team}.");
            _browserHelper.WaitUntillTrue(() => teamsArrows.ToList().Count() >= 21);
            var cb = _browserHelper.ExecutUntillTrue(() => teamsArrows.Where((x,i) => i == team).ToList().FirstOrDefault());
            _browserHelper.ClickJavaScript(cb);
        }

        public void SelectPublishTo(List<int> cats)
        {
            Base.MongoDb.UpdateSteps($"Checking category.");
            _browserHelper.WaitUntillTrue(() => publishToCbx.ToList().Count() >= 2);
            var cbxs = publishToCbx.ToList().Where(c => c.Displayed).ToList();
            cats.ForEach(c => _browserHelper.ClickJavaScript(publishToCbx.ToList()[c]));
        }

        public void CheckpublishToCategoryCb()
        {
            Base.MongoDb.UpdateSteps($"Checking publish to category check box.");
            _browserHelper.WaitForElement(publishToCatgoryCb, nameof(publishToCatgoryCb));
            _browserHelper.ClickJavaScript(publishToCatgoryCb);
        }

        public void InsertCategory(string category)
        {
            Base.MongoDb.UpdateSteps($"Inserting category {category}.");
            _browserHelper.ScrollToTop();
            _browserHelper.WaitForElement(categoryTextbox, nameof(categoryTextbox));
            _browserHelper.Click(categoryTextbox, nameof(categoryTextbox));
            _browserHelper.SetText(categoryTextbox, category);
            _browserHelper.WaitForElement(activeResult, nameof(activeResult));
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