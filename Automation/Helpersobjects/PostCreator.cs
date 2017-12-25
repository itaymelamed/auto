using System;
using System.Collections.Generic;
using Automation.BrowserFolder;
using Automation.PagesObjects;

namespace Automation.Helpersobjects
{
    public class PostCreator : HomePage
    {
        public Type Article { get; }
        public Type Lists { get; }
        public Type LineUp { get; }
        public List<Type> Templates { get; }
        Browser _browser;

        public PostCreator(Browser browser)
            :base(browser)
        {
            Article = typeof(ArticleBase);
            Lists = typeof(ListsTemplate);
            Templates = new List<Type>() { Article, Lists };
            _browser = browser;
        }

        public PostPage Create(Type template) 
        {
            EditorPage editorPage = ClickOnAddArticle();
            ArticleBase articleBase = editorPage.ClickOnTemplate(Templates.FindIndex(x => x == template));
            articleBase.ClickOnMagicStick(2);
            articleBase.WriteTitle("VIDEO:Title Title Title");
            PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
            PostPage postPage = previewPage.ClickOnPublishBtn();
            postPage.ValidatePostCreated("VIDEO:Title Title Title");

            return new PostPage(_browser);
        }


    }
}
