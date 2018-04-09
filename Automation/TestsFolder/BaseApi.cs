using System;
using System.Collections.Generic;
using System.Linq;
using AutomatedTester.BrowserMob.HAR;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder
{
    public class BaseApi : Base
    {
        protected BrowserFolder.Browser _browser { get; set; }

        [SetUp]
        public void OneTimeSetUp()
        {
            _browser = new BrowserFolder.Browser(true);
            _browser.Maximize();
            _browser.Navigate(_config.ConfigObject.Echo);
            _browser.ProxyApi.NewHar(null, "?captureHeaders=true");
            _browser.ProxyApi.NewHarPost();

            Auth0LoginPage loginPage = new Auth0LoginPage(_browser);
            NewsRoomPage newsRoomPage = loginPage.Login(_config.ConfigObject.Users.AdminUser);
            List<Request> requests = _browser.ProxyApi.GetRequests();
            var postsRequest = requests.Where(r => r.Url == "http://echo.minutemediaservices.com/api/v1/posts" && r.Method == "POST").ToList();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _browser.Quit();
        }
    }
}
