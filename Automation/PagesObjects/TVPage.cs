﻿using System;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class TVPage : ArticleBase
    {
        [FindsBy(How = How.CssSelector, Using = ".code")]
        IWebElement embedField { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".post-preview")]
        IWebElement previewIframe { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".video-container-image")]
        IWebElement videoContainer { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".code-wrap textarea")]
        IWebElement embedCode { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".btn.save")]
        IWebElement okBtn { get; set; }

           



       


        public TVPage(Browser broswer)
            : base(broswer)
        {

        }

        public void DragVideo()
        {
            Base.MongoDb.UpdateSteps("Drag and drop video elemnet");
            _browserHelper.WaitForElement(previewIframe, nameof(previewIframe));
            _browserHelper.DragElement(previewIframe, editorMedia);
        }

        public bool ValidateVideoAppear()
        {
            Base.MongoDb.UpdateSteps("Validate video appear");
            DragVideo();
            return _browserHelper.WaitForElement(videoContainer, nameof(videoContainer));
        }


        public void SetEmbedCode(string embadeCode)
        {
            Base.MongoDb.UpdateSteps("Set JW embed code");
            _browserHelper.WaitForElement(embedCode,nameof(embedCode));
            _browserHelper.SetText(embedCode,embadeCode); 
        }
        public void ClickOnOkBtn()
        {
            Base.MongoDb.UpdateSteps("Click on ok btn");
            _browserHelper.WaitForElement(okBtn,nameof(okBtn));
            _browserHelper.Click(okBtn,nameof(okBtn));
        }
    }
}