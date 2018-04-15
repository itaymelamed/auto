using System.Threading;
using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class FaceBookconnectPage : BaseObject
    {
        IWebElement emailTxtBox => FindElement("#email");

        IWebElement passwordTxtBox => FindElement("#pass");

        IWebElement loginBtn => FindElement("[name='login']");

        public FaceBookconnectPage(Browser browser)
            :base(browser)
        {
        }

        public HomePage Login(IUser user)
        {
            _browserHelper.WaitForElement(() => emailTxtBox, nameof(emailTxtBox));
            _browserHelper.WaitForElement(() => passwordTxtBox, nameof(passwordTxtBox));
            _browserHelper.WaitForElement(() => loginBtn, nameof(loginBtn));

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
