using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class ListsTemplate : ArticleBase
    {
        [FindsBy(How = How.CssSelector, Using = ".editor-list-item.old-app")]
        private IWebElement items{ get; set; }

        [FindsBy(How = How.CssSelector, Using = ".slideshow-items li")]
        private IList<IWebElement> itemsList { get; set; }

        public ListsTemplate(Browser browser)
            : base(browser)
        {
            PageFactory.InitElements(_driver, this);
        }

        public void WriteItemTitle(string title)
        {
            _browserHelper.WaitForElement(items, nameof(items));
            IReadOnlyCollection<IWebElement> itemsList = null;
            _browserHelper.ExecutUntill(items, x =>
            {
                itemsList = _driver.FindElements(By.XPath(".//input[@name='title']"));
                return itemsList.Count == 3;
            });

            itemsList.ToList().ForEach(t => {
                t.SendKeys(title);
            });
        }

        public int CheckDeafaultItems()
        {
            return itemsList.Count();
        }
    }
}
