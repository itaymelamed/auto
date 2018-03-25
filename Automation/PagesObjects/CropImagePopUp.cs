using System;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class CropImagePopUp : ArticleBase
    {
        [FindsBy(How = How.CssSelector, Using = ".modal-crop-image__button-ok")]
        private IWebElement okBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".modal-crop-image__title")]
        private IWebElement cropTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".modal-edit-image__title")]
        private IWebElement editTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".modal-edit-image__button-ok")]
        private IWebElement editOkBtn { get; set; }

        public CropImagePopUp(Browser browser)
            :base(browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void ClickOnCropImageBtn()
        {
            if (!_browserHelper.WaitForElement(okBtn, "Cropping image",20 ,false))
            {
                _browser.Refresh();
                Thread.Sleep(2000);
                DragImage(0);
            }
            BaseUi.MongoDb.UpdateSteps($"Clicking on crop image Ok button.");
            Thread.Sleep(1000);
            _browserHelper.ScrollToEl(okBtn);
            _browserHelper.ClickJavaScript(okBtn);
            _browserHelper.WaitForElementDiss(okBtn);
        }

        public void ClickOnEditokBtn()
        {
            _browserHelper.WaitForElement(editOkBtn, "Image edit button");
            BaseUi.MongoDb.UpdateSteps($"Clicking on edit Ok button.");
            Thread.Sleep(1000);
            _browserHelper.ScrollToEl(editOkBtn);
            _browserHelper.ClickJavaScript(editOkBtn);
            _browserHelper.WaitForElementDiss(editOkBtn, 120);
        }
    }
}
