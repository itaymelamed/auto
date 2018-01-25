using System;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class SchedulrPage
    {
        [FindsBy(How = How.Id, Using = "time")]
        IWebElement time { get; set; }

        [FindsBy(How = How.ClassName, Using = "league")]
        IList<IWebElement> leaguesList { get; set; } 

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public SchedulrPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidateTime()
        {
            Base.MongoDb.UpdateSteps("Validate date.");
            _browserHelper.WaitForElement(time, nameof(time));

            var dateParsed = time .Text.Split(' ');
            var day = int.Parse(dateParsed[0]);
            var month = dateParsed[1];
            var timeString = dateParsed[2];
            var zone = time.Text.Split('@')[1];

            int exDay = DateTime.Now.Day;
            string exMonth = DateTime.Now.ToString("MMMM");
            string exTime = DateTime.Now.ToString("h:mm");

            return day == exDay && month == exMonth && timeString == exTime && zone.Trim() == "London (GMT)";
        }

        public bool ValidateLeagues(BsonArray leaguesBson)
        {
            Base.MongoDb.UpdateSteps("Validate leagues apear.");
            var exLeagues = leaguesBson.Select(l => l.ToString());
            _browserHelper.WaitUntillTrue(() => leaguesList.ToList().Count() == exLeagues.Count());
            var acLeagues = leaguesList.ToList().Select(l => l.Text);
            var diffs = string.Join(",", exLeagues.Except(acLeagues).Union(acLeagues.Except(exLeagues)));

            return _browserHelper.WaitUntillTrue(() => exLeagues.SequenceEqual(acLeagues), $"Expected Leagues:{string.Join(",",exLeagues)} | Actual Leagues:{string.Join(",", acLeagues)}");
        }
    }
}