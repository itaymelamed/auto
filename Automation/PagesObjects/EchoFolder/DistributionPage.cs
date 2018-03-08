using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects.EchoFolder
{
    public class DistributionPage : EchoPage
    {
        [FindsBy(How = How.CssSelector, Using = ".default.text")]
        IWebElement channelDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[role='listitem']")]
        IList <IWebElement> topHeaderLinks { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".icon.custom")]
        IList<IWebElement> mediumsNames { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ui.raised")]
        IList<IWebElement> selectedChannels { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ui.primary")]
        IWebElement publishBTN { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ui.active.visible .default.text")]
        IWebElement channelOpenDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ui.olive.label.oval")]
        IWebElement publishedStatus { get; set; }

        public DistributionPage(Browser browser)
            :base(browser)
        
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void SelectChannel(string channel)
        {
            Base.MongoDb.UpdateSteps($"Select channel {channel} from the list.");
            _browserHelper.WaitForElement(channelDropDown,nameof(channelDropDown));
            _browserHelper.Click(channelDropDown,nameof(channelDropDown)); 
            _browserHelper.ExecuteUntill(() => mediumsNames.Where(t => t.Text == channel).FirstOrDefault().Click());
        }

        public void SelectChannel(int i)
        {
            Base.MongoDb.UpdateSteps($"Select channel from the list.");
            _browserHelper.WaitForElement(channelDropDown, nameof(channelDropDown));
            _browserHelper.Click(channelDropDown, nameof(channelDropDown));
            _browserHelper.ExecuteUntill(() => mediumsNames.ToList()[i].Click());
        }
        public void SelectChannelDPOpen(int i)
        {
            Base.MongoDb.UpdateSteps($"Select channel from the list when the dropdown is open.");
            _browserHelper.WaitForElement(channelOpenDropDown,nameof(channelOpenDropDown));
            _browserHelper.ExecuteUntill(() => mediumsNames.ToList()[i].Click());
        }

        public bool ValidateSelectedChannels(string channel)
        {
            Base.MongoDb.UpdateSteps($"Validate selected channels.");
            SelectChannel(channel);

            return _browserHelper.WaitUntillTrue(() => selectedChannels.ToList().Any(c => c.Text == channel));
        }

        public void PublishPostChannel(string channel)
        {
            Base.MongoDb.UpdateSteps($"Validate selected channels.");
            SelectChannel(channel);
            _browserHelper.WaitForElement(publishBTN, nameof(publishBTN));
            _browserHelper.Click(publishBTN, nameof(publishBTN));
        }

        public void SelectChannelByIndex(int i)
        {
            Base.MongoDb.UpdateSteps($"Validate selected channels.");
            _browserHelper.ExecuteUntill(() => SelectChannel(i));
        }

        public void ClickOnPublishBtn()
        {
            Base.MongoDb.UpdateSteps($"Click on publish button.");
            _browserHelper.Click(publishBTN,nameof(publishBTN));
        }


        public void WaitForPublishedSatatus()
        {
            Base.MongoDb.UpdateSteps($"Wait for published status");
            _browserHelper.WaitForElement(publishedStatus,nameof(publishedStatus));
        }
    }
}