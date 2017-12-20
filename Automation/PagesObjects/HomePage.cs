using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class HomePage
    {
        [FindsBy(How = How.ClassName, Using = "sign-in-up__facebook-btn")]
        IWebElement connectBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "new-article")]
        IWebElement writeAnArticleBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".main-sidenav-toggle__text")]
        IWebElement menu { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href='/admin']")]
        IWebElement admin { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".user-menu__link img")]
        IWebElement userProfilePic { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public HomePage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public FaceBookconnectPage ClickOnConnectBtn()
        {
            _browserHelper.WaitForElement(connectBtn, "Connect Button");

            Base.MongoDb.UpdateSteps($"Click on Connect Button.");
            connectBtn.Click();

            Base.MongoDb.UpdateSteps($"Switch to FaceBook login tab.");
            _browser.SwitchToLastTab();

            return new FaceBookconnectPage(_browser);
        }

        public EditorPage ClickOnAddArticle()
        {
            _browserHelper.WaitForElement(writeAnArticleBtn, "Write an article Button");

            Base.MongoDb.UpdateSteps($"Click on Write New Article Button.");
            _browserHelper.Click(writeAnArticleBtn, nameof(writeAnArticleBtn));

            return new EditorPage(_browser);
        }

        public bool ValidateConnectBtn()
        {
            return _browserHelper.WaitForElement(connectBtn, nameof(connectBtn), 20, false);
        }

        public string ValidatemenuBtnTxt()
        {
            _browserHelper.WaitForElement(menu, "Menu button");
            Base.MongoDb.UpdateSteps($"Validate Menu button text.");
            return menu.Text;
        }

        public void ValidateUserProfilePic()
        {
            Base.MongoDb.UpdateSteps($"Validate user's profile pic.");
            _browserHelper.WaitForElement(userProfilePic, nameof(userProfilePic), 60, true);
            Thread.Sleep(1000);
        }

        public void HoverOverUserProfilePic()
        {
            Base.MongoDb.UpdateSteps($"Hover Over User Profile Picture.");
            _browserHelper.Hover(userProfilePic);
        }

        public void ClickOnAdmin()
        {
            Base.MongoDb.UpdateSteps($"Click on Admin.");
            _browserHelper.WaitForElement(admin, nameof(admin), 60, true);
            _browserHelper.Click(admin, nameof(admin));
        }

        public bool ValidateAdminAppears()
        {
            Base.MongoDb.UpdateSteps($"Validate Admin Appears.");
            return _browserHelper.WaitForElement(admin, nameof(admin), 2, false);
        }
    }
}