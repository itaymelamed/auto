using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using OpenQA.Selenium;

namespace Automation.PagesObjects.Influencers
{
    public class LoginPage : BaseObject
    {
        IWebElement _email => FindElement("#user_email");

        IWebElement _password => FindElement("#user_password");

        IWebElement _loginBtn => FindElement(".actions input");

        public LoginPage(Browser browser)
            :base(browser)
        {
        }

        public void SetEmail(string email)
        {
            UpdateStep("Setting Email in Email textx box.");
            _browserHelper.WaitForElement(() => _email);
            _browserHelper.SetText(_email, email);
        }

        public void SetPassword(string password)
        {
            UpdateStep("Setting password in password text box.");
            _browserHelper.WaitForElement(() => _password);
            _browserHelper.SetText(_password, password);
        }

        public void ClickOnLoginBtn()
        {
            UpdateStep("Clicking on login button.");
            _browserHelper.WaitForElement(() => _loginBtn);
            _browserHelper.Click(_loginBtn);
        }

        public void Login(IUser user)
        {
            SetEmail(user.UserName);
            SetPassword(user.Password);
            ClickOnLoginBtn();
        }
    }
}