using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutomatedTester.BrowserMob.HAR;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class AdsUnitHelper
    {
        List<JObject> _acJsons;
        List<JObject> _exJsons;
        string _errors;

        public AdsUnitHelper(List<Request> requests, BsonArray exJsons)
        {
            _acJsons = requests.Where(r => r.Url.Contains("https://securepubads.g.doubleclick.net/gampad/ads")).Select(r => RequestToJobject(r)).ToList();
            _exJsons = BsonArrayToList(exJsons);
            _errors = string.Empty;
        }

        public string ValidateJsons()
        {
            _exJsons.ForEach(e => {
                _errors = _acJsons.Any(a => JToken.DeepEquals(e, a)) ? "" : $"Ad {e.Properties().Select(p => p.Name).FirstOrDefault()} was not found. {Environment.NewLine}";
            });

            return _errors;
        }


        JObject RequestToJobject(Request request)
        {
            try
            {
                Uri uri = new Uri(request.Url);
                string query = uri.Query;
                var parametersString = HttpUtility.ParseQueryString(query)["cust_params"];
                var parametersCollection = HttpUtility.ParseQueryString(parametersString);
                var paramsDic = parametersCollection.AllKeys.ToDictionary(k => k, k => parametersCollection[k]);
                var jsonString = JsonConvert.SerializeObject(paramsDic);
                return JObject.Parse(jsonString);
            }
            catch
            {
                return JObject.Parse(@"{'name' : 'video'}");
            }
        }

        List<JObject> BsonArrayToList(BsonArray exJsons)
        {
            return exJsons.ToList().Select(j => JObject.Parse(j.ToString())).ToList();
        }
    }
}