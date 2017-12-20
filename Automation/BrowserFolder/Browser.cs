using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Automation.ConfigurationFolder;
using System.IO;
using Automation.TestsObjects;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.BrowserFolder
{
    public class Browser
    {
        public IWebDriver Driver { get; }
        public BrowserHelper BrowserHelper { get; }

        public Browser(Configurations config)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            Driver = !Environment.MachineName.Contains("minutes-MacBook-Pro.local") ? new RemoteWebDriver(new Uri($"http://{config.Ip}:4444/wd/hub"), chromeOptions.ToCapabilities(), TimeSpan.FromMinutes(30)) :
                                 new ChromeDriver();
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

        internal void SwitchToLastTab()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
        }

        public void Close()
        {
            Driver.Close();
        }

        public string GetSource()
        {
            return Driver.PageSource;
        }
    }
}
