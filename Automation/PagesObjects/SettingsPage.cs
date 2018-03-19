using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static Automation.PagesObjects.CastrPage;

namespace Automation.PagesObjects
{
    public class SettingsPage : HomePage
    {
        [FindsBy(How = How.Id, Using = "writing_locale")]
        IWebElement languageDd { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name='commit']")]
        IWebElement saveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".settings-ajax-loader")]
        IWebElement loader { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".write-article a")]
        IWebElement writeAnArticleBtn { get; set; }

        public SettingsPage(Browser browser)
            :base(browser)
        {
        }

        public void ChangeLanguage(Languages language)
        {
            BaseUi.MongoDb.UpdateSteps($"Selecting language {language} in language Drop down list.");
            _browserHelper.WaitForElement(languageDd, nameof(languageDd));
            _browserHelper.SelectFromDropDown(languageDd, language.ToString());
        }

        public void ClickOnSaveBtn()
        {
            BaseUi.MongoDb.UpdateSteps($"Clicking on Save button.");
            _browserHelper.WaitForElement(saveBtn, nameof(saveBtn));
            _browserHelper.ClickByPoint(saveBtn ,70, 0);
            _browserHelper.WaitForElementDiss(loader, 60);
        }

        public EditorPage ClickOnWriteAnArticle()
        {
            BaseUi.MongoDb.UpdateSteps($"Clicking on Write an article button.");
            _browserHelper.WaitForElement(writeAnArticleBtn, nameof(writeAnArticleBtn));
            _browserHelper.Click(writeAnArticleBtn, nameof(writeAnArticleBtn));

            return new EditorPage(_browser);
        }
    }
}
