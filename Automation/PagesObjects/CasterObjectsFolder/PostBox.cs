using Automation.BrowserFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.CasterObjectsFolder
{
    public class PostBox : SchedulrPage
    {
        [FindsBy(How = How.CssSelector, Using = ".urls__input")]
        IWebElement postUrl { get; set; }

        public PostBox(Browser browser)
            :base(browser)
        {
        }
    }
}