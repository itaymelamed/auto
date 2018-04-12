using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using MongoDB.Bson;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class AdsTxtValidatorPage
    {
        [FindsBy(How = How.CssSelector, Using = ".error_number")]
        IWebElement errorsNum { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".warning_number")]
        IWebElement warningsNum { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#results p")]
        IList<IWebElement> errors { get; set; }

        Browser _browser;
        IWebDriver _driver;
        BrowserHelper _browserHelper;

        public AdsTxtValidatorPage(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public string GetErrors(BsonArray ignor)
        {
            var errorsString = string.Empty;
            var ignorList = ignor.Select(i => i.ToString()).ToList();
            List<string> errorsList;
            if (Counter())
            {
                _browserHelper.ExecuteUntill(() =>
                {
                    errorsList = new List<string>();
                    errorsList = errors.ToList().Select(e => e.Text).ToList();
                    ignorList.ForEach(i => errorsList.RemoveAll(e => e.Contains(i)));
                    errorsList.ForEach(e => errorsString += "<div><h3>" + e + "</h3></div>");
                });
            }

            return errorsString;
        }

        bool Counter()
        {
            var result =_browserHelper.WaitUntillTrue(() => 
            {
                int errorsN;
                int warningsN;

                errorsN = int.Parse(errorsNum.Text);
                warningsN = int.Parse(warningsNum.Text);

                return errorsN > 0 || warningsN > 0;
            }, "", 5, false);

            return result;
        }
    }
}