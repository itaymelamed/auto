﻿using System;
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
        JObject _exJson;
        List<string> _adNames;
        List<Request> _requests;

        string _errors;

        public AdsUnitHelper(List<Request> requests, BsonValue exJsons, BsonArray adNames)
        {
            _requests = requests;
            _exJson = JObject.Parse(exJsons.ToString());
            _adNames = adNames.Select(x => x.ToString()).ToList();
            _errors = string.Empty;
        }

        public string ValidateJsons()
        {
            _adNames.ForEach(n =>
            {
                var request = _requests.Where(r => r.Url.Contains(n)).FirstOrDefault();
                var acJson = RequestToJobject(request);
                _errors += JToken.DeepEquals(acJson, _exJson) ? "" : $"Ad {n} request was not sent. {Environment.NewLine}";
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
                return JObject.Parse(@"{'remove' : 'video'}");
            }
        }
    }
}