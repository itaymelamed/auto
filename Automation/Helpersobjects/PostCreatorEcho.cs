using System;
using System.Collections.Generic;
using Automation.BrowserFolder;
using Automation.PagesObjects;
using Automation.PagesObjects.EchoFolder;
using MongoDB.Bson;
using NUnit.Framework;

namespace Automation.Helpersobjects
{
    public class PostCreatorEcho 
    {
        Browser _browser;
        string _title;

        public PostCreatorEcho(Browser browser)
        {
            _browser = browser;
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
            articleBase.WriteDec(@"ABOUT 90MIN
                                    90min Company Overview
                                    90min is a global football media and technology company focused on the digital generation. 90min taps into the passion and dedication of the hyper - connected fan by powering the production of authentic, engaging and socially driven content distributed to a rapidly growing audience of over 60 million monthly users in ten languages across web, mobile and social.
                                    90min’s mission is to champion fan - generated media as a key ingredient of sports journalism.
                                    90min includes multilingual support in English, German, Spanish, Italian, French, Turkish, Portuguese, Vietnamese, Thai and Indonesian.With a network of over 200 team communities in Europe, South America and Asia, 90min’s content is consumed by a global audience of millions via online media, mobile platforms and handheld devices. 90min receives over 250 million page views and over 500 million Facebook and Twitter impressions monthly.
                                    platform which gives fan opinions a single global voice.
                                    Citizen journalism: By providing a self - publishing platform for football, 90min is recognising passion and turning it into journalistic work.With a strict editorial curation process in place, 90min is democratising the sport media landscape without losing content quality.
                                    Technology: 90min content generation tools are the richest in online football.Match predictions, test test test test test test test test");
            articleBase.WriteTags(new BsonArray(new List<string>{ "test1", "test2", "test3" }));
            PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
            PostPage postPage = previewPage.ClickOnPublishBtn();

            return _title;
        }
    }
}
