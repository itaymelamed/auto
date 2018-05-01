using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.PagesObjects.ExternalPagesobjects;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class HomePage : BaseObject
    {
        protected List<IWebElement> leagues => _browserHelper.FindElements(".items-list");

        protected List<IWebElement> postsTitlesInFeedPages => _browserHelper.FindElements(".bottom-title-default__header");

        List<IWebElement> topStoriesTtitles => _browserHelper.FindElements(".bottom-title-default__header");

        List<IWebElement> moreNewsTitles => _browserHelper.FindElements(".feedpage-article__title");

        List<IWebElement> gridTitels => _browserHelper.FindElements(".bottom-title-default");

        IWebElement menu => _browserHelper.FindElement(".main-sidenav-toggle__text");

        IWebElement admin => _browserHelper.FindElement("[href='/admin']");

        IWebElement settings => _browserHelper.FindElement("[href='/edit_settings']");

        IWebElement userProfilePic => _browserHelper.FindElement(".user-menu__link img");

        IWebElement topicTitle => _browserHelper.FindElement(".page-topic__single-title");

        IWebElement coverStoryTitle => _browserHelper.FindElement(".page-topic__single-title-header");

        IWebElement coverStory => _browserHelper.FindElement(".page-topic__cover-element");

        IWebElement coverStoryLink => _browserHelper.FindElement(".page-topic__cover");

        IWebElement connectBtn => _browserHelper.FindElement(".sign-in-up__facebook-btn");

        IWebElement writeAnArticleBtn => _browserHelper.FindElement(".new-article");

        List<IWebElement> feedLinks => _browserHelper.FindElements(".nav__item__link");

        public HomePage(Browser browser)
            :base(browser)
        {
        }

        public FaceBookconnectPage ClickOnConnectBtn()
        {
            _browserHelper.WaitForElement(() => connectBtn, "Connect Button");

            UpdateStep($"Clicking on Connect Button.");
            _browserHelper.Click(connectBtn, nameof(connectBtn));

            UpdateStep($"Switching to FaceBook login tab.");
            _browser.SwitchToLastTab();

            return new FaceBookconnectPage(_browser);
        }

        public void ClickOnConnectBtnWithCoockies()
        {
            _browserHelper.WaitForElement(() => connectBtn, "Connect Button");

            UpdateStep($"Clicking on Connect Button.");
            connectBtn.Click();
            ValidateUserProfilePic();
        }

        public EditorPage ClickOnAddArticle()
        {
            _browserHelper.WaitForElement(() => writeAnArticleBtn, "Write an article Button");

            UpdateStep($"Clicking on Write New Article Button.");
            _browserHelper.Click(writeAnArticleBtn, nameof(writeAnArticleBtn));

            return new EditorPage(_browser);
        }

        public bool ValidateConnectBtn()
        {
            return _browserHelper.WaitForElement(() => connectBtn, nameof(connectBtn), 20, false);
        }

        public string ValidatemenuBtnTxt()
        {
            _browserHelper.WaitForElement(() => menu, "Menu button");
            UpdateStep($"Validating Menu button text.");
            return menu.Text;
        }

        public void ValidateUserProfilePic()
        {
            UpdateStep($"Validate user's profile pic.");
            _browserHelper.WaitForElement(() => userProfilePic, nameof(userProfilePic), 180, true);
            Thread.Sleep(1000);
        }

        public void HoverOverUserProfilePic()
        {
            UpdateStep($"Hovering over user profile picture.");
            _browserHelper.Hover(userProfilePic);
        }

        public AdminPage ClickOnAdmin()
        {
            UpdateStep($"Clicking on Admin.");
            _browserHelper.WaitForElement(() => admin, nameof(admin), 30, true);
            _browserHelper.Click(admin, nameof(admin));

            return new AdminPage(_browser);
        }

        public SettingsPage ClickOnSettings()
        {
            UpdateStep($"Clicking on Settings.");
            _browserHelper.WaitForElement(() => settings, nameof(settings), 30, true);
            _browserHelper.Click(settings, nameof(settings));

            return new SettingsPage(_browser);
        }

        public bool ValidateAdminAppears()
        {
            UpdateStep($"Validating Admin Appears.");
            return _browserHelper.WaitForElement(() => admin, nameof(admin), 2, false);
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
            UpdateStep($"Clicking on menu.");
            _browserHelper.WaitForElement(() => menu, nameof(menu));
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
            UpdateStep("Getting the topic title text.");
            _browserHelper.WaitForElement(() => topicTitle, nameof(topicTitle));
            string title = topicTitle.Text;
            return title;
        }

        public bool ValidateTopicTitle(string title)
        {
            UpdateStep("Validating the topic title text.");
            _browserHelper.RefreshUntill(() => 
            {
                string coverTitle = GetTopicText();
                return coverTitle == title;
            });

            return GetTopicText() == title;
        }

        public bool ValidateTitleApearsInGrid (string title)
        {
            UpdateStep("Validating the post appear on the grid");
            _browserHelper.WaitUntillTrue(() => gridTitels.ToList().Count() > 2);

            return gridTitels.ToList().Any(t => t.Text == title);
        }

        public string GetCoverText()
        {
            UpdateStep("Getting the title text.");
            _browserHelper.WaitForElement(() => coverStoryTitle, nameof(coverStoryTitle));
            string title = coverStoryTitle.Text;
            return title;
        }

        public bool ValidateTopStoriesTitle(string title)
        {
            UpdateStep("Validating the top stories title text.");
            bool result = false;

            _browserHelper.RefreshUntill(() =>
            {
                _browserHelper.WaitUntillTrue(() => topStoriesTtitles.ToList().Count() >= 2);
                result = topStoriesTtitles.ToList().Any(t => t.Text == title);
                return result;
            });

            return result;
        }

        public bool ValidateMoreNewsTitle(string title)
        {
            UpdateStep("Validating the more news title text.");
            bool result = false;
            _browserHelper.WaitUntillTrue(() => moreNewsTitles.ToList().Count() >= 2);
            _browserHelper.ExecuteUntill(() => result = moreNewsTitles.ToList().Any(t => t.Text == title));

            return result;
        }

        public PostPage ClickOnCoverStory()
        {
            UpdateStep("Clicking on Cover Story Title.");
            _browserHelper.Click(coverStory, nameof(coverStory));

            return new PostPage(_browser); 
        }

        public string GetCoverStoryUrl()
        {
            _browserHelper.WaitForElement(() => coverStoryLink, nameof(coverStoryLink));
            return coverStoryLink.GetAttribute("href");
        }

        public string GetFeedUrl()
        {
            _browserHelper.WaitUntillTrue(() => feedLinks.Count > 0);
            return feedLinks.Where(l => l.Displayed).ToList()[1].GetAttribute("href");
        }
    }
}