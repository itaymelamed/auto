using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class JsonHelper
    {
        ApiObject _api;
        Func<JObject, bool> search;
        string _apiUrls;

        public JsonHelper(string apiBaseUrl)
        {
            _api = new ApiObject();
            _apiUrls = apiBaseUrl;
        }

        public bool SearchArticleInFeed(string valueToSearch, int team = 179, int timeOut = 20)
        {
            int timePassed = 0;
            string url = "";
            JObject res = _api.GetRequest(url);

            search = (r) => {
                var articles = r["data"]["feed"].ToList()
                    .Where(x => x.ToString().Contains("article"))
                    .Where(j => j["article"]["title"].ToString() == valueToSearch).ToList();
                return articles.Count >= 1;
            };

            while(!search(res) && timePassed <= timeOut)
            {
                res = _api.GetRequest(url);
                search(res);
                Thread.Sleep(1000);
                timePassed++;
            }

            return search(res);
        }
    }
}