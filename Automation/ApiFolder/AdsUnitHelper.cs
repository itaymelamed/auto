using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutomatedTester.BrowserMob.HAR;
using Automation.TestsFolder;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class AdsUnitHelper : JsonHelper
    {
        JObject _exJson;
        List<string> _adNames;
        List<Request> _requests;
        static string _url = "https://securepubads.g.doubleclick.net/gampad/ads";

        string _errors;

        public AdsUnitHelper(List<Request> requests, BsonValue exJsons, BsonArray displyed, BsonArray notDisplyed, string experimentId)
        {
            _requests = requests;
            _exJson = JObject.Parse(exJsons.ToString());
            _exJson.Add("experimentId",experimentId);
            _adNames = displyed.Select(x => x.ToString()).ToList();
            _adNames.AddRange(notDisplyed.Select(x => x.ToString()));
            _errors = string.Empty;
        }

        public string ValidateJsons(BsonArray ignor)
        {
            _adNames.ForEach(n =>
            {
                Base.MongoDb.UpdateSteps($"Validate {n} request was sent.");
                var ignorList = ignor.Count > 0 ? ignor.Select(i => i.ToString()).ToList() : new List<string>(){""};
                var request = _requests.Where(r => r.Url.Contains(n) && r.Url.Contains(_url)).FirstOrDefault();
                var acJson = RequestToJobject(request);
                if (acJson == null)
                {
                    _errors += $"<div>No request has found for <b>{n}</b></div>"; ;
                    return;
                }

                ignorList.ForEach(i =>
                {
                    if (acJson.Properties().Select(p => p.Name).Contains(i))
                        acJson.Remove(i);
                });

                _errors += $"{JsonComparer(_exJson, acJson, n)}";
            });

            return _errors;
        }

        public string ValidateJsons()
        {
            _adNames.ForEach(n =>
            {
                Base.MongoDb.UpdateSteps($"Validate {n} request was sent.");
                var request = _requests.Where(r => r.Url.Contains(n) && r.Url.Contains(_url)).FirstOrDefault();
                var acJson = RequestToJobject(request);

                _errors += acJson == null ? $"<div>No request has found for <b>{n}</b>.</div>" : $"{JsonComparer(_exJson, acJson, n)}";
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
                return null;
            }
        }
    }
}