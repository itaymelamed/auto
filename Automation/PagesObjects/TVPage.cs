using System;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class TVPage : ArticleBase
    {

        IWebElement videoLength => FindElement(".jw-text-duration[role = 'timer']");

        IWebElement embedField => FindElement(".code");

        IWebElement previewIframe => FindElement(".post-preview");

        IWebElement videoContainer => FindElement(".video-container-image");

        IWebElement embedCode => FindElement(".code-wrap textarea");

        IWebElement okBtn => FindElement(".btn.save");

        public TVPage(Browser broswer)
            : base(broswer)
        {

        }

        public void DragVideo()
        {
            Base.MongoDb.UpdateSteps("Dragging and drop video elemnet");
            _browserHelper.WaitForElement(() => previewIframe, nameof(previewIframe));
            _browserHelper.DragElement(previewIframe, editorMedia);
        }

        public bool ValidateVideoAppear()
        {
            Base.MongoDb.UpdateSteps("Validating video appears");
            DragVideo();
            return _browserHelper.WaitForElement(() => videoContainer, nameof(videoContainer));
        }

        public void SetEmbedCode(string embadeCode)
        {
            Base.MongoDb.UpdateSteps("Inserting JW embed code");
            _browserHelper.WaitForElement(() => embedCode,nameof(embedCode));
            _browserHelper.SetText(embedCode,embadeCode); 
        }

        public void ClickOnOkBtn()
        {
            Base.MongoDb.UpdateSteps("Clicking on ok button");
            _browserHelper.WaitForElement(() => okBtn,nameof(okBtn));
            _browserHelper.Click(okBtn,nameof(okBtn));
        }
    }
}