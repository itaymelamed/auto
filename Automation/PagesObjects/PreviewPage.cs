﻿using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class PreviewPage
    {
        [FindsBy(How = How.ClassName, Using = "header-container__header-main-actions__publish")]
        IWebElement publishBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "header-container__header-main-actions__back")]
        IWebElement editBtn { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public PreviewPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public PostPage ClickOnPublishBtn()
        {
            Base.MongoDb.UpdateSteps($"Click on Publish button.");
            _browserHelper.WaitForElement(publishBtn, nameof(publishBtn));
            publishBtn.Click();

            return new PostPage(_browser);
        }

        public ArticleBase ClickOnEditButton()
        {
            Base.MongoDb.UpdateSteps($"Click on edit button.");
            _browserHelper.WaitForElement(editBtn, nameof(editBtn));
            editBtn.Click();

            return new ArticleBase(_browser); 
        }
    }
}
