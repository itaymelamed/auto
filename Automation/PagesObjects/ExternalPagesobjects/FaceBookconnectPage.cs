using System.Threading;
using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class FaceBookconnectPage
    {
        [FindsBy(How = How.Id, Using = "email")]
		IWebElement emailTxtBox { get; set; }

        [FindsBy(How = How.Id, Using = "pass")]
		IWebElement passwordTxtBox { get; set; }

        [FindsBy(How = How.Name, Using = "login")]
		IWebElement loginBtn { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public FaceBookconnectPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public HomePage Login(IUser user)
        {
            _browserHelper.WaitForElement(emailTxtBox, nameof(emailTxtBox));
            _browserHelper.WaitForElement(passwordTxtBox, nameof(passwordTxtBox));
            _browserHelper.WaitForElement(loginBtn, nameof(loginBtn));

            BaseUi.MongoDb.UpdateSteps($"Set email in text box.");
            _browserHelper.SetText(emailTxtBox, user.UserName);

            BaseUi.MongoDb.UpdateSteps($"Set password in text box.");
            _browserHelper.SetText(passwordTxtBox, user.Password);

			BaseUi.MongoDb.UpdateSteps($"Click on Login button.");
			loginBtn.Click();

            Thread.Sleep(1000);

            _browser.SwitchToFirstTab();

            return new HomePage(_browser);
        }
    }
}
