using System.Collections.Generic;
using Automation.BrowserFolder;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace Automation.Helpersobjects
{
    public class DataLayer
    {
        Browser _browser;

        public DataLayer(Browser browser)
        {
            _browser = browser;
        }

        public bool WaitForEvent(string eventName)
        {
            return _browser.BrowserHelper.WaitUntillTrue(() => 
            {
                var datalayer = GetDataLayer();
                return datalayer.ContainsValue(eventName);
            }, "Data Layer is null", 60, false);
        }

        Dictionary<string, string> GetDataLayer()
        {
            var dataLayer = ((IJavaScriptExecutor)_browser.Driver).ExecuteScript("return window.dataLayer.shift()");
            var json = JsonConvert.SerializeObject(dataLayer);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return dictionary;
        }
    }
}