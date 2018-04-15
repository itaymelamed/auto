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

        public NewsRoomPage Login(IUser user)
        {
            Base.MongoDb.UpdateSteps("Inserting Username");
            _browserHelper.WaitForElement(() => userNameTextBox, nameof(userNameTextBox));
            _browserHelper.SetText(userNameTextBox ,user.UserName);

            Base.MongoDb.UpdateSteps("Inserting Password");
            _browserHelper.WaitForElement(() => passwordTextBox, nameof(passwordTextBox));
            _browserHelper.SetText(passwordTextBox, user.Password);

            Base.MongoDb.UpdateSteps("Clicking on the login button");
            _browserHelper.WaitForElement(() => loginBtn, nameof(loginBtn));
            _browserHelper.Click(loginBtn,nameof(loginBtn));

            return new NewsRoomPage(_browser);
        }

        public bool ValidateAuto0Page()
        {
            Base.MongoDb.UpdateSteps("Validating you're in auth0 page");
            return _browserHelper.WaitForElement(() => Auto0Panel,nameof(Auto0Panel));

        }

        public ManagementPage LoginEI(IUser user)
        {
            Base.MongoDb.UpdateSteps("Inserting Username");
            _browserHelper.WaitForElement(() => userNameTextBox, nameof(userNameTextBox));
            _browserHelper.SetText(userNameTextBox, user.UserName);

            Base.MongoDb.UpdateSteps("Inserting Password");
            _browserHelper.WaitForElement(() => passwordTextBox, nameof(passwordTextBox));
            _browserHelper.SetText(passwordTextBox, user.Password);

            Base.MongoDb.UpdateSteps("Clicking on the login button");
            _browserHelper.WaitForElement(() => loginBtn, nameof(loginBtn));
            _browserHelper.Click(loginBtn, nameof(loginBtn));

            _browserHelper.WaitForUrlToChange(Base._config.Url + "/management");

            return new ManagementPage(_browser);
        }

    }
}