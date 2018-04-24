using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects
{
    public class PreviewPage : BaseObject
    {
        IWebElement publishBtn => FindElement(".header-container__header-main-actions__publish");

        IWebElement editBtn => FindElement(".header-container__header-main-actions__back");

        public PreviewPage(Browser browser)
            : base(browser)
        {
        }

        public PostPage ClickOnPublishBtn()
        {
            UpdateStep($"Clicking on Publish button.");
            _browserHelper.WaitForElement(() => publishBtn, nameof(publishBtn));
            _browserHelper.Click(publishBtn, nameof(publishBtn));

            return new PostPage(_browser);
        }

        public ArticleBase ClickOnEditButton()
        {
            UpdateStep($"Clicking on edit button.");
            _browserHelper.WaitForElement(() => editBtn, nameof(editBtn));
            editBtn.Click();

            return new ArticleBase(_browser); 
        }
    }
}
