using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class Auth0LoginPage : BaseObject
    {
        IWebElement userNameTextBox => FindElement("[name='username']");

        IWebElement passwordTextBox => FindElement("[name='password']");

        IWebElement loginBtn => FindElement("[type='submit']");

        IWebElement Auto0Panel => FindElement(".auth0-lock-body-content");

        public Auth0LoginPage(Browser browser)
            : base(browser)
        {
        }

        void Login(IUser user)
        {
            UpdateStep("Inserting Username");
            _browserHelper.WaitForElement(() => userNameTextBox, nameof(userNameTextBox));
            _browserHelper.SetText(userNameTextBox, user.UserName);

            UpdateStep("Inserting Password");
            _browserHelper.WaitForElement(() => passwordTextBox, nameof(passwordTextBox));
            _browserHelper.SetText(passwordTextBox, user.Password);

            UpdateStep("Clicking on the login button");
            _browserHelper.WaitForElement(() => loginBtn, nameof(loginBtn));
            _browserHelper.Click(loginBtn, nameof(loginBtn));
        }

        public NewsRoomPage LoginNewsRoom(IUser user)
        {
            Login(user);
            return new NewsRoomPage(_browser);
        }

        public ManagementPage LoginEI(IUser user)
        {
            Login(user);
            _browserHelper.WaitForUrlToChange(Base._config.Url + "/management");

            return new ManagementPage(_browser);
        }

        public bool ValidateAuto0Page()
        {
            UpdateStep("Validating you're in auth0 page.");
            return _browserHelper.WaitForElement(() => Auto0Panel,nameof(Auto0Panel));
        }
    }
}