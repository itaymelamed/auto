using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation.PagesObjects
{
    public class SlideShowPage : ListsTemplate
    {

        [FindsBy(How = How.CssSelector, Using = ".image-upload-drop-target")]
        IList<IWebElement> dropMedia { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".redactor-element.redactor_editor")]
        IList<IWebElement> bodyTextBoxs { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[name=title]")]
        IList<IWebElement> titlesBoxs { get; set; }


        public SlideShowPage(Browser browser)
            :base (browser)
        {
        }

        public void DragAndDropImages()
        {
            
            Base.MongoDb.UpdateSteps("Dragging images to media drop boxes..");
            Thread.Sleep(2000);
            _browserHelper.WaitUntillTrue(() => dropMedia.ToList().Count() >= 4, "Media boxes failed to load");
            _browserHelper.WaitUntillTrue(() => imagesResults.ToList().Count() == 30, "There were no 30 results images.");

            _browserHelper.WaitUntillTrue(() =>
            {
                dropMedia.ToList().ForEach(m =>
                {
                    _browserHelper.DragElement(imagesResults.FirstOrDefault(), m);
                    CropImagePopUp cropImagePopUp = new CropImagePopUp(_browser);
                    cropImagePopUp.ClickOnCropImageBtn();
                    cropImagePopUp.ClickOnEditokBtn();
                });

                return true;
            });
        }

        public void SetBodyTextBoxsSlide(string text)
        {
            Base.MongoDb.UpdateSteps("Setting body text boxs");
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() >= 4);
            bodyTextBoxs.ToList().ForEach(x => _browserHelper.SetText(x, text));
        }

        public List<string> GetBodyTextBoxesSlideValue()
        {
            Base.MongoDb.UpdateSteps("Getting body text boxes value");
            List<string> values = new List<string>();
            _browserHelper.WaitUntillTrue(() => bodyTextBoxs.ToList().Count() == 5);
            bodyTextBoxs.ToList().ForEach(t => values.Add(t.Text));

            return values;
        }

        public string ValidateBodyTextBoxesSlide(List<string> acValues, string text)
        {
            Base.MongoDb.UpdateSteps("Validating body text boxes.");

            var errors = string.Empty;
            if (acValues.All((v => v == text)))
                return errors;

            acValues.Where(v => v != text).ToList().ForEach(x => errors += $"{x} {Environment.NewLine}");

            return errors;
        }

    }
}