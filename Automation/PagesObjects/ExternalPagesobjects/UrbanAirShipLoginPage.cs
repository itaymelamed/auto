using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using MongoDB.Bson;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class UrbanAirShipLoginPage
    {
        [FindsBy(How = How.Id, Using = "id_username")]
        IWebElement userName { get; set; }

        [FindsBy(How = How.Id, Using = "id_password")]
        IWebElement password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[value='Log in']")]
        IWebElement logInBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".message a")]
        IList<IWebElement> pns { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".loader")]
        IWebElement loader { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public UrbanAirShipLoginPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void Login(BsonValue user)
        {
            _browserHelper.WaitForElement(logInBtn, nameof(logInBtn));
            _browserHelper.SetText(userName ,user["UserName"].ToString());
            _browserHelper.SetText(password, user["Password"].ToString());
            _browserHelper.ClickJavaScript(logInBtn);
        }

        public bool SearchPost(string postTitle)
        {
            return _browserHelper.RefreshUntill(() => 
            {
                _browserHelper.WaitForElementDiss(loader);
                _browserHelper.WaitForElement(pns.ToList().FirstOrDefault(), "Pn Post" ,20 ,false);
                return pns.ToList().Any(m => Regex.Replace(m.Text.ToLower().Replace('-', ' '), @"[\d-]", string.Empty) == postTitle);
            }, $"Post {postTitle} was not found on PN list.", 120);
        }
    }
}