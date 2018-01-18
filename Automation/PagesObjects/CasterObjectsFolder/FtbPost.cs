﻿using System.Collections.Generic;
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
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(league);
            ChooseLeaguePage(leaguePage);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }

        public override void PublishPost(int league = 0)
        {
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(league);
            CheckPublishTo(1);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.ConfirmAlarem();
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed);
        }

        public void PublishPostToTeam(int team, int league, List<int> publishTo, string category)
        {
            Base.MongoDb.UpdateSteps($"Publish post to team #{team}.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            CheckLeague(league);
            ClickOnTeamArrow(team);
            SelectPublishTo(publishTo);
            InsertCategory(category);
            _browserHelper.Click(publishBtn, nameof(publishBtn));
            _browserHelper.WaitUntillTrue(() => sucMsg.Displayed, "Failed to publish post.");
        }
    }
}