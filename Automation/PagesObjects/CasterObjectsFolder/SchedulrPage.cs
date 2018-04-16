using System;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class SchedulrPage : BaseObject
    {
        IWebElement time => FindElement("#time");

        List<IWebElement> leaguesList => FindElements(".league");

        List<IWebElement> postsFacebook => FindElements(".card.facebook");

        List<IWebElement> postsTwitter => FindElements(".card.twitter");

        IWebElement dayDd => FindElement("#date__3i");

        IWebElement yearDd => FindElement("#date__1i");

        IWebElement goBtn => FindElement("form button");

        List<IWebElement> leagues => FindElements(".league");

        IWebElement selectedTab => FindElement(".tab.selected");

        public SchedulrPage(Browser browser)
            :base(browser)
        {
        }

        public string ValidateTime()
        {
            Base.MongoDb.UpdateSteps("Validating date.");
            _browserHelper.WaitForElement(() => time, nameof(time));

            var dateParsed = time .Text.Split(' ');
            var day = int.Parse(dateParsed[0]);
            var month = dateParsed[1];
            var timeString = dateParsed[2];
            var zone = time.Text.Split('@')[1];

            int exDay = DateTime.Now.Day;
            string exMonth = DateTime.Now.ToString("MMMM");
            string exTime = DateTime.Now.AddHours(1).ToString("H:mm");

            return day == exDay && month == exMonth && timeString == exTime && zone.Trim() == "London (GMT)" ? "" : $"Expected date: {exDay}/{exMonth} {exTime}. Actual: {day}/{month} {timeString}";
        }

        public bool ValidateLeagues(BsonArray leaguesBson)
        {
            Base.MongoDb.UpdateSteps("Validating leagues apear.");
            var exLeagues = leaguesBson.Select(l => l.ToString());
            _browserHelper.WaitUntillTrue(() => leaguesList.ToList().Count() == exLeagues.Count());
            var acLeagues = leaguesList.ToList().Select(l => l.Text);
            var diffs = string.Join(",", exLeagues.Except(acLeagues).Union(acLeagues.Except(exLeagues)));

            return _browserHelper.WaitUntillTrue(() => exLeagues.SequenceEqual(acLeagues), $"Expected Leagues:{string.Join(",",exLeagues)} | Actual Leagues:{string.Join(",", acLeagues)}");
        }

        public bool ValidatePostTwitter(string title, int timeOut = 30, bool ex = true)
        {
            return ValidatePost(postsTwitter.ToList(), title, timeOut, ex);
        }

        public bool ValidatePostFacebook(string title, int timeOut = 30, bool ex = true)
        {
            return ValidatePost(postsFacebook.ToList(), title, timeOut, ex);
        }

        bool ValidatePost(List<IWebElement> posts, string title, int timeOut = 30, bool ex = true)
        {
            Base.MongoDb.UpdateSteps($"Validating post {title}.");
            var curHour = DateTime.Parse(time.Text.Split(' ')[2]).TimeOfDay.ToString().Split(':');
            Func<List<IWebElement>> postFunc = () => posts.ToList().Where(t => Regex.Replace(t.GetAttribute("title").Split('|').Last().ToLower().Replace('-', ' ').Trim(), @"[\d-]", string.Empty) == title).ToList();
            var postCount = postFunc().Count() == 1;
            var post = _browserHelper.WaitUntillTrue(() => postFunc().FirstOrDefault().Displayed, "", timeOut, ex);
            var hour = _browserHelper.WaitUntillTrue(() => posts.ToList().Where(p => p.GetAttribute("title").Contains(curHour[0]+":"+curHour[1])).FirstOrDefault().Displayed, "", timeOut, ex);

            return hour && post && postCount;
        }

        public void SelectDay(int date)
        {
            Base.MongoDb.UpdateSteps("Selecting Day");
            _browserHelper.WaitForElement(() => dayDd, nameof(dayDd));
            _browserHelper.SelectFromDropDown(dayDd, date.ToString());
        }

        public void SelectYear(int year)
        {
            Base.MongoDb.UpdateSteps($"Selecting Year {year}");
            _browserHelper.WaitForElement(() => yearDd, nameof(yearDd));
            _browserHelper.SelectFromDropDown(yearDd, year.ToString());
        }

        public SchedulrPage ClickOnGoBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on Go button.");
            _browserHelper.WaitForElement(() => goBtn, nameof(goBtn));
            _browserHelper.Click(goBtn, nameof(goBtn));

            return new SchedulrPage(_browser);
        }

        public SchedulrPage SelectLeague(int league)
        {
            Base.MongoDb.UpdateSteps($"Clicking on League #{league}.");
            _browserHelper.WaitUntillTrue(() => leagues.ToList().Count() >= 5);
            _browserHelper.Click(leagues.ToList()[league], $"League {league}");

            return new SchedulrPage(_browser);
        }
    }
}