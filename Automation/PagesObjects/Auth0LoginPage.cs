using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class Auth0LoginPage
    {
        [FindsBy(How = How.CssSelector, Using = "[name='username']")]
        IWebElement userNameTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='password']")]
        IWebElement passwordTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[type='submit']")]
        IWebElement loginBtn { get; set; }

        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public Auth0LoginPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }


        public NewsRoomPage Login(IUser user)
        {
            Base.MongoDb.UpdateSteps("Set Username");
            _browserHelper.WaitForElement(userNameTextBox, nameof(userNameTextBox));
            _browserHelper.SetText(userNameTextBox ,user.UserName);

            Base.MongoDb.UpdateSteps("Set Password");
            _browserHelper.WaitForElement(passwordTextBox, nameof(passwordTextBox));
            _browserHelper.SetText(passwordTextBox, user.Password);

            Base.MongoDb.UpdateSteps("Click on login BTN");
            _browserHelper.WaitForElement(loginBtn, nameof(loginBtn));
            _browserHelper.Click(loginBtn,nameof(loginBtn));

            return new NewsRoomPage(_browser);
        }
    }
}
