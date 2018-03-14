using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using Automation.TestsObjects;
using Automation.TestsFolder;
using OpenQA.Selenium;
using Automation.ApiFolder;
using System.Linq;

namespace Automation.BrowserFolder
{
    public class Browser
    {
        public IWebDriver Driver { get; }
        public BrowserHelper BrowserHelper { get; }
        public ProxyApi ProxyApi { get; set; }
        static readonly object _syncObject = new object();
        ChromeOptions _options;

        public Browser(HubLoadBalancer loadBalancer, bool proxy = false)
        {
            lock(_syncObject)
            {
                _options = !proxy ? CreateChromeOptions() : CreateProxyChromeOptions();
                string url = loadBalancer.GetAvailbleHub();
                Driver = new RemoteWebDriver(new Uri(url), _options.ToCapabilities(), TimeSpan.FromMinutes(30));
            }

            BrowserHelper = new BrowserHelper(Driver);
        }

        public Browser(bool proxy = false)
        {
            _options = !proxy ? CreateChromeOptions() : CreateProxyChromeOptions();
            Driver = new ChromeDriver(_options);
            BrowserHelper = new BrowserHelper(Driver);
        }

        public void Navigate(string url)
        {
            Base.MongoDb.UpdateSteps($"Navigated to url: {url}");
            Driver.Navigate().GoToUrl(url);
        }
        
        public void Maximize()
        {
            Driver.Manage().Window.Maximize();
        }

        public void Quit()
        {
            Driver.Quit();
        }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        public void OpenNewTab()
        {
            IWebElement body = Driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Control + 't');
        }

        public string GetUrl()
        {
            return Driver.Url;
        }

        public string GetScreenShot(Test test)
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
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Navigate().GoToUrl(url);
        }

        public void SwitchToFirstTab()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }

        public int GetNumOfTabs()
        {
            return Driver.WindowHandles.Count;
        }

        internal void SwitchToLastTab()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
        }

        internal void SwitchToTab(int i, int wait = 0)
        {
            var xx = Driver.WindowHandles.ToList();
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

        ChromeOptions CreateChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AcceptInsecureCertificates = true;

            return chromeOptions;
        }

        ChromeOptions CreateProxyChromeOptions()
        {
            ProxyApi = new ProxyApi(Base._config.Host);
            var options = CreateChromeOptions();
            var prxoy = ProxyApi.CreateProxy();
            options.Proxy = prxoy;

            return options;
        }

        public void AddCookies(string key, string value)
        {
            Driver.Manage().Cookies.AddCookie(new Cookie(key, value));
        }
    }
}