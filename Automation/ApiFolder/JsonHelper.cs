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

        public string JsonComparer(JObject exJson, JObject acJson, string adName)
        {
            var errors = string.Empty;

            var exJsonKeys = exJson.Properties().Select(p => p.Name).ToList();
            var acJsonKeys = acJson.Properties().Select(p => p.Name).ToList();
            var diffsKeys = exJsonKeys.Except(acJsonKeys).ToList();
            diffsKeys.AddRange(acJsonKeys.Except(exJsonKeys));
            diffsKeys = diffsKeys.Distinct().ToList();

            diffsKeys.ForEach(d => errors += $"<div><b>{adName}</b>: Following parameter is missing the request: {d}</div></br></br>");

            if (!string.IsNullOrEmpty(errors))
            {
                errors += $"<div><b>{adName}:</b> </div><div><b>Expected JSON:</b> {exJson.ToString()}.</div> <div><b>Actual JSON: {acJson.ToString()}</b></div>";
                return errors;
            }

            try
            {
                exJson.Properties().Select(p => p.Name).ToList().ForEach(k => errors += exJson[k].ToString() == acJson[k].ToString() ? "" : $"<div><b>{adName}</b>: Expected value for key: {k} is <b>{exJson[k]}</b>. Actual: <b>{acJson[k]}</b></div><div>-------------------------</div>");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return errors;
        }
    }
}