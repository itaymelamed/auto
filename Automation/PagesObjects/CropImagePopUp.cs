using System;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class CropImagePopUp : ArticleBase
    {
        IWebElement okBtn => FindElement(".modal-crop-image__button-ok");

        IWebElement cropTitle => FindElement(".modal-crop-image__title");

        IWebElement editTitle => FindElement(".modal-edit-image__title");

        IWebElement editOkBtn => FindElement(".modal-edit-image__button-ok");

        public CropImagePopUp(Browser browser)
            :base(browser)
        {
        }

        public void ClickOnCropImageBtn()
        {
            if (!_browserHelper.WaitForElement(() => okBtn, "Cropping image",20 ,false))
            {
                _browser.Refresh();
                Thread.Sleep(2000);
                DragImage(0);
            }

            UpdateStep($"Clicking on crop image Ok button.");
            Thread.Sleep(1000);
            _browserHelper.ScrollToEl(okBtn);
            _browserHelper.ClickJavaScript(okBtn);
            _browserHelper.WaitForElementDiss(() => okBtn);
        }

        public void ClickOnEditokBtn()
        {
            _browserHelper.WaitForElement(() => editOkBtn, "Image edit button");
            UpdateStep($"Clicking on edit Ok button.");
            Thread.Sleep(1000);
            _browserHelper.ScrollToEl(editOkBtn);
            _browserHelper.ClickJavaScript(editOkBtn);
            _browserHelper.WaitForElementDiss(() => editOkBtn, 120);
        }
    }
}
