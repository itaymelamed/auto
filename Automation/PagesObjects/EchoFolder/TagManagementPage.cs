using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using MongoDB.Bson;
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

        IWebElement RemoveTagBtn => _browserHelper.FindElement(By.XPath("//button [contains(text(), 'REMOVE TAG')]"), "RemoveBtn");

        IWebElement RemoveTagPopUpMsg => FindElement(".ui.tiny.modal.transition.visible.active");

        IWebElement YesBtn_RemoveTagPopUp => _browserHelper.FindElement(By.XPath("//button[contains(text(), 'Yes')]"), "YesBtn");

        IWebElement RemoveTagSuccessMsg => FindElement(".ui.black.basic.segment");

        IWebElement RemoveSynBtn => FindElement(".react-tagsinput-remove");

        IWebElement SynIcon => FindElement(".react-tagsinput-tag");


        public TagManagementPage(Browser browser)
            :base(browser)
        {
        }

        public void ClickOnTags()
        {
            UpdateStep($"Clicking on for Tags.");

            _browserHelper.WaitForElement(() => TagsIcon, nameof(TagsIcon));
            _browserHelper.Click(TagsIcon, nameof(TagsIcon));
        }

        public bool ValidateDropDown()
        {
            UpdateStep($"Validating that the Lanuage Drop Down appears.");
            return _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
        }

        public void ClickOnLangBtn()
        {
            UpdateStep($"Clicking on Lang Drop Down button.");
            _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
            _browserHelper.Click(LangDropDown, nameof(LangDropDown));
        }

        public List<string> GetLangList()
        {
            UpdateStep($"Getting the Lang Drop list.");
            _browserHelper.WaitUntillTrue(() => LanungeFilterList.Count > 0);
            return LanungeFilterList.Select(l => l.Text).ToList(); 
        }

        public bool ValidateLangList(BsonArray langnuges)
        {
            UpdateStep($"Validating the Lang Drop list.");
            _browserHelper.WaitForElement(() => LangDropDownOpen, nameof(LangDropDownOpen));
           var actualLangList = GetLangList();
            return langnuges.Select(e => e.ToString()).SequenceEqual(actualLangList);
        }

        public bool ValidateCurrntLang(string langnuge)
        {
            UpdateStep($"Validating the current lang.");
            _browserHelper.WaitForElement(() => LangDropDown, nameof(LangDropDown));
            var actualDefaultLang = LangDropDown.Text;
            return langnuge == actualDefaultLang;
        }

        public void ClickOnCreateBtn()
        {
            UpdateStep($"Clicking on create btn.");
            _browserHelper.WaitForElement(() => CreateTagBtn, nameof(CreateTagBtn));
            _browserHelper.Click(CreateTagBtn,nameof(CreateTagBtn));
        }

        public string FillTheTagName()
        {
            UpdateStep($"Filling the tag name.");
            _browserHelper.WaitForElement(() => CreateTagWindow, nameof(CreateTagWindow));
            string tag = "NewTag" + new Random().Next(1,999999);
            _browserHelper.SetText(TagNameField, tag);
            return tag;
        }

        public void InsertSynonyms()
        {
            UpdateStep($"Inserting synonyms.");
            _browserHelper.WaitForElement(() => SynonymsField, nameof(SynonymsField));
            _browserHelper.Click(SynonymsField, nameof(SynonymsField));
            _browserHelper.SetText(SynonymsField, "NewSyn");
            SynonymsField.SendKeys(Keys.Enter);
        }

        public void ClickOnDoneBtn()
        {
            UpdateStep($"Clicking on done button.");
            _browserHelper.WaitForElement(() => DoneBtn, nameof(DoneBtn));
            _browserHelper.Click(DoneBtn, nameof(DoneBtn));
        }

        public string GetPopUpText()
        {
            UpdateStep($"Getting the popup text.");
            _browserHelper.WaitForElement(() => PopUpMessage, nameof(PopUpMessage));
            var actualPopUpText = PopUpMessage.Text;
            return actualPopUpText;
        }

        public void SearchForTagName(string tag)
        {
            UpdateStep($"Searching for tag: {tag}.");
            _browserHelper.WaitForElement(() => SearchField, nameof(SearchField));
            _browserHelper.Click(SearchField, nameof(SearchField));
            _browserHelper.SetText(SearchField, tag);
        }

        public string GetTagText()
        {
            UpdateStep($"Getting the tag text.");
            _browserHelper.WaitForElement(() => SelectedTag, nameof(SelectedTag));
            var actualTagName = SelectedTag.Text;
            return actualTagName;
        }

        public string GetNoTagsFoundMsg()
        {
            UpdateStep($"Getting the no Tags was found message.");
            _browserHelper.WaitForElement(() => NoTagsFoundMsg, nameof(NoTagsFoundMsg));
            var actualMsg = NoTagsFoundMsg.Text;
            return actualMsg;
        }

        public void ClickOnTag(string tag)
        {
            UpdateStep($"Clicking on Tag.");
            _browserHelper.WaitForElement(() => SearchField, nameof(SearchField));
            _browserHelper.Click(SearchField, nameof(SearchField));
            _browserHelper.SetText(SearchField, tag);
            _browserHelper.Click(SelectedTag, nameof(SelectedTag));
        }

        public void ClickOnTagNameField()
        {
            UpdateStep($"Clicking on Tag name field.");

            _browserHelper.WaitForElement(() => CreateTagWindow, nameof(CreateTagWindow));
            _browserHelper.Click(TagNameField, nameof(TagNameField));
        }

        public void EditTag()
        {
            UpdateStep($"Editing the name of the Tag.");

            ClickOnTagNameField();
            _browserHelper.SetText(TagNameField,"3");
        }

        public void ClickOnRemoveTagBtn()
        {
            UpdateStep($"Clicking on remove button.");

            _browserHelper.WaitForElement(() => CreateTagWindow,nameof(CreateTagWindow));
            _browserHelper.Click(RemoveTagBtn, nameof(RemoveTagBtn));
        }

        public void ClickOnYesBtn()
        {
            UpdateStep($"Clicking on Yes button.");

            _browserHelper.WaitForElement(() => RemoveTagPopUpMsg, nameof(RemoveTagPopUpMsg));
            _browserHelper.Click(YesBtn_RemoveTagPopUp, nameof(YesBtn_RemoveTagPopUp));
        }
       
        public void ClickOnSelectedTag()
        {
            UpdateStep($"Clicking on Selected tag.");

            _browserHelper.WaitForElement(() => SelectedTag,nameof(SelectedTag));
            _browserHelper.Click(SelectedTag, nameof(SelectedTag));
        }

        public bool VliadateTagRemoved()
        {
            UpdateStep($"Validting that the tag removed.");

            _browserHelper.WaitForElement(() => RemoveTagSuccessMsg, nameof(RemoveTagSuccessMsg));
            return _browserHelper.WaitForElementDiss(() => SelectedTag) ;
        }

        public void ClickOnRemoveSyn()
        {
            UpdateStep($"Clicking on remove synonyms button.");

            _browserHelper.WaitForElement(() => SynIcon, nameof(SynIcon));
             _browserHelper.Click(RemoveSynBtn,nameof(RemoveSynBtn));
        }

        public bool ValidateSynRemoved()
        {
            UpdateStep($"Clicking on remove synonyms button.");

            _browserHelper.WaitForElement(() => CreateTagWindow, nameof(CreateTagWindow));
            return  _browserHelper.WaitForElementDiss(()=>SynIcon);
        }
    }
}