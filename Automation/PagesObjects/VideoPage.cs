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

        [FindsBy(How = How.CssSelector, Using = "div.jw-state-playing")]
        IWebElement videoPlaying { get; set; }

        [FindsBy(How = How.TagName, Using = "video")]
        IWebElement video{ get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-icon-volume.jw-icon-tooltip")]
        IWebElement volume { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-icon-fullscreen")]
        IWebElement fullScreen { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".jw-slider-time .jw-buffer")]
        IWebElement progressBar { get; set; }
            
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
            return _browserHelper.WaitForElement(videoPlaying, nameof(videoPlaying), 60);
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
            _browserHelper.Click(volume, nameof(volume));
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
            _browserHelper.Click(progressBar, nameof(progressBar));
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
            _browserHelper.Hover(video);
        }

        public void WaitForRequest(Func<List<Request>> func, string eventAction)
        {
            _browserHelper.WaitUntillTrue(() => func().Any(r => r.Url.Contains(eventAction) || r.PostData.Text.Contains(eventAction)), "Request was not sent.", 120);
        }
    }
}