using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using MongoDB.Bson;

namespace Automation.PagesObjects
{
    public class ArticleBase : BaseObject
    {
        IWebElement titleTextBox => FindElement("[data-model] [name=title]");

        protected IWebElement editorMedia => FindElement(".span15.right-container .left.media.drop.old-app");

        IWebElement editorSeo => FindElement("[data-view=EditorSeo] [name=description]");

        IWebElement editorTags => FindElement(".tags-container .input");

        IWebElement editorWysiWyg => FindElement(".redactor_toolbar");

        IWebElement images => FindElement(".search-image__search-results-list");

        IWebElement descTxtBox => FindElement(".redactor_editor");

        IWebElement previewBtn => FindElement(".orange.publish");

        IWebElement youtubeIcon => FindElement(".search-tabs .icon-video-icon");

        IWebElement youtubeLinkTxtBox => FindElement(".search-video .query");

        IWebElement youtubeSearchBtn => FindElement("[data-view=EditorSearchVideo] button .ficon.icon-search");

        IWebElement youtubeSearchResult => FindElement(".video-thumb img");

        List<IWebElement> dropMedia => FindElements(".redactor_editor p");

        IWebElement youtubeVideoSaveBtn => FindElement(".btn.save");

        IWebElement youtubeVideoInPost => FindElement(".ytp-cued-thumbnail-overlay-image");

        List<IWebElement> tags => FindElements(".tags-container ul li");

        IWebElement autoComplete => FindElement("#autocomplete");

        IWebElement magicStick => FindElement(".icon-magic-stick");

        List<IWebElement> autoCompleteRows => FindElements(".autocomplete-row");

        IWebElement containerImage => FindElement(".image-container__image");

        IWebElement deleteBtnContainerImage => FindElement("[title='DELETE']");

        IWebElement imageSearchBox => FindElement("[style='display: block;'] .query");

        IWebElement searchImageBtn => FindElement("[style='display: block;'] .search .ficon");

        IWebElement playBuzzCheckBox => FindElement("[for='playbuzz_enabled']");

        IWebElement playBuzzUrlField => FindElement(".playbuzz-url");

        IWebElement playBuzzImage => FindElement(".game_image_wrapper img");

        protected List<IWebElement> imagesResults => FindElements(".search-results li");

        protected IWebElement coverUpload => FindElement("[data-type='Cover'] .upload");

        public ArticleBase(Browser browser)
            :base(browser)
        {
        }

        public void WriteTitle(string title)
        {
            Base.MongoDb.UpdateSteps($"Writing titile {title}.");

            _browserHelper.WaitForElement(() => titleTextBox, nameof(titleTextBox));
            Thread.Sleep(1000);
            titleTextBox.Clear();
            _browserHelper.SetText(titleTextBox, title);
        }

        public virtual CropImagePopUp DragImage(int imageIndex)
        {
            if (_browserHelper.WaitUntillTrue(() =>
             {
                 Base.MongoDb.UpdateSteps($"Dragging image number {imageIndex}.");
                 _browserHelper.WaitForElement(() => editorMedia, nameof(editorMedia), 60);
                 _browserHelper.WaitUntillTrue(() => imagesResults.ToList().Count() == 30);
                 Thread.Sleep(2000);
                 var image = imagesResults.ToList()[imageIndex];
                 Thread.Sleep(2000);
                _browserHelper.WaitForElement(() => image);
                 _browserHelper.DragElement(image, editorMedia);
                 return true;
             }))
                return new CropImagePopUp(_browser);
            throw new NUnit.Framework.AssertionException("Failed to drag images.");
        }

        public void WriteDec(string desc)
        {
            Base.MongoDb.UpdateSteps($"Writing a description.");

            _browserHelper.WaitForElement(() => descTxtBox, nameof(descTxtBox));
            descTxtBox.SendKeys(desc);
        }

        public PreviewPage ClickOnPreviewBtn()
        {
            _driver.SwitchTo().DefaultContent();
            _browserHelper.WaitForElement(() => previewBtn, nameof(previewBtn));
            Base.MongoDb.UpdateSteps($"Clicking on Preview Button.");
            _browserHelper.ClickJavaScript(previewBtn);

            return new PreviewPage(_browser);
        }

        public void AddYoutubeVideo(string link, int par)
        {
            Base.MongoDb.UpdateSteps($"Clicking on Youtube icon.");
            _browserHelper.WaitForElement(() => youtubeIcon, nameof(youtubeIcon));
            youtubeIcon.Click();

            Base.MongoDb.UpdateSteps($"Inserting Youtube link: {link}.");
            _browserHelper.WaitForElement(() => youtubeLinkTxtBox, nameof(youtubeLinkTxtBox));
            youtubeLinkTxtBox.SendKeys(link);

            Base.MongoDb.UpdateSteps($"Clicking on search button.");
            _browserHelper.WaitForElement(() => youtubeSearchBtn, nameof(youtubeSearchBtn));
            youtubeSearchBtn.Click();

            _browserHelper.WaitForElement(() => youtubeSearchResult, nameof(youtubeSearchResult));

            Base.MongoDb.UpdateSteps($"Dragging a Youtube video..");
            _browserHelper.DragElement(youtubeSearchResult, dropMedia[par]);

            Base.MongoDb.UpdateSteps($"Clicking on save button.");
            _browserHelper.WaitForElement(() => youtubeVideoSaveBtn, nameof(youtubeVideoSaveBtn));
            _browserHelper.Click(youtubeVideoSaveBtn, nameof(youtubeVideoSaveBtn));

            _browserHelper.WaitForElementDiss(() => youtubeVideoSaveBtn);
        }

        public bool CheckYoutubeVideoInPost()
        {
            Base.MongoDb.UpdateSteps($"Clicking on save button.");
            _driver.SwitchTo().Frame(_driver.FindElement(By.XPath("//iframe[contains(@class, 'Youtube')]")));
            return _browserHelper.WaitForElement(() => youtubeVideoInPost, nameof(youtubeVideoInPost));
        }

        public bool ValidateTitle()
        {
            Base.MongoDb.UpdateSteps("Validating title field.");
            return _browserHelper.WaitForElement(() => titleTextBox, nameof(titleTextBox), 60, false);
        }

        public bool ValidateEditorMedia()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(() => editorMedia, nameof(editorMedia), 60, false);
        }

        public bool ValidateEditorSeo()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(() => editorSeo, nameof(editorSeo), 60, false);
        }

        public bool ValidateEditorTags()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(() => editorTags, nameof(editorTags), 60, false);
        }

        public bool ValidateEditorWysiWyg()
        {
            Base.MongoDb.UpdateSteps("Validating editor media field.");
            return _browserHelper.WaitForElement(() => editorWysiWyg, nameof(editorWysiWyg), 60, false);
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
            _browserHelper.WaitForElement(() => titleTextBox, nameof(titleTextBox), 60, true);
            return titleTextBox.GetAttribute("value");
        }

        public string GetBodyValue()
        {
            Base.MongoDb.UpdateSteps("Getting body text box value.");
            _browserHelper.WaitForElement(() => descTxtBox, nameof(descTxtBox), 60, true);
            return descTxtBox.Text;
        }

        public void WriteTags(BsonArray tagsArray)
        {
            List<string> tagsList = tagsArray.ToList().Select(t => t.ToString()).ToList();
            Base.MongoDb.UpdateSteps("Inserting Tags in tags text field.");

            _browserHelper.WaitForElement(() => editorTags, nameof(editorTags), 60, true);
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
            _browserHelper.WaitForElement(() => editorTags, nameof(editorTags), 60, true);
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
            _browserHelper.WaitForElement(() => autoComplete, nameof(autoComplete));
            Thread.Sleep(1000);
            List<string> tagsList = autoCompleteRows.Select(t => t.Text).ToList();

            return tagsList.Any(x => x == tag);
        }

        public void ClickOnMagicStick(int num = 1)
        {
            Base.MongoDb.UpdateSteps($"Clicking on magic stick button.");
            _browserHelper.WaitForElement(() => magicStick, nameof(magicStick));
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
            return _browserHelper.WaitForElement(() => containerImage, nameof(containerImage));
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
               return _browserHelper.WaitForElement(() => deleteBtnContainerImage, nameof(deleteBtnContainerImage));
           });
        }

        public void SearchImage(string search)
        {
            Base.MongoDb.UpdateSteps($"Inserting {search} in image search text box.");
            _browserHelper.WaitForElement(() => imageSearchBox, nameof(imageSearchBox));
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
            _browserHelper.WaitForElement(() => playBuzzCheckBox, nameof(playBuzzCheckBox));
            _browserHelper.Click(playBuzzCheckBox, nameof(playBuzzCheckBox));
        }

        public bool ValidatePlayBuzzTBXEnabled()
        {
            Base.MongoDb.UpdateSteps("Validating PlayBuzz TBX Enabled");
            _browserHelper.WaitForElement(() => playBuzzUrlField, nameof(playBuzzUrlField));
            return playBuzzUrlField.Enabled;
        }

        public void SetPlayBuzzURL(string url)
        {
            Base.MongoDb.UpdateSteps("Inserting Play Buzz URL");
            _browserHelper.WaitForElement(() => playBuzzUrlField, nameof(playBuzzUrlField));
            _browserHelper.SetText(playBuzzUrlField, url);
        }

        public bool ValidatePlayBuzzImageAppears()
        {
            Base.MongoDb.UpdateSteps("Validating PlayBuzz Image Appears");
            _browserHelper.ExecuteUntill(() => _browserHelper.MoveToIframe("pb_feed_iframe"));
            return _browserHelper.WaitForElement(() => playBuzzImage, nameof(playBuzzImage));
        }

        public void SetSeoDesc()
        {
            _browserHelper.WaitForElement(() => editorSeo, nameof(editorSeo));
            _browserHelper.SetText(editorSeo, "text text text");
            _browserHelper.WaitForElement(() => editorSeo,nameof(editorSeo));
            _browserHelper.SetText(editorSeo,"text text text");
        }


        public void FillArticleTemplate()
        {
            Base.MongoDb.UpdateSteps("Fill the article template");
            WriteTitle("EI Full Flow");
            SearchImage("cat");
            CropImagePopUp cropImagePopUp = DragImage(0);
            cropImagePopUp.ClickOnCropImageBtn();
            cropImagePopUp.ClickOnEditokBtn();
            WriteDec("kashkosh");
            WriteTags(new BsonArray{ "test1", "test2", "test3" });
            PreviewPage previewPage = ClickOnPreviewBtn();
            PostPage postPage = previewPage.ClickOnPublishBtn();
        }
    }
}