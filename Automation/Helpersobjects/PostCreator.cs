using System;
using System.Collections.Generic;
using System.Linq;
using Automation.BrowserFolder;
using Automation.PagesObjects;
using Automation.TestsFolder;
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
            var parsedTitle = new string(Title.ToCharArray().Where(c => char.IsLetter(c) || c == '-').ToArray()).Replace("posts", "").Replace("-", " ");
            Title = parsedTitle.Trim();

            return new PostPage(_browser);
        }


        public string Create()
        {
            _browser.Navigate(Base._config.Url + "/" + Base._config.ConfigObject.Language + "/" + "admin");
            AdminPage adminPage = new AdminPage(_browser);
            adminPage.ClickOnCreatePost();
            var title = adminPage.GetPostTitle().Split('>')[1];
            var parsedTitle = new string(title.ToCharArray().Where(c => char.IsLetter(c) || c == '-').ToArray()).Replace("posts", "").Replace("-", " ");
            Title = parsedTitle.Trim();
            return parsedTitle;
        }

        public List<string> Create(int posts)
        {
            Base.MongoDb.UpdateSteps("Creating multiple posts.");
            List<string> titles = new List<string>();
            for (int i = 0; i < posts; i++)
            {
                titles.Add(Create());
            }

            return titles;
        }

        public string CreateToDomain(string domain)
        {
            _browser.Navigate("http://" + Base._config.Env + "." + Base._config.GlobalConfigObject[domain]["Url"] + "/" + Base._config.GlobalConfigObject[domain]["Language"] + "/" + "admin");
            AdminPage adminPage = new AdminPage(_browser);
            adminPage.ClickOnCreatePost();
            var title = adminPage.GetPostTitle().Split('>')[1];
            var parsedTitle = new string(title.ToCharArray().Where(c => char.IsLetter(c) || c == '-').ToArray()).Replace("posts", "").Replace("-", " ");
            Title = parsedTitle.Trim();
            return parsedTitle;
        }
    }
}