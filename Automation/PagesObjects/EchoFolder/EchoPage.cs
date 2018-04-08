using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.EchoFolder
{
    public class EchoPage : NewsRoomPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@role='listbox'][1]")]
        IWebElement langnugeDropDown { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@role='listbox'][2]")]
        IWebElement statusDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".text.medium.regular")]
        IList<IWebElement> postsTitles { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".tableBody [style='flex: 0 0 13%;']")]
        IList<IWebElement> authorNames { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".tableBody [style='flex: 1 0 97px;']")]
        IList<IWebElement> domains { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".oval")]
        IList<IWebElement> statuses { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class='tableCell']  a[href *= 'editor']")]
        IList<IWebElement> EchoEditButtons { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".logout")]
        IWebElement logOut { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[style= 'margin-right: 3rem;']")]
        IWebElement languageFilter { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@role='listbox'][2]")]
        IWebElement StatusFilter { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".loader")]
        IWebElement loader { get; set; }

        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public EchoPage(Browser browser)
            : base(browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public bool ValidatePostCreation(string title)
        {
            Base.MongoDb.UpdateSteps($"Searching for post title: {title}.");
            return _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Any(t => t.Text == title));
        }

        public bool ValidateAuthor(string author, string title)
        {
            Base.MongoDb.UpdateSteps($"Validating author name: {author}.");
            _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Count() >= 2);
            int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
            string authorIndex = authorNames.ToList()[i].Text;
            return author == authorIndex;
        }

        public bool ValidateDomain(string domain, string title)
        {
            Base.MongoDb.UpdateSteps($"Validating the domain of the site: {domain}.");
            _browserHelper.WaitUntillTrue(() => domains.ToList().Count() >= 2);
            int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
            string domainIndex = domains.ToList()[i].Text;
            return domain.ToLower() == domainIndex;
        }

        public bool ValidateSatatus(string status, string title)
        {
            Base.MongoDb.UpdateSteps($"Validating the status of the post: {status}.");

            return _browserHelper.RefreshUntill(() =>
            {
                _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Any(t => t.Text == title));
                int i = postsTitles.ToList().Select(t => t.Text).ToList().FindIndex(t => t == title);
                string statusIndex = statuses.ToList()[i].Text;
                return statusIndex == status;
            });
        }

        public DistributionPage SelectPost(string title)
        {
            Base.MongoDb.UpdateSteps($"Selecting post {title} from the list.");
            _browserHelper.ExecuteUntill(() => postsTitles.Where(t => t.Text == title).FirstOrDefault().Click());

            return new DistributionPage(_browser);
        }

        public DistributionPage SelectPostByIndex(int i)
        {
            Base.MongoDb.UpdateSteps($"Selecting post {i} from the list.");
            _browserHelper.ExecuteUntill(() => postsTitles[i].Click());

            return new DistributionPage(_browser);
        }

        public void ClickOnEditButtonInEcho(string title)
        {
            Base.MongoDb.UpdateSteps($"Clicking on the edit button in the echo page. Title: {title}");
            _browserHelper.WaitUntillTrue(() => EchoEditButtons.ToList().Count() >= 2);
            _browserHelper.WaitForElementDiss(loader);
            var index = postsTitles.ToList().FindIndex(t => t.Text == title);;
            _browserHelper.Click(EchoEditButtons[index], "Edit Button");
        }

        public void ClickOnLogoutButton()
        {
            Base.MongoDb.UpdateSteps("Clicking on the logout button in the echo page.");
            _browserHelper.WaitForElement(logOut, nameof(logOut));
            _browserHelper.Click(logOut, nameof(logOut));
        }

        public void ClickOnLangnugeFilter()
        {
            Base.MongoDb.UpdateSteps("Clicking on language filter.");
            _browserHelper.WaitForElement(languageFilter, nameof(languageFilter));
            _browserHelper.Click(languageFilter,nameof(languageFilter));
        }

        public List<string> GetLanguagesFromDd(int exLangauges)
        {
            Base.MongoDb.UpdateSteps("Getting the lagnunges from the drop down list.");
            _browserHelper.WaitUntillTrue(() => languageFilter.FindElements(By.CssSelector(".item")).Count == exLangauges);
            List<string> languagesString = languageFilter.FindElements(By.CssSelector(".item")).Select(e => e.Text).ToList();
            return languagesString;
        }

        public bool ValidateLanguageFilterList(BsonArray languages)
        {
            Base.MongoDb.UpdateSteps("Validating Language Filter List.");

            List<string> languagesListEx = languages.Select(l => l.ToString()).ToList();
            var languagesListAc = GetLanguagesFromDd(languagesListEx.Count);

            return languagesListEx.SequenceEqual(languagesListAc);
        }

        public void ClickOnSatusFilter()
        {
            Base.MongoDb.UpdateSteps("Clicking on Status Filter.");
            _browserHelper.WaitForElement(StatusFilter, nameof(StatusFilter));
            _browserHelper.Click(StatusFilter, nameof(StatusFilter));
        }

        public List<string> GetStatusFromDd(int exStatus)
        {
            Base.MongoDb.UpdateSteps("Getting the status from the drop down list.");
            _browserHelper.WaitUntillTrue(() => StatusFilter.FindElements(By.CssSelector(".item")).Count == exStatus);
            List<string> StatusString = StatusFilter.FindElements(By.CssSelector(".item")).Select(e => e.Text).ToList();
            return StatusString;
        }

        public bool ValidateStatusFilterList(BsonArray StatusList)
        {
            Base.MongoDb.UpdateSteps("Validating status Filter List.");

            List<string> StatusListEx = StatusList.Select(l => l.ToString()).ToList();
            var languagesListAc = GetStatusFromDd(StatusListEx.Count);

            return StatusListEx.SequenceEqual(languagesListAc);
        }

        public EchoPage ClickOnLanguage(string language)
        {
            Base.MongoDb.UpdateSteps($"Clicking on language: {language}.");
            List<IWebElement> languages = new List<IWebElement>();
            _browserHelper.ExecuteUntill(() => languages = languageFilter.FindElements(By.CssSelector(".item")).ToList());
            _browserHelper.ExecuteUntill(() => languages.Where(e => e.Text == language).FirstOrDefault().Click());

            return new EchoPage(_browser);
        }

        public EchoPage ClickOnStatus(string status)
        {
            Base.MongoDb.UpdateSteps($"Clicking on status: {status}.");
            List<IWebElement> statuses = new List<IWebElement>();
            _browserHelper.ExecuteUntill(() => statuses = StatusFilter.FindElements(By.CssSelector(".item")).ToList());
            _browserHelper.ExecuteUntill(() => statuses.Where(e => e.Text == status).FirstOrDefault().Click());

            return new EchoPage(_browser);
        }

        List<string> GetPostsStatusses()
        {
            Base.MongoDb.UpdateSteps($"Getting all the posts statusses.");
            _browserHelper.WaitForElementDiss(loader);
            _browserHelper.WaitUntillTrue(() => statuses.ToList().Count > 1);
            return statuses.ToList().Select(s => s.Text).ToList();
        }

        public bool ValidateStatusses(string status)
        {
            Base.MongoDb.UpdateSteps($"Validating all the posts status: {status}.");
            var statusses = GetPostsStatusses();
            return statusses.All(s => s == status);
        }

        public void WaitForPosts()
        {
            _browserHelper.WaitForElementDiss(loader);
            _browserHelper.WaitUntillTrue(() => postsTitles.ToList().Count >= 1);
        }
    }
}