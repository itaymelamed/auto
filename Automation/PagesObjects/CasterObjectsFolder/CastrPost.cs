using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

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

        [FindsBy(How = How.CssSelector, Using = "button.archive")]
        IWebElement archiveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".reset")]
        IWebElement resetBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish")]
        IWebElement publishBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".publish-list__item input")]
        IList<IWebElement> pubishToCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".save")]
        IWebElement saveForLaterBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".alert-warning .ok")]
        IWebElement resetConfirmPopUpOkBtn { get; set; }

        public CastrPost(Browser browser)
            :base(browser)
        {
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "");
            Thread.Sleep(2000);
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

        public void UncheckPublishToFtb()
        {
            Base.MongoDb.UpdateSteps($"Uncheck publish to ftb90.");
            if (!_browserHelper.WaitForElement(ftb90CheckBox, nameof(ftb90CheckBox), 10, false))
                return;
            _browserHelper.WaitUntillTrue(() => 
            {
                if (_browserHelper.CheckAttribute(ftb90CheckBox) && ftb90CheckBox.Displayed)
                {
                    ftb90CheckBox.Click();
                    return ftb90CheckBox.Selected;
                }

                return true;
            });
        }

        public string GetPostUrl()
        {
            Base.MongoDb.UpdateSteps($"Validate post url");
            _browserHelper.WaitForElement(postUrl, nameof(postUrl));
            _browserHelper.WaitUntillTrue(() => postUrl.GetAttribute("value") != "", "Post url hasn't shown", 60);
            return postUrl.GetAttribute("value");
        }

        public void CheckLeague(int i)
        {
            Thread.Sleep(2000);
            Base.MongoDb.UpdateSteps($"Check League #{i}.");

            _browserHelper.WaitUntillTrue(() =>
            {
                var league = leagueCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(league);
                _browserHelper.Click(league, "League #{i}");
                return leagueCheckBox.ToList().Any(x => x.Selected);
            }, $"Failed to check league #{i}.", 30);
        }

        public void PublishPost()
        {
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(0);
            CheckPublishTo(1);
            UncheckPublishToFtb();
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed);
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

        public void CheckPublishTo(int i)
        {
            Base.MongoDb.UpdateSteps($"Check Publish to check box #{i}.");
            _browserHelper.WaitUntillTrue(() => pubishToCheckBox.ToList().Count() > 2);

            _browserHelper.WaitUntillTrue(() =>
            {
                var publish = pubishToCheckBox.Where((l, j) => i == j).FirstOrDefault();
                _browserHelper.MoveToEl(publish);
                publish.Click();
                return pubishToCheckBox.Any(x => x.GetAttribute("checked") == "true");
            }, $"Failed to check Publish to #{i}.");
        }

        public void ClickOnPublishBtn()
        {
            Base.MongoDb.UpdateSteps($"Click on Publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));
        }
    }
}
