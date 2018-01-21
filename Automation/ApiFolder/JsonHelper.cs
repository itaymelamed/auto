using System;
using System.Linq;
using System.Text.RegularExpressions;
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

        public enum Leagues
        {
            ChampionLeags = 2,
            PremierLeague = 1
        }

        public JsonHelper() { }

        public JsonHelper(int team, string valueToSearch)
        {
            _api = new ApiObject();
            _apiConfig = Base._config.ApiConfig;
            _url = _apiConfig.GetFeedUrl(team);
            _valueToSearch = valueToSearch;
        }

        public JsonHelper(Leagues league, string valueToSearch)
        {
            _api = new ApiObject();
            _apiConfig = Base._config.ApiConfig;
            _url = _apiConfig.GetLeagueUrl(league);
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
            return res["data"]["feed"].ToList()
                    .Where(x => x.ToString().Contains("article"))
                                              .Any(j => Regex.Replace(j["article"]["title"].ToString().ToLower().Replace('-', ' '), @"[\d-]", string.Empty) == _valueToSearch);
        }

        public bool WaitUntill(Func<bool> func, int timeOut)
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