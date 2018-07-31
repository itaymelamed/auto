using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using static Automation.PagesObjects.CastrPage;

namespace Automation.PagesObjects
{
    public class SettingsPage : HomePage
    {
        IWebElement languageDd => FindElement("writing_locale");

        IWebElement saveBtn => FindElement("[name='commit']");

        IWebElement loader => FindElement(".settings-ajax-loader");

        IWebElement writeAnArticleBtn => FindElement(".write-article a");

        public SettingsPage(Browser browser)
            :base(browser)
        {
        }

        public void ChangeLanguage(Languages language)
        {
            UpdateStep($"Selecting language {language} in language Drop down list.");
            _browserHelper.WaitForElement(() => languageDd, nameof(languageDd));
            _browserHelper.SelectFromDropDown(languageDd, language.ToString());
        }

        public void ClickOnSaveBtn()
        {
            UpdateStep($"Clicking on Save button.");
            _browserHelper.WaitForElement(() => saveBtn, nameof(saveBtn));
            _browserHelper.ClickByPoint(saveBtn ,70, 0);
            _browserHelper.WaitForElementDiss(() => loader, 60);
        }

        public EditorPage ClickOnWriteAnArticle()
        {
            UpdateStep($"Clicking on Write an article button.");
            _browserHelper.WaitForElement(() => writeAnArticleBtn, nameof(writeAnArticleBtn));
            _browserHelper.Click(writeAnArticleBtn, nameof(writeAnArticleBtn));

            return new EditorPage(_browser);
        }
    }
}
