using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.PagesObjects.ExternalPagesobjects;
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

        [FindsBy(How = How.CssSelector, Using = "[href='/edit_settings']")]
        IWebElement settings { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".user-menu__link img")]
        IWebElement userProfilePic { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".items-list")]
        protected IList<IWebElement> leagues { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".bottom-title-default__header")]
        protected IList<IWebElement> postsTitlesInFeedPages { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".page-topic__single-title")]
        IWebElement topicTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".bottom-title-default")]
        IList<IWebElement> gridTitels { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".page-topic__single-title-header")]
        IWebElement coverStoryTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".bottom-title-default__header")]
        IList<IWebElement> topStoriesTtitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".feedpage-article__title")]
        IList<IWebElement> moreNewsTitles { get; set; }


        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

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

        public void ClickOnConnectBtnWithCoockies()
        {
            _browserHelper.WaitForElement(connectBtn, "Connect Button");

            Base.MongoDb.UpdateSteps($"Click on Connect Button.");
            connectBtn.Click();
            ValidateUserProfilePic();
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
            _browserHelper.WaitForElement(userProfilePic, nameof(userProfilePic), 180, true);
            Thread.Sleep(1000);
        }

        public void HoverOverUserProfilePic()
        {
            Base.MongoDb.UpdateSteps($"Hover Over User Profile Picture.");
            _browserHelper.Hover(userProfilePic);
        }

        public AdminPage ClickOnAdmin()
        {
            Base.MongoDb.UpdateSteps($"Click on Admin.");
            _browserHelper.WaitForElement(admin, nameof(admin), 30, true);
            _browserHelper.Click(admin, nameof(admin));

            return new AdminPage(_browser);
        }

        public SettingsPage ClickOnSettings()
        {
            Base.MongoDb.UpdateSteps($"Click on Settings.");
            _browserHelper.WaitForElement(settings, nameof(settings), 30, true);
            _browserHelper.Click(settings, nameof(settings));

            return new SettingsPage(_browser);
        }

        public bool ValidateAdminAppears()
        {
            Base.MongoDb.UpdateSteps($"Validate Admin Appears.");
            return _browserHelper.WaitForElement(admin, nameof(admin), 2, false);
        }

        public HomePage Login(IUser user)
        {
            FaceBookconnectPage faceBookconnectPage = ClickOnConnectBtn();
            HomePage homePageConnected = faceBookconnectPage.Login(user);
            homePageConnected.ValidateUserProfilePic();

            return new HomePage(_browser); 
        }

        public virtual CastrPage GoToCastr()
        {
            AdminPage adminPage = new AdminPage(_browser);
            return adminPage.ClickOnCasterLink();
        }

        public CastrPage GotoCastrByUrl(string brandBaseUrl)
        {
            _browser.Navigate($"{brandBaseUrl}/management/castr");
            return new CastrPage(_browser); 
        }

        public void ClickOnMenu()
        {
            Base.MongoDb.UpdateSteps($"Click on menu.");
            _browserHelper.WaitForElement(menu, nameof(menu));
            _browserHelper.Click(menu, nameof(menu));
        }

        public LeagueFeed ClickLeague(int league)
        {
            _browserHelper.WaitUntillTrue(() => leagues.ToList().Count() >= 6, "Not all leagues were loaded.");
            var leagueEl = _browserHelper.ExecutUntillTrue(() => leagues.ToList().Where((l, i) => i == league).FirstOrDefault());
            _browserHelper.Click(leagueEl, nameof(leagueEl));

            return new LeagueFeed(_browser);
        }

        string GetTopicText()
        {
            Base.MongoDb.UpdateSteps("Get the topic title text.");
            _browserHelper.WaitForElement(topicTitle, nameof(topicTitle));
            string title = topicTitle.Text;
            return title;
        }

        public bool ValidateTopicTitle(string title)
        {
            Base.MongoDb.UpdateSteps("Validate the topic title text.");
            string coverTitle = GetTopicText();
            return coverTitle == title;
        }

        public bool ValidateTitleApearsInGrid (string title)
        {
            Base.MongoDb.UpdateSteps("Validate the post appear on the grid");
            _browserHelper.WaitUntillTrue(() => gridTitels.ToList().Count() > 2);

            return gridTitels.ToList().Any(t => t.Text == title);
        }

        public string GetCoverText()
        {
            Base.MongoDb.UpdateSteps("Get the title text.");
            _browserHelper.WaitForElement(coverStoryTitle, nameof(coverStoryTitle));
            string title = coverStoryTitle.Text;
            return title;
        }

        public bool ValidateTopStoriesTitle(string title)
        {
            Base.MongoDb.UpdateSteps("Validate the top stories title text.");
            bool result = false;
            _browserHelper.WaitUntillTrue(() => topStoriesTtitle.ToList().Count() >= 2);
            _browserHelper.ExecuteUntill(() => result = topStoriesTtitle.ToList().Any(t => t.Text == title));

            return result;
        }

        public bool ValidateMoreNewsTitle(string title)
        {
            Base.MongoDb.UpdateSteps("Validate the more news title text.");
            bool result = false;
            _browserHelper.WaitUntillTrue(() => moreNewsTitles.ToList().Count() >= 2);
            _browserHelper.ExecuteUntill(() => result = moreNewsTitles.ToList().Any(t => t.Text == title));

            return result;
        }
    }
}