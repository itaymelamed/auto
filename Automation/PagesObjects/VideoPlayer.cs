using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.Helpersobjects;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class VideoPlayer : BaseObject
    {


        IWebElement play => FindElement("[aria-label='Play']");

        IWebElement playBtn => FindElement("[aria-label='Start Playback']");

        IWebElement videoPlaying => FindElement("div.jw-state-playing");

        IWebElement video => FindElement("video");

        IWebElement volume => FindElement(".jw-icon-volume.jw-icon-tooltip");

        IWebElement fullScreen => FindElement(".jw-icon-fullscreen");

        IWebElement progressBar => FindElement(".jw-slider-time");

        IWebElement timePassed => FindElement(".jw-text-elapsed");

        IWebElement videoLength => FindElement(".jw-text-duration[role = 'timer']");

        IWebElement adCounter => FindElement(".jw - text - alt");
       
        public DataLayer DataLayer { get; }

        public VideoPlayer(Browser browser)
            :base(browser)
        {
            DataLayer = new DataLayer(_browser);
        }

        public bool WaitForVideoToPlay()
        {
            Base.MongoDb.UpdateSteps("Waiting for video to be played.");
            return _browserHelper.WaitForElement(() => videoPlaying, nameof(videoPlaying), 120);
        }

        public void WaitForVideoToComplete(int sec)
        {
            Base.MongoDb.UpdateSteps("Waiting for video to be completed.");
            Thread.Sleep(TimeSpan.FromSeconds(sec));
        }

        public void Mute()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Clicking ov volume button.");
            _browserHelper.WaitForElement(() => volume, nameof(volume), 120);
            _browserHelper.ExecuteUntill(() => volume.Click());
        }

        public void FullScreen()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Clicking on FullScreen button.");
            _browserHelper.WaitForElement(() => fullScreen, nameof(fullScreen), 120);
            _browserHelper.Click(fullScreen, nameof(fullScreen));
        }

        public void Seek()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Dragging timeline.");
            _browserHelper.WaitForElement(() => progressBar, nameof(progressBar), 120);
            _browserHelper.ExecuteUntill(() => progressBar.Click(), 120);
        }

        public void Pause()
        {
            HoverOverVideo();
            Base.MongoDb.UpdateSteps("Clicking on Pause.");
            _browserHelper.WaitForElement(() => play, nameof(play), 120);
            _browserHelper.Click(play, nameof(play));
        }

        void HoverOverVideo()
        {
            Base.MongoDb.UpdateSteps("Hovering over the video.");
            _browserHelper.WaitForElement(() => video, nameof(video));
            _browserHelper.Hover(video);
        }

        public void ClickOnPlay()
        {
            Base.MongoDb.UpdateSteps("Clicking on the Play button.");
            _browserHelper.WaitForElement(() => playBtn, nameof(playBtn), 60);
            _browserHelper.Click(playBtn, nameof(playBtn));
        }

        string GetTimePassed()
        {
            _browserHelper.Hover(video);
            _browserHelper.WaitForElement(() => timePassed, nameof(timePassed));
            return timePassed.Text;
        }

        string GetVideoTime()
        {
            _browserHelper.Hover(video);
            _browserHelper.WaitForElement(() => videoLength, nameof(videoLength));
            return videoLength.Text;
        }

        int GetAdTime()
        {
            _browserHelper.WaitForElement(() => adCounter, nameof(adCounter));
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