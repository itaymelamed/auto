using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using MongoDB.Bson;

namespace Automation.PagesObjects
{
    public class ArticleBase
    {
        [FindsBy(How = How.CssSelector, Using = "[data-model] [name=title]")]
        IWebElement titleTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".span15.right-container .left.media.drop.old-app")]
        protected IWebElement editorMedia { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-view=EditorSeo] [name=description]")]
        IWebElement editorSeo { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".tags-container .input")]
        IWebElement editorTags { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".redactor_toolbar")]
        IWebElement editorWysiWyg { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".search-image__search-results-list")]
        IWebElement images { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".redactor_editor")]
        IWebElement descTxtBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".orange.publish")]
        IWebElement previewBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".search-tabs .icon-video-icon")]
        IWebElement youtubeIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".search-video .query")]
        IWebElement youtubeLinkTxtBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-view=EditorSearchVideo] button .ficon.icon-search")]
        IWebElement youtubeSearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".video-thumb img")]
        IWebElement youtubeSearchResult { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".redactor_editor p")]
        IList<IWebElement> dropMedia { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".btn.save")]
        IWebElement youtubeVideoSaveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ytp-cued-thumbnail-overlay-image")]
        IWebElement youtubeVideoInPost { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".tags-container ul li")]
        IList<IWebElement> tags { get; set; }

        [FindsBy(How = How.Id, Using = "autocomplete")]
        IWebElement autoComplete { get; set; }

        [FindsBy(How = How.ClassName, Using = "icon-magic-stick")]
        IWebElement magicStick { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".autocomplete-row")]
        IList<IWebElement> autoCompleteRows { get; set; }

        [FindsBy(How = How.ClassName, Using = "image-container__image")]
        IWebElement containerImage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[title='DELETE']")]
        IWebElement deleteBtnContainerImage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[style='display: block;'] .query")]
        IWebElement imageSearchBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[style='display: block;'] .search .ficon")]
        IWebElement searchImageBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[for='playbuzz_enabled']")]
        IWebElement playBuzzCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".playbuzz-url")]
        IWebElement playBuzzUrlField { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".game_image_wrapper img")]
        IWebElement playBuzzImage { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".search-results li")]
        protected IList<IWebElement> imagesResults { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[data-type='Cover'] .upload")]
        protected IWebElement coverUpload { get; set; }

        protected Browser _browser;
        protected IWebDriver _driver;
        protected BrowserHelper _browserHelper;

        public ArticleBase(Browser browser)
        {
            _browser = browser;
            _driver = browser.Driver;
            _browserHelper = browser.BrowserHelper;
            PageFactory.InitElements(_driver, this);
        }

        public void WriteTitle(string title)
        {
            Base.MongoDb.UpdateSteps($"Writing titile {title}.");

            _browserHelper.WaitForElement(titleTextBox, nameof(titleTextBox));
            Thread.Sleep(1000);
            titleTextBox.Clear();
            _browserHelper.SetText(titleTextBox, title);
        }

        public virtual CropImagePopUp DragImage(int imageIndex)
        {
            if (_browserHelper.WaitUntillTrue(() =>
             {
                 Base.MongoDb.UpdateSteps($"Dragging image number {imageIndex}.");
                 _browserHelper.WaitForElement(editorMedia, nameof(editorMedia), 60);
                 _browserHelper.WaitUntillTrue(() => imagesResults.ToList().Count() == 30);
                 Thread.Sleep(2000);
                 var image = imagesResults.ToList()[imageIndex];
                 Thread.Sleep(2000);
                 _browserHelper.DragElement(image, editorMedia);
                 return true;
             }))
                return new CropImagePopUp(_browser);
            throw new NUnit.Framework.AssertionException("Failed to drag images.");
        }

        public void WriteDec(string desc)
        {
            Base.MongoDb.UpdateSteps($"Writing a description.");

            _browserHelper.WaitForElement(descTxtBox, nameof(descTxtBox));
            descTxtBox.SendKeys(desc);
        }

        public PreviewPage ClickOnPreviewBtn()
        {
            _driver.SwitchTo().DefaultContent();
            _browserHelper.WaitForElement(previewBtn, nameof(previewBtn));
            Base.MongoDb.UpdateSteps($"Clicking on Preview Button.");
            _browserHelper.ClickJavaScript(previewBtn);

            return new PreviewPage(_browser);
        }

        public void AddYoutubeVideo(string link, int par)
        {
            Base.MongoDb.UpdateSteps($"Clicking on Youtube icon.");
            _browserHelper.WaitForElement(youtubeIcon, nameof(youtubeIcon));
            youtubeIcon.Click();

            Base.MongoDb.UpdateSteps($"Inserting Youtube link: {link}.");
            _browserHelper.WaitForElement(youtubeLinkTxtBox, nameof(youtubeLinkTxtBox));
            youtubeLinkTxtBox.SendKeys(link);

            Base.MongoDb.UpdateSteps($"Clicking on search button.");
            _browserHelper.WaitForElement(youtubeSearchBtn, nameof(youtubeSearchBtn));
            youtubeSearchBtn.Click();

            _browserHelper.WaitForElement(youtubeSearchResult, nameof(youtubeSearchResult));

            Base.MongoDb.UpdateSteps($"Dragging a Youtube video..");
            _browserHelper.DragElement(youtubeSearchResult, dropMedia[par]);

            Base.MongoDb.UpdateSteps($"Clicking on save button.");
            _browserHelper.WaitForElement(youtubeVideoSaveBtn, nameof(youtubeVideoSaveBtn));
            _browserHelper.Click(youtubeVideoSaveBtn, nameof(youtubeVideoSaveBtn));

            _browserHelper.WaitForElementDiss(youtubeVideoSaveBtn);
        }

        public bool CheckYoutubeVideoInPost()
        {
            Base.MongoDb.UpdateSteps($"Clicking on save button.");
            _driver.SwitchTo().Frame(_driver.FindElement(By.XPath("//iframe[contains(@class, 'Youtube')]")));
            return _browserHelper.WaitForElement(youtubeVideoInPost, nameof(youtubeVideoInPost));
        }

        public bool ValidateTitle()
        {
            Base.MongoDb.UpdateSteps("Validating title field.");
            return _browserHelper.WaitForElement(titleTextBox, nameof(titleTextBox), 60, false);
        }

        public bool ValidateEditorMedia()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(editorMedia, nameof(editorMedia), 60, false);
        }

        public bool ValidateEditorSeo()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(editorSeo, nameof(editorSeo), 60, false);
        }

        public bool ValidateEditorTags()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(editorTags, nameof(editorTags), 60, false);
        }

        public bool ValidateEditorWysiWyg()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(editorWysiWyg, nameof(editorWysiWyg), 60, false);
        }

        public string ValidateFildes()
        {
            var error = string.Empty;

            error += ValidateTitle() ? "" : "Title is missing." + Environment.NewLine;
            error += ValidateEditorMedia() ? "" : "Editor media is missing." + Environment.NewLine;
            error += ValidateEditorSeo() ? "" : "Editor Seo is missing." + Environment.NewLine;
            error += ValidateEditorTags() ? "" : "Editor Tags is missing." + Environment.NewLine;
            error += ValidateEditorWysiWyg() ? "" : "Editor WysiWyg is missing.";

            return error;
        }

        public string GetTitleValue()
        {
            Base.MongoDb.UpdateSteps("Getting Title textbox value.");
            _browserHelper.WaitForElement(titleTextBox, nameof(titleTextBox), 60, true);
            return titleTextBox.GetAttribute("value");
        }

        public string GetBodyValue()
        {
            Base.MongoDb.UpdateSteps("Getting body text box value.");
            _browserHelper.WaitForElement(descTxtBox, nameof(descTxtBox), 60, true);
            return descTxtBox.Text;
        }

        public void WriteTags(BsonArray tagsArray)
        {
            List<string> tagsList = tagsArray.ToList().Select(t => t.ToString()).ToList();
            Base.MongoDb.UpdateSteps("Inserting Tags in tags text field.");

            _browserHelper.WaitForElement(editorTags, nameof(editorTags), 60, true);
            _browserHelper.MoveToEl(editorTags);
            _browserHelper.WaitUntillTrue(() =>
            {
                tagsList.ForEach(t =>
                {
                    editorTags.SendKeys(t);
                    Thread.Sleep(2000);
                    editorTags.SendKeys(Keys.Enter);
                });

                return GetTagsValue().Count >= tagsArray.Count;
            });
        }

        public void WriteShortTags(string shortTag)
        {
            Base.MongoDb.UpdateSteps("Inserting short Tags in tags text field.");
            _browserHelper.WaitForElement(editorTags, nameof(editorTags), 60, true);
            editorTags.SendKeys(shortTag);
        }

        public List<string> GetTagsValue()
        {
            Base.MongoDb.UpdateSteps("Getting tags value.");
            return tags.ToList().Select(t => t.GetAttribute("innerText")).ToList();
        }

        public bool ValidateAutoComplete(string tag)
        {
            Base.MongoDb.UpdateSteps($"Validating'{tag}' is found on the autocomplet box.");
            _browserHelper.WaitForElement(autoComplete, nameof(autoComplete));
            Thread.Sleep(1000);
            List<string> tagsList = autoCompleteRows.Select(t => t.Text).ToList();

            return tagsList.Any(x => x == tag);
        }

        public void ClickOnMagicStick(int num = 1)
        {
            Base.MongoDb.UpdateSteps($"Clicking on magic stick button.");
            _browserHelper.WaitForElement(magicStick, nameof(magicStick));
            for (int i = 0; i < num; i++)
            {
                _browserHelper.Click(magicStick, nameof(magicStick));
            }
            _browserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitUntillTrue(() =>
                {
                    WriteTags(new BsonArray(new List<string>() { "Atest", "BTest", "CTest" }));
                    return tags.ToList().Count() >= 3;
                });

                return GetTagsValue().Count() > 0;
            }, "Failed to add tags");
        }

        public bool ValidateContainerImage()
        {
            Base.MongoDb.UpdateSteps($"Validating the cover image container.");
            return _browserHelper.WaitForElement(containerImage, nameof(containerImage));
        }

        public void HoverOverCoverImage()
        {
            Base.MongoDb.UpdateSteps($"Hovering over the cover image");
            _browserHelper.Hover(containerImage);
        }

        public bool ValidateDeleteButtonCoverimage()
        {
            Base.MongoDb.UpdateSteps($"Validating the delete button on cover image.");
            return _browserHelper.WaitUntillTrue(() =>
           {
               HoverOverCoverImage();
               return _browserHelper.WaitForElement(deleteBtnContainerImage, nameof(deleteBtnContainerImage));
           });
        }

        public void SearchImage(string search)
        {
            Base.MongoDb.UpdateSteps($"Inserting {search} in image search text box.");
            _browserHelper.WaitForElement(imageSearchBox, nameof(imageSearchBox));
            _browserHelper.ExecuteUntill(() => imageSearchBox.Clear());
            _browserHelper.SetText(imageSearchBox, search);
            Base.MongoDb.UpdateSteps($"Clicking on image search button.");
            _browserHelper.Click(searchImageBtn, nameof(searchImageBtn));
        }

        public int ValidateImageSearchResults(int maxRes)
        {
            Base.MongoDb.UpdateSteps($"Validating Image Search Results.");
            _browserHelper.WaitUntillTrue(() => imagesResults.ToList().Count == 30, $"There were less than {maxRes} results.", 60, false);

            return imagesResults.ToList().Count();
        }

        public bool ValidateImageContenet(string search)
        {
            Base.MongoDb.UpdateSteps($"Validating the Image Search content.");

            _browserHelper.WaitUntillTrue(() =>
            {
                return imagesResults.ToList().Any(r => r.FindElement(By.XPath(".//span")).GetAttribute("data-url").Contains(search));
            }, $"No match found between search query and actual results.", 30);

            return true;
        }

        public bool ValidateEditMode()
        {
            _browserHelper.WaitUntillTrue(() => _browser.GetUrl().Contains("editor"), "User is not on edit page.");
            return true;
        }

        public void ClickOnPlayBuzzCBX()
        {
            Base.MongoDb.UpdateSteps("Clicking on PlayBuzz CheckBox");
            _browserHelper.WaitForElement(playBuzzCheckBox, nameof(playBuzzCheckBox));
            _browserHelper.Click(playBuzzCheckBox, nameof(playBuzzCheckBox));
        }

        public bool ValidatePlayBuzzTBXEnabled()
        {
            Base.MongoDb.UpdateSteps("Validating PlayBuzz TBX Enabled");
            _browserHelper.WaitForElement(playBuzzUrlField, nameof(playBuzzUrlField));
            return playBuzzUrlField.Enabled;
        }

        public void SetPlayBuzzURL(string url)
        {
            Base.MongoDb.UpdateSteps("Inserting Play Buzz URL");
            _browserHelper.WaitForElement(playBuzzUrlField, nameof(playBuzzUrlField));
            _browserHelper.SetText(playBuzzUrlField, url);
        }

        public bool ValidatePlayBuzzImageAppears()
        {
            Base.MongoDb.UpdateSteps("Validating PlayBuzz Image Appears");
            _browserHelper.ExecuteUntill(() => _browserHelper.MoveToIframe("pb_feed_iframe"));
            return _browserHelper.WaitForElement(playBuzzImage, nameof(playBuzzImage));
        }

        public void SetSeoDesc()
        {
            _browserHelper.WaitForElement(editorSeo, nameof(editorSeo));
            _browserHelper.SetText(editorSeo, "text text text");
        }
    }
}