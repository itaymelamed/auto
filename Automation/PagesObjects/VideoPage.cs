using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomatedTester.BrowserMob.HAR;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class VideoPage
    {
        [FindsBy(How = How.CssSelector, Using = "[aria-label='Play']")]
        IWebElement play { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[aria-label='Start Playback']")]
        IWebElement playBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.jw-state-playing")]
        IWebElement videoPlaying { get; set; }

        [FindsBy(How = How.TagName, Using = "video")]
        IWebElement video{ get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-icon-volume.jw-icon-tooltip")]
        IWebElement volume { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-icon-fullscreen")]
        IWebElement fullScreen { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-slider-time")]
        IWebElement progressBar { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-text-elapsed")]
        IWebElement timePassed { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-text-duration[role = 'timer']")]
        IWebElement videoLength { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-text-alt")]
        IWebElement adCounter { get; set; }
                            
        BrowserFolder.Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public VideoPage(BrowserFolder.Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool WaitForVideoToPlay()
        {
            Base.MongoDb.UpdateSteps("wait for video to be played.");
            return _browserHelper.WaitForElement(videoPlaying, nameof(videoPlaying), 120);
        }

        public void WaitForVideoToComplete(int sec)
        {
            Base.MongoDb.UpdateSteps("wait for video to be completed.");
            Thread.Sleep(TimeSpan.FromSeconds(sec));
        }

        public void Mute()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Click ov volume button.");
            _browserHelper.WaitForElement(volume, nameof(volume), 120);
            _browserHelper.ExecuteUntill(() => volume.Click());
        }

        public void FullScreen()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Click on FullScreen button.");
            _browserHelper.WaitForElement(fullScreen, nameof(fullScreen), 120);
            _browserHelper.Click(fullScreen, nameof(fullScreen));
        }

        public void Seek()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Drag timeline.");
            _browserHelper.WaitForElement(progressBar, nameof(progressBar), 120);
            _browserHelper.ExecuteUntill(() => progressBar.Click(), 120);
        }

        public void Pause()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Click on Pause.");
            _browserHelper.WaitForElement(play, nameof(play), 120);
            _browserHelper.Click(play, nameof(play));
        }

        void HoverOverVideo()
        {
            Base.MongoDb.UpdateSteps("Hover over video.");
            _browserHelper.WaitForElement(video, nameof(video));
            _browserHelper.Hover(video);
        }

        public void WaitForRequest(Func<List<Request>> func, string eventAction)
        {
            _browserHelper.WaitUntillTrue(() => func().Any(r => r.Url.Contains(eventAction) || r.PostData.Text.Contains(eventAction)), "Request was not sent.", 120);
        }

        public void ClickOnPlay()
        {
            Base.MongoDb.UpdateSteps("Click on Play btn.");
            _browserHelper.WaitForElement(playBtn, nameof(playBtn), 60);
            _browserHelper.Click(playBtn, nameof(playBtn));
        }

        string GetTimePassed()
        {
            _browserHelper.Hover(video);
            _browserHelper.WaitForElement(timePassed, nameof(timePassed));
            return timePassed.Text;
        }

        string GetVideoTime()
        {
            _browserHelper.Hover(video);
            _browserHelper.WaitForElement(videoLength, nameof(videoLength));
            return videoLength.Text;
        }

        int GetAdTime()
        {
            _browserHelper.WaitForElement(adCounter, nameof(adCounter));
            _browserHelper.WaitUntillTrue(() => adCounter.Text != "Loading ad");
            var xx = adCounter.Text;
            var adTotalTime = new string(adCounter.Text.ToCharArray().Where(c => char.IsNumber(c)).ToArray());
            return int.Parse(adTotalTime);
        }

        public void WaitForVideoComplete()
        {
            Base.MongoDb.UpdateSteps("Waiting for video to be completed.");
            _browserHelper.WaitUntillTrue(() => GetTimePassed() == GetVideoTime(), "Video was not completed.", 300);
        }

        public void WaitUntillVideoPrecnent(int precent)
        {
            Base.MongoDb.UpdateSteps($"Waiting for video to complete {precent}%.");
            _browserHelper.WaitUntillTrue(() => CalculateTimePassed() >= precent, "Video was not played", 300);
        }

        double CalculateTimePassed()
        {
            var videoTime = TimeSpan.Parse(GetVideoTime()).TotalMinutes;
            var videoTimePassed = TimeSpan.Parse(GetTimePassed()).TotalMinutes;
            var completed = Math.Round(videoTimePassed / videoTime * 100);
            Base.MongoDb.UpdateSteps($"Video completed : {completed}%");

            return completed;
        }

        public bool WaitForAdPrecent(int precent)
        {
            double adTime = GetAdTime();
            Thread.Sleep(2000);
            return _browserHelper.WaitUntillTrue(() => Math.Round((adTime - GetAdTime()) / adTime * 100) >= precent, "Ad was not played", 60);
        }
    }
}