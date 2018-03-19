using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using MongoDB.Bson;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class ListsTemplate : ArticleBase
    {
        [FindsBy(How = How.CssSelector, Using = ".introduction.fragment")]
        IWebElement introduction { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".image-container__image")]
        IList<IWebElement> images { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".public-DraftEditor-content")]
        IList<IWebElement> bodyTextBoxs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".title-wrapper input")]
        IList<IWebElement> subTitleFields { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".image-upload-drop-target")]
        IList<IWebElement> mediaDropBoxs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".ascending")]
        IWebElement ascendingIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".descending")]
        IWebElement descendingIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".count")]
        IList<IWebElement> counters { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".redactor-element.redactor_editor")]
        IList<IWebElement> bodyTextBoxs2 { get; set; }

        public ListsTemplate(Browser browser) :
            base(browser)
        {

        }

        public string GetIntroductionValue()
        {
            BaseUi.MongoDb.UpdateSteps("Getting introduction value");
            _browserHelper.WaitForElement(introduction, nameof(introduction));
            return introduction.Text;
        }

        public List<string> GetImagesUrl()
        {
            Thread.Sleep(2000);
            BaseUi.MongoDb.UpdateSteps("Getting images URL.");
            List<string> imagesSrcs = new List<string>();
            images.ToList().ForEach(p =>
            {
                imagesSrcs.Add(p.GetAttribute("src"));
            });

            return imagesSrcs;
        }

        public void SetBodyTextBoxs(string text)
        {
            Base.MongoDb.UpdateSteps("Inserting body text boxs");
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() >= 4);
            bodyTextBoxs.ToList().ForEach(x => _browserHelper.SetText(x, text));
        }

        public void SetBodyTextBoxsMmNews(string text)
        {
            Base.MongoDb.UpdateSteps("Inserting body text boxs");
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs2.ToList().Count() >= 4);
            bodyTextBoxs2.ToList().ForEach(x => _browserHelper.SetText(x, text));
        }

        public List<string> GetBodyTextBoxesValue()
        {
            BaseUi.MongoDb.UpdateSteps("Inserting body text boxes value");
            List<string> values = new List<string>();
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() == 5);
            bodyTextBoxs.ToList().ForEach(t => values.Add(t.Text));

            return values;
        }

        public string ValidateBodyTextBoxes(List<string> acValues, string text)
        {
            BaseUi.MongoDb.UpdateSteps("Validating body text boxes.");

            var errors = string.Empty;
            if (acValues.All((v => v == text)))
                return errors;

            acValues.Where(v => v != text).ToList().ForEach(x => errors += $"{x} {Environment.NewLine}");

            return errors;
        }

        public void SetSubTitles(BsonArray titles)
        {
           BaseUi.MongoDb.UpdateSteps("Inserting subtitle values.");
           List<string> titlesList = titles.ToList().Select(t => t.ToString()).ToList();
           var xxx = subTitleFields.ToList().Count();
           _browserHelper.WaitUntillTrue(() => subTitleFields.ToList().Count() == 3);
           int i = 0;
           subTitleFields.ToList().ForEach(s =>
           {
               _browserHelper.SetText(s, titlesList[i]);
               i++;
           });
        }

        public List<string> GetSubTitelsValues()
        {
            BaseUi.MongoDb.UpdateSteps("Getting subtitle values.");
            List<string> values = new List<string>();
            _browserHelper.WaitUntillTrue(() => subTitleFields.ToList().Count() == 3);
            subTitleFields.ToList().ForEach(t => values.Add(t.GetAttribute("value")));

            return values;
        }

        public bool ValidateSubTitlesFields(List<string> acValues, BsonArray exValues)
        {
            BaseUi.MongoDb.UpdateSteps("Validating subtitles fields.");
            List<string> titlesList = exValues.ToList().Select(t => t.ToString()).ToList();
            return acValues.SequenceEqual(titlesList);
        }

        public void DragImages()
        {
            BaseUi.MongoDb.UpdateSteps("Dragging images to media drop boxes.");
            Thread.Sleep(2000);
            _browserHelper.WaitUntillTrue(() => mediaDropBoxs.ToList().Count() == 4, "Media boxes failed to load");
            _browserHelper.WaitUntillTrue(() => imagesResults.ToList().Count() == 30, "There were no 30 results images.");

            _browserHelper.WaitUntillTrue(() =>
            {
                mediaDropBoxs.ToList().ForEach(m =>
                {
                    _browserHelper.DragElement(imagesResults.FirstOrDefault(), m);
                    CropImagePopUp cropImagePopUp = new CropImagePopUp(_browser);
                    cropImagePopUp.ClickOnCropImageBtn();
                    cropImagePopUp.ClickOnEditokBtn();
                });

                return true;
            });
       }

        public void ClickOnAscendingBtn()
        {
            BaseUi.MongoDb.UpdateSteps("Clicking on Ascending button");
            _browserHelper.ScrollToTop();
            _browserHelper.WaitForElement(ascendingIcon, nameof(ascendingIcon));
            _browserHelper.Click(ascendingIcon, nameof(ascendingIcon));
        }

        public void ClickOnDscBtn()
        {
            BaseUi.MongoDb.UpdateSteps("Clicking on Descending button");
            _browserHelper.ScrollToTop();
            _browserHelper.WaitForElement(descendingIcon, nameof(descendingIcon));
            _browserHelper.Click(descendingIcon, nameof(descendingIcon));
        }

        public List<string> GetItemsIndex()
        {
            BaseUi.MongoDb.UpdateSteps("Getting items index");
            _browserHelper.WaitUntillTrue(() => counters.ToList().Count() == 3);
            return counters.ToList().Select(c => c.Text).ToList();
        }

        public bool ValidateAscDesc(List<string> before, List<string> after)
        {
            BaseUi.MongoDb.UpdateSteps("Validating AscDesc");
            before.Reverse();
            return before.SequenceEqual(after);
        }
    }
}
