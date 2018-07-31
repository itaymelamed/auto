using Automation.TestsObjects;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Automation.BrowserFolder
{
    public class Capbilties
    {
        public DesiredCapabilities DesiredCap { get; }
        public ChromeOptions Options { get; }

        public Capbilties()
        {
            var test = (TestContext.CurrentContext.Test.Properties.Get("Test")) as Test;
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("disable-infobars");
            Options = chromeOptions;

            DesiredCapabilities capabilities = chromeOptions.ToCapabilities() as DesiredCapabilities;
            capabilities.SetCapability("name", $"Test Name:{test.TestName}, I2d: {test.TestNumber}");
            capabilities.SetCapability("videoName", $"{test.TestName}_{test.TestRunId}.mp4");
            //capabilities.SetCapability("webdriver.load.strategy", "none");
            capabilities.SetCapability("enableVNC", true);
            capabilities.SetCapability("enableVideo", true);
            //capabilities.SetCapability("pageLoadStrategy", "none");
            DesiredCap = capabilities;
        }
    }
}
