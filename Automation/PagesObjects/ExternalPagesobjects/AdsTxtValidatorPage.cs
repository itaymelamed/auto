using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using MongoDB.Bson;
using OpenQA.Selenium;

namespace Automation.PagesObjects.ExternalPagesobjects
{
    public class AdsTxtValidatorPage : BaseObject
    {
        IWebElement errorsNum => _browserHelper.FindElement(".error_number");

        IWebElement warningsNum => _browserHelper.FindElement(".warning_number");

        List<IWebElement> errors => _browserHelper.FindElements("#results p");

        public AdsTxtValidatorPage(Browser browser)
            :base(browser)
        {
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