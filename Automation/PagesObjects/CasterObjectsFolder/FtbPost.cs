using System;
using Automation.BrowserFolder;
using Automation.TestsFolder;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class FtbPost : CastrPost
    {
        public FtbPost(Browser browser)
            :base(browser)
        {
        }

        public override void PublishPostToFeed(LeaguePages leaguePage, int league)
        {
            BaseUi.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(league);
            ChooseLeaguePage(leaguePage);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public override void PublishPost()
        {
            BaseUi.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(0);
            CheckPublishTo(1);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed);
        }

    }
}
