﻿using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class FacebookAppPage
    {
        [FindsBy(How = How.CssSelector, Using = "[role='article']")]
         IList<IWebElement> posts { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public FacebookAppPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public IWebElement SearchPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Serach post in Facebook.");
            var post = _browserHelper.ExecutUntillTrue(() => posts.ToList().Where(p => p.FindElement(By.XPath(".//p")).Text == title).FirstOrDefault(), "Facebook post was not created.");
            return post;
        }

        public bool ValidatePostTitle(string title)
        {
            return _browserHelper.WaitUntillTrue(() => SearchPost(title).FindElement(By.XPath(".//p")).Text == title);
        }

        public IWebElement GetPostBody(string title)
        {
             Base.MongoDb.UpdateSteps($"Serach post in body.");
             return _driver.FindElement(By.XPath($"//a[contains(text(),'{title}')]"));
         }

        public bool VlidatePostDetails(string title)
        {
            Base.MongoDb.UpdateSteps($"Validate post context.");
            var el = SearchPost(title);
            return GetPostBody(title).Text.Contains(title);
        }

        public PostPage ClickOnPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Click On facebook Post.");
            _browserHelper.ClickJavaScript(GetPostBody(title));
            _browser.SwitchToLastTab();
            return new PostPage(_browser); 
        }

        public void ScrollToPost()
        {
            Base.MongoDb.UpdateSteps($"Scroll to post..");
            _browserHelper.ScrollToBottom();
        }
    }
}
