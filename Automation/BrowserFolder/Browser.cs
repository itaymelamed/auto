﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using Automation.TestsFolder;
using OpenQA.Selenium;
using Automation.ApiFolder;
using System.Linq;
using NUnit.Framework.Internal;

namespace Automation.BrowserFolder
{
    public class Browser
    {
        public RemoteWebDriver Driver { get; }
        public BrowserHelper BrowserHelper { get; }
        public ProxyApi ProxyApi { get; set; }
        public string SessionId { get; }
        static readonly object _syncObject = new object();
        ChromeOptions _options;

        public Browser(bool proxy = false, bool wait = true)
        {
            try
            {
                ProxyApi = proxy ? new ProxyApi(Base._config.Host) : null;
                string url = $"http://{Base._config.Host}:32005/wd/hub";
                Driver = Base._config.Local ? new ChromeDriver(CreateProxyChromeOptions(wait)) : new RemoteWebDriver(new Uri(url), GetCap(proxy, wait), TimeSpan.FromMinutes(30));
                SessionId = Driver.SessionId.ToString();
                BrowserHelper = new BrowserHelper(Driver);
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException($"Init Browser has failed. Error: {e.Message}.");
            }
        }

        public void Navigate(string url)
        {
            Base.MongoDb.UpdateSteps($"Navigating to url: {url}.");
            try
            {
                var cap = Driver.Capabilities.GetCapability("pageLoadStrategy");
                Driver.Navigate().GoToUrl(url);
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException($"Navigating to {url} was timeout. Error: {e.Message}.");
            }
        }
        
        public void Maximize()
        {
            try
            {
                Driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                throw new NUnit.Framework.AssertionException($"Window maximizing has failed.. Error: {ex.Message}.");
            }
        }

        public void Quit()
        {
            try
            {
                Driver.Quit();
            }
            catch
            {

            }
        }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        public void OpenNewTab()
        {
            try
            {
                IWebElement body = Driver.FindElement(By.TagName("body"));
                body.SendKeys(Keys.Control + 't');
            }
            catch (Exception ex)
            {
                throw new NUnit.Framework.AssertionException($"Failed to open new tab. Error: {ex.Message}.");
            }
        }

        public string GetUrl()
        {
            try
            {
                return Driver.Url;
            }
            catch
            {
                return "";
            }
        }

        public string GetScreenShot(TestsObjects.Test test)
        {
            string path = "";

            string picUrl = "";

            try
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{test.TestName}_{test.TestRunId}.jpg");
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                string screenshot = ss.AsBase64EncodedString;
                byte[] screenshotAsByteArray = ss.AsByteArray;
                ss.SaveAsFile(path, ScreenshotImageFormat.Jpeg);

                Account account = new Account(
                    "dpqroysdg",
                    "233854257866176",
                    "Uh3NWNHgTZEZz5IYF6rXkcIEx7c"
                );

                Cloudinary cloudinary = new Cloudinary(account);

                var uploadeParameters = new ImageUploadParams()
                {
                    File = new FileDescription(path)
                };

                var uploadeResult = cloudinary.Upload(uploadeParameters);

                picUrl = uploadeResult.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                Exception e = ex;
            }

            return picUrl;
        }

        public void OpenNewTab(string url = "", int timeOut = 60)
        {
            Base.MongoDb.UpdateSteps("Opening new tab.");

            try
            {
                OpenNewTab();
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                Driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                throw new NUnit.Framework.AssertionException($"Failed to open new tab. Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        public void SwitchToFirstTab()
        {
            Base.MongoDb.UpdateSteps("Swithching to first tab");
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }

        public int GetNumOfTabs()
        {
            return Driver.WindowHandles.Count;
        }

        internal void SwitchToLastTab()
        {
            Base.MongoDb.UpdateSteps("Swithching to last tab");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        internal void SwitchToTab(int i, int wait = 0)
        {
            if (wait > 0)
                BrowserHelper.WaitUntillTrue(() => GetNumOfTabs() == wait);
            Driver.SwitchTo().Window(Driver.WindowHandles[i]);
        }

        public void Close()
        {
            Driver.Close();
        }

        public string GetSource()
        {
            return Driver.PageSource;
        }

        ChromeOptions CreateChromeOptions(bool wait)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AcceptInsecureCertificates = true;

            if (!wait)
                chromeOptions.PageLoadStrategy = PageLoadStrategy.None;

            return chromeOptions;
        }

        ChromeOptions CreateProxyChromeOptions(bool wait)
        {
            ProxyApi = new ProxyApi(Base._config.Host);
            var options = CreateChromeOptions(wait);
            var prxoy = ProxyApi.CreateProxy();
            options.Proxy = prxoy;

            return options;
        }

        DesiredCapabilities GetCap(bool proxy, bool wait)
        {
            var test = TestExecutionContext.CurrentContext.CurrentTest.Properties.Get("Test") as TestsObjects.Test;
            _options = !proxy ? CreateChromeOptions(wait) : CreateProxyChromeOptions(wait);
            var capabilities = (DesiredCapabilities)_options.ToCapabilities();
            capabilities.SetCapability("browser", "chrome");
            capabilities.SetCapability("version", "66.0");
            if(!proxy)
            {
                capabilities.SetCapability("enableVNC", true);
                capabilities.SetCapability("enableVideo", true);
            }
            capabilities.SetCapability("videoName", $"{test.TestRunId}_{test.TestNumber}.mp4");
            capabilities.SetCapability("name", test.TestName);
            capabilities.SetCapability("videoFrameRate", 24);

            return capabilities;
        }

        public void AddCookies(string key, string value)
        {
            Driver.Manage().Cookies.AddCookie(new Cookie(key, value));
        }

        public void SetBrowserSize(int width, int height)
        {
            Driver.Manage().Window.Size = new System.Drawing.Size(width, height);
        }

        public Cookie GetCookie(string cookieName)
        {
            try
            {
                return Driver.Manage().Cookies.GetCookieNamed(cookieName) == null ? throw new NUnit.Framework.AssertionException($"Cookie {cookieName} is missong.") : Driver.Manage().Cookies.GetCookieNamed(cookieName);
            }
            catch
            {
                throw new NUnit.Framework.AssertionException($"Get Cookie oparation has failed.");
            }
        }

        public void DeleteCookies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }
	}
}