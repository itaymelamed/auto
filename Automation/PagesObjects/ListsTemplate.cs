using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

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

        [FindsBy(How = How.CssSelector, Using = ".search-results.search-image__search-results-list")]
        IList<IWebElement> imagesResultsBox { get; set; }



        public ListsTemplate(Browser browser) :
            base(browser)
        {

        }

        public string GetIntroductionValue()
        {
            Base.MongoDb.UpdateSteps("Get introduction value");
            _browserHelper.WaitForElement(introduction, nameof(introduction));
            return introduction.Text;
        }

        public List<string> GetImagesUrl()
        {
            Base.MongoDb.UpdateSteps("Get images URL.");
            List<string> imagesSrcs = new List<string>();
            images.ToList().ForEach(p =>
            {
                imagesSrcs.Add(p.GetAttribute("src"));
            });

            return imagesSrcs;
        }

        public void SetBodyTextBoxs(string text)
        {
            Base.MongoDb.UpdateSteps("Set body text boxs");
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() == 5);
            bodyTextBoxs.ToList().ForEach(x => _browserHelper.SetText(x, text));
        }

        public List<string> GetBodyTextBoxesValue()
        {
            Base.MongoDb.UpdateSteps("Get body text boxes value");
            List<string> values = new List<string>();
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() == 5);
            bodyTextBoxs.ToList().ForEach(t => values.Add(t.Text));

            return values;
        }

        public string ValidateBodyTextBoxes(List<string> acValues, string text)
        {
            Base.MongoDb.UpdateSteps("Validate body text boxes.");

            var errors = string.Empty;
            if (acValues.All((v => v == text)))
                return errors;

            acValues.Where(v => v != text).ToList().ForEach(x => errors += $"{x} {Environment.NewLine}");

            return errors;
        }

        public void SetSubTitles(string text)
        {
            Base.MongoDb.UpdateSteps("Set subtitle values.");
            var xxx = subTitleFields.ToList().Count();
            _browserHelper.WaitUntillTrue(() => subTitleFields.ToList().Count() == 3);
            subTitleFields.ToList().ForEach(s => _browserHelper.SetText(s, text));
        }

        public List<string> GetSubTitelsValues()
        {
            Base.MongoDb.UpdateSteps("Get subtitle values.");
            List<string> values = new List<string>();
            _browserHelper.WaitUntillTrue(() => subTitleFields.ToList().Count() == 3);
            subTitleFields.ToList().ForEach(t => values.Add(t.GetAttribute("value")));

            return values;
        }

        public bool ValidateSubTitlesFields(List<string> acValues, string text)
        {
            Base.MongoDb.UpdateSteps("Validate subtitles fields.");
            return acValues.All(v => v == text);
        }

        public void DragImages()
        {
            Base.MongoDb.UpdateSteps("Drag images to media drop boxes.");
            _browserHelper.WaitUntillTrue(() => mediaDropBoxs.ToList().Count() == 4);
            _browserHelper.WaitUntillTrue(() => imagesResultsBox.ToList().Count() == 30);

            _browserHelper.WaitUntillTrue(() =>
            {
                mediaDropBoxs.ToList().ForEach(m =>
                {
                    _browserHelper.DragElement(images.FirstOrDefault(), m);
                    CropImagePopUp cropImagePopUp = new CropImagePopUp(_browser);
                    cropImagePopUp.ClickOnCropImageBtn();
                    cropImagePopUp.ClickOnEditokBtn();
                });

                return true;
            });
        }
    }
}
