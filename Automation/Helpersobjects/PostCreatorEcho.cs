using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.PagesObjects;
using Automation.PagesObjects.EchoFolder;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.Helpersobjects
{
    public class PostCreatorEcho : PostCreator
    {
        static Random random = new Random();
        string _title;

        public PostCreatorEcho(Browser browser)
            :base(browser)
        {
            _title = TestContext.CurrentContext.Test.Name + new Random().Next(1, 1000);
        }

        public string CreatePost()
        {
            EchoPage echoPage = new EchoPage(_browser);
            EditorPage editorPage = echoPage.ClickOnEditorBtn();

            ArticleBase articleBase = editorPage.ClickOnArticle();
            articleBase.WriteTitle(_title);
            articleBase.SearchImage("cats");
            CropImagePopUp cropImagePopUp = articleBase.DragImage(0);
            cropImagePopUp.ClickOnCropImageBtn();
            cropImagePopUp.ClickOnEditokBtn();
            articleBase.WriteDec(CreateRendomDesc());
            _browser.BrowserHelper.WaitUntillTrue(() =>
            {
                _browserHelper.WaitUntillTrue(() => {
                    articleBase.WriteTags(new BsonArray(new List<string>() { "Atest", "BTest", "CTest" }));
                    return articleBase.GetTagsValue().Count >= 3;
                });

                return articleBase.GetTagsValue().Count > 0;
            }, "Failed to add tags");
            PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
            PostPage postPage = previewPage.ClickOnPublishBtn();

            return _title;
        }

        public string CreateRendomDesc()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 200)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
