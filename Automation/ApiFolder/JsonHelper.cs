﻿using System;
using System.Linq;
using System.Threading;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class JsonHelper
    {
        ApiObject _api;
        ApiConfig _apiConfig;
        string _url;
        string _valueToSearch;

        public JsonHelper(int team, string valueToSearch)
        {
            _api = new ApiObject();
            _apiConfig = Base._config.ApiConfig;
            _url = _apiConfig.GetFeedUrl(team);
            _valueToSearch = valueToSearch;
        }

        public JsonHelper(string category, string valueToSearch)
        {
            _api = new ApiObject();
            _apiConfig = Base._config.ApiConfig;
            _url = _apiConfig.GetCategoryUrl(category);
            _valueToSearch = valueToSearch;
        }

        public JsonHelper(string language, int team, string brand, string valueToSearch)
        {
            _api = new ApiObject();
            _apiConfig = Base._config.ApiConfig;
            _url = _apiConfig.GetFeedUrlOtherBrand(language, brand, team);
            _valueToSearch = valueToSearch;
        }

        public bool SearchArticleInFeed(int timeOut = 120)
        {
            Base.MongoDb.UpdateSteps("Search post on Api feed.");
            return WaitUntill(Search, timeOut);
        }

        bool Search()
        {
            JObject res = _api.GetRequest(_url);
            var articles = res["data"]["feed"].ToList()
                    .Where(x => x.ToString().Contains("article"))
                       .Where(j => j["article"]["title"].ToString() == _valueToSearch).ToList();
            return articles.Count >= 1;
        }

        bool WaitUntill(Func<bool> func, int timeOut)
        {
            int timePassed = 0;
            while(!func() && timePassed <= timeOut)
            {
                Thread.Sleep(1000);
                timePassed++;
            }

            return func();
        }
    }
}