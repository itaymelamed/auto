using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using OpenQA.Selenium;

namespace Automation.PagesObjects.EchoFolder
{
    public class DistributionPage : EchoPage
    {
        IWebElement channelDropDown => FindElement(".default.text");

        List<IWebElement> topHeaderLinks => FindElements("[role='listitem']");

        List<IWebElement> mediumsNames => FindElements(".icon.custom");

        List<IWebElement> selectedChannels => FindElements(".ui.raised");

        IWebElement publishBTN => FindElement(".ui.primary");

        IWebElement channelOpenDropDown => FindElement(".ui.active.visible .default.text");

        IWebElement publishedStatus => FindElement(".ui.olive.label.oval");

        IWebElement trashIcon => FindElement(".trashButton");

        IWebElement yesBtn => FindElement(".actions .primary");

        IWebElement openLink => FindElement("[role='listitem'] a[href*='posts']");

        IWebElement editLink => FindElement("[role='listitem'] a[href*='editor']");

        IWebElement FromPublishedToNewButton => FindElement(".ui.label.oval.label.leftOptionSelected");

        IWebElement MediumCombobox => FindElement("[role='combobox']");

        public DistributionPage(Browser browser)
            : base(browser)
        {
        }

        public void SelectChannel(string channel)
        {
            UpdateStep($"Selecting channel {channel} from the list.");
            _browserHelper.WaitForElement(() => channelDropDown, nameof(channelDropDown));
            _browserHelper.Click(channelDropDown, nameof(channelDropDown));
            _browserHelper.ExecuteUntill(() => mediumsNames.Where(t => t.Text == channel).FirstOrDefault().Click());
        }

        public void SelectChannel(int i)
        {
            UpdateStep($"Selecting channel from the list.");
            _browserHelper.WaitForElement(() => channelDropDown, nameof(channelDropDown));
            _browserHelper.Click(channelDropDown, nameof(channelDropDown));
            _browserHelper.ExecuteUntill(() => mediumsNames.ToList()[i].Click());
        }

        public void SelectChannelDPOpen(int i)
        {
            UpdateStep($"Selecting channel from the list when the dropdown is open.");
            _browserHelper.WaitForElement(() => channelOpenDropDown, nameof(channelOpenDropDown));
            _browserHelper.ExecuteUntill(() => mediumsNames.ToList()[i].Click());
        }

        public bool ValidateSelectedChannels(string channel)
        {
            UpdateStep($"Validating selected channels.");
            SelectChannel(channel);

            return _browserHelper.WaitUntillTrue(() => selectedChannels.ToList().Any(c => c.Text == channel));
        }

        public void PublishPostChannel(string channel)
        {
            UpdateStep($"Validating selected channels.");
            SelectChannel(channel);
            _browserHelper.WaitForElement(() => publishBTN, nameof(publishBTN));
            _browserHelper.Click(publishBTN, nameof(publishBTN));
        }

        public void SelectChannelByIndex(int i)
        {
            UpdateStep($"Validating selected channels.");
            _browserHelper.ExecuteUntill(() => SelectChannel(i));
        }

        public void ClickOnPublishBtn()
        {
            UpdateStep($"Clicking on publish button.");
            _browserHelper.Click(publishBTN, nameof(publishBTN));
        }

        public void WaitForPublishedSatatus()
        {
            UpdateStep($"Waiting for published status");
            _browserHelper.WaitForElement(() => publishedStatus, nameof(publishedStatus));
        }

        public void MarkSelectedChannels()
        {
            UpdateStep($"Mark selected channels in the distribution page");
            _browserHelper.Click(publishedStatus, nameof(publishedStatus));
        }

        public void ClickOnTrashIcon()
        {
            UpdateStep($"Removing selected channels in the distribution page");
            _browserHelper.WaitForElement(() => trashIcon, nameof(trashIcon));
            _browserHelper.Click(trashIcon, nameof(trashIcon));
        }

        public void ClickOnYesBtn()
        {
            UpdateStep($"Clicking on the yes button in the confirm Removal popup message");
            _browserHelper.WaitForElement(() => yesBtn, nameof(yesBtn));
            _browserHelper.Click(yesBtn, nameof(yesBtn));
        }

        public void UnpublishPost()
        {
            UpdateStep($"Unpublishing post");
            _browserHelper.WaitUntillTrue(() => selectedChannels.ToList().Count() > 0);
            var selectedNum = selectedChannels.ToList().Count();
            selectedChannels.ToList()[0].Click();
            ClickOnTrashIcon();
            ClickOnYesBtn();
            _browserHelper.WaitUntillTrue(() => selectedChannels.ToList().Count() == selectedNum - 1);
        }

        public void ClickOnOpenLink()
        {
            UpdateStep($"Clicking on open link");
            _browserHelper.WaitForElement(() => openLink,nameof(openLink));
            _browserHelper.Click(openLink, nameof(openLink));
        }

        public void ClickOnEditLink()
        {
            UpdateStep($"Clicking on edit link");
            _browserHelper.WaitForElement(() => editLink, nameof(editLink));
            _browserHelper.Click(editLink, nameof(editLink));
        }

        public void ClickOnNewButton()
        {
            UpdateStep($"Clicking on new button in the distribution page");
            _browserHelper.WaitForElement(() => FromPublishedToNewButton, nameof(FromPublishedToNewButton));
            _browserHelper.Click(FromPublishedToNewButton, nameof(FromPublishedToNewButton));
        }

        public string GetTitleChannel()
        {
            UpdateStep($"Getting the title of the selected channel");
            _browserHelper.WaitForElement(() => selectedChannels.FirstOrDefault(), "channel");
            return selectedChannels.FirstOrDefault().Text;
        }

        public bool ValidateChannelLanguage(string language)
        {
            string channelText = GetTitleChannel();
            UpdateStep($"Validating channel language is {language}");
            return channelText.Split(' ').Last() == language;
        }
    }
}