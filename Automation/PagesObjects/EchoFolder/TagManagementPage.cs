using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using MongoDB.Bson;
using NUnit.Framework;
using OpenQA.Selenium;


namespace Automation.PagesObjects.EchoFolder
{
    public class TagManagementPage : BaseObject
    {

        IWebElement TagsIcon => _browserHelper.FindElement(By.XPath("(//div[@class='sideNavContainer']//div//span)[2]"), "Tags");

        IWebElement LangDropDown => FindElement(".ui.dropdown.distinct.selection");

        IWebElement LangDropDownOpen => FindElement(".menu.transition.visible");

        List<IWebElement> LanungeFilterList => FindElements("[role='option']");

        IWebElement CreateTagBtn => FindElement(".ui.button.secondary");

        IWebElement CreateTagWindow => FindElement(".ui.grey.basic.segment");

        IWebElement TagNameField => FindElement("[placeholder='Tag Name']");

        IWebElement SynonymsField => FindElement(".synonyms-input input");

        IWebElement DoneBtn => FindElement(".ui.button.primary.medium");

        IWebElement PopUpMessage => FindElement(".ui.black.basic.segment");

        IWebElement SearchField => FindElement(".ui.icon.input input");

        IWebElement SelectedTag => FindElement(".ui.label.chip");

        IWebElement NoTagsFoundMsg => FindElement(".ui.brown.basic.segment");

        IWebElement RemoveTagBtn => _browserHelper.FindElement(By.XPath("//span[contains(text(), 'REMOVE TAG')]"), "RemoveBtn");

        IWebElement RemoveTagPopUpMsg => FindElement(".ui.tiny.modal.transition.visible.active");

        IWebElement YesBtn_RemoveTagPopUp => _browserHelper.FindElement(By.XPath("//span[contains(text(), 'Yes')]"), "YesBtn");

        public TagManagementPage(Browser browser)
            :base(browser)
        {
        }

        public void ClickOnTags()
        {
            _browserHelper.WaitForElement(() => TagsIcon, nameof(TagsIcon));
            _browserHelper.Click(TagsIcon, nameof(TagsIcon));
        }

        public bool ValidateDropDown()
        {
            return _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
        }

        public void ClickOnLangBtn()
        {
            _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
            _browserHelper.Click(LangDropDown, nameof(LangDropDown));
        }

        public List<string> GetLangList()
        {
            _browserHelper.WaitUntillTrue(() => LanungeFilterList.Count > 0);
            return LanungeFilterList.Select(l => l.Text).ToList(); 
        }

        public bool ValidateLangList(BsonArray langnuges)
        {
            _browserHelper.WaitForElement(() => LangDropDownOpen, nameof(LangDropDownOpen));
           var actualLangList = GetLangList();
            return langnuges.Select(e => e.ToString()).SequenceEqual(actualLangList);
        }

        public bool ValidateCurrntLang(string langnuge)
        {
            _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
            var actualDefaultLang = LangDropDown.Text;
            return langnuge == actualDefaultLang;
        }

        public void ClickOnCreateBtn()
        {
            _browserHelper.WaitForElement(() => CreateTagBtn, nameof(CreateTagBtn));
            _browserHelper.Click(CreateTagBtn,nameof(CreateTagBtn));
        }

        public string FillTheTagName()
        {
            _browserHelper.WaitForElement(() => CreateTagWindow, nameof(CreateTagWindow));
            string tag = "NewTag" + new Random().Next(1,999999);
            _browserHelper.SetText(TagNameField,tag);
            return tag;
        }

        public void InsertSynonyms()
        {
            _browserHelper.WaitForElement(() => SynonymsField, nameof(SynonymsField));
            _browserHelper.Click(SynonymsField, nameof(SynonymsField));
            _browserHelper.SetText(SynonymsField, "NewSyn");
            SynonymsField.SendKeys(Keys.Enter);
        }

        public void ClickOnDoneBtn()
        {
            _browserHelper.WaitForElement(() => DoneBtn, nameof(DoneBtn));
            _browserHelper.Click(DoneBtn,nameof(DoneBtn));
        }

        public string GetPopUpText()
        {
            _browserHelper.WaitForElement(() => PopUpMessage, nameof(PopUpMessage));
            var actualPopUpText = PopUpMessage.Text;
            return actualPopUpText;
        }

        public void SearchForTagName(string tag)
        {
            _browserHelper.WaitForElement(() => SearchField, nameof(SearchField));
            _browserHelper.Click(SearchField,nameof(SearchField));
            _browserHelper.SetText(SearchField, tag);
        }

        public string GetTagText()
        {
            _browserHelper.WaitForElement(() => SelectedTag, nameof(SelectedTag));
            var actualTagName = SelectedTag.Text;
            return actualTagName;
        }

        public string GetNoTagsFoundMsg()
        {
            _browserHelper.WaitForElement(() => NoTagsFoundMsg, nameof(NoTagsFoundMsg));
            var actualMsg = NoTagsFoundMsg.Text;
            return actualMsg;
        }

        public void ClickOnTag(string tag)
        {
            _browserHelper.WaitForElement(() => SearchField, nameof(SearchField));
            _browserHelper.Click(SearchField, nameof(SearchField));
            _browserHelper.SetText(SearchField, tag);
            _browserHelper.Click(SelectedTag, nameof(SelectedTag));
        }

        public void ClickOnTagNameField()
        {
            _browserHelper.WaitForElement(() => CreateTagWindow, nameof(CreateTagWindow));
            _browserHelper.Click(TagNameField, nameof(TagNameField));
        }

        public void EditTag()
        {
            ClickOnTagNameField();
            _browserHelper.SetText(TagNameField,"3");
        }

        public void ClickOnRemoveTagBtn()
        {
            _browserHelper.WaitForElement(() => CreateTagWindow,nameof(CreateTagWindow));
            _browserHelper.Click(RemoveTagBtn,nameof(RemoveTagBtn));
        }

        public void ClickOnYesBtn()
        {
            _browserHelper.WaitForElement(() => RemoveTagPopUpMsg,nameof(RemoveTagPopUpMsg));
            _browserHelper.Click(YesBtn_RemoveTagPopUp,nameof(YesBtn_RemoveTagPopUp));
        }

        public void RemoveTag(string tag)
        {
            ClickOnTag(tag);
            ClickOnRemoveTagBtn();
            ClickOnYesBtn();
        }

        public void ClickOnSelectedTag()
        {
            _browserHelper.WaitForElement(() => SelectedTag,nameof(SelectedTag));
            _browserHelper.Click(SelectedTag,nameof(SelectedTag));
        }
    }
}