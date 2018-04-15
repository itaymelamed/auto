using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Automation.BrowserFolder;
using MongoDB.Bson;
using OpenQA.Selenium;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class UrbanAirShipLoginPage : BaseObject
    {
        IWebElement userName => FindElement("#id_username");

        IWebElement password => FindElement("#id_password");

        IWebElement logInBtn => FindElement("[value='Log in']");

        List<IWebElement> pns => FindElements(".message a");

        IWebElement loader => FindElement(".loader");

        public UrbanAirShipLoginPage(Browser browser)
            :base(browser)
        {
        }

        public void Login(BsonValue user)
        {
            _browserHelper.WaitForElement(() => logInBtn, nameof(logInBtn));
            _browserHelper.SetText(userName ,user["UserName"].ToString());
            _browserHelper.SetText(password, user["Password"].ToString());
            _browserHelper.ClickJavaScript(logInBtn);
        }

        public bool SearchPost(string postTitle)
        {
            return _browserHelper.RefreshUntill(() => 
            {
                _browserHelper.WaitForElementDiss(() => loader);
                _browserHelper.WaitForElement(() => pns.FirstOrDefault(), "Pn Post" ,20 ,false);
                return pns.ToList().Any(m => Regex.Replace(m.Text.ToLower().Replace('-', ' '), @"[\d-]", string.Empty) == postTitle);
            }, $"Post {postTitle} was not found on PN list.", 120);
        }
    }
}