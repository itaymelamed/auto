using System.Collections.Generic;
using Automation.BrowserFolder;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class FtbPost : CastrPost
    {
        public FtbPost(Browser browser)
            :base(browser)
        {
        }

        public override void PublishPostToFeed(Platforms leaguePage, int league)
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!archiveBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            ChoosePlatform(leaguePage);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public override void PublishPost(int league = 0)
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!archiveBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            CheckPublishTo(1);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed);
        }

        public void PublishPostToTeam(int team, int league, List<int> publishTo, string category)
        {
            UpdateStep($"Publishing post to team #{team}.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            if (!archiveBtn.Enabled)
                ResetPost();
            CheckLeague(league);
            ClickOnTeamArrow(team);
            SelectPublishTo(publishTo);
            InsertCategory(category);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }
    }
}