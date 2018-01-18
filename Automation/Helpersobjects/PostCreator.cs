﻿using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.Helpersobjects
{
    public class PostCreator : HomePage
    {
        public Type Article { get; }
        public Type Lists { get; }
        public Type LineUp { get; }
        public List<Type> Templates { get; }
        public string Title { get; set; }

        public PostCreator(Browser browser)
            :base(browser)
        {
            Article = typeof(ArticleBase);
            Lists = typeof(ListsTemplate);
            Templates = new List<Type>() { Article, Lists };
            _browser = browser;
            Title = $"VIDEO:{TestContext.CurrentContext.Test.Name}" + new Random().Next(1, 1000);
        }

        public PostPage Create(Type template) 
        {
            EditorPage editorPage = ClickOnAddArticle();
            ArticleBase articleBase = editorPage.ClickOnTemplate(Templates.FindIndex(x => x == template));
            articleBase.ClickOnMagicStick(2);
            articleBase.WriteTitle(Title);
            PreviewPage previewPage = articleBase.ClickOnPreviewBtn();
            PostPage postPage = previewPage.ClickOnPublishBtn();
            postPage.ValidatePostCreated(Title);

            return new PostPage(_browser);
        }

        public string Create()
        {
            AdminPage adminPage = ClickOnAdmin();
            adminPage.ClickOnCreatePost();
            var title = adminPage.GetPostTitle().Split('>')[1];
            var parsedTitle = new string(title.ToCharArray().Where(c => char.IsLetter(c) || c == '-').ToArray()).Replace("posts", "").Replace("-"," ");
            Title = parsedTitle.Trim();
            return parsedTitle;
        }
    }
}
