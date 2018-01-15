using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.BrowserFolder;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.PagesObjects
{
    public class SlideShowPage : ArticleBase
    {

        [FindsBy(How = How.CssSelector, Using = ".image-upload-drop-target")]
        IList<IWebElement> dropMedia { get; set; }


        public SlideShowPage(Browser browser)
            :base (browser)
        {
        }

        public void DragAndDropImages()
        {
            
            Base.MongoDb.UpdateSteps("Drag images to media drop boxes.");
            Thread.Sleep(2000);
            _browserHelper.WaitUntillTrue(() => dropMedia.ToList().Count() == 5, "Media boxes failed to load");
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
    }
}