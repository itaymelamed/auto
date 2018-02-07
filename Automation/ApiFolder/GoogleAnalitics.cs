using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutomatedTester.BrowserMob.HAR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class GoogleAnalitics
    {
        List<Request> _requests;
        static string _url = "https://www.google-analytics.com/collect";

        public GoogleAnalitics(List<Request> requests)
        {
            _requests = requests.Where(r => r.Url.Contains(_url)).ToList();
        }

        JObject RequestToJobject(Request request)
        {
            try
            {
                Uri uri = new Uri(request.Url);
                string query = uri.Query;
                var parameters = HttpUtility.ParseQueryString(query);
                var paramsDic = parameters.AllKeys.Where(k => k.Contains("cd")).ToDictionary(k => k, k => parameters[k]);
                var jsonString = JsonConvert.SerializeObject(paramsDic);
                return JObject.Parse(jsonString);
            }
            catch
            {
                return JObject.Parse(@"{'error' : 'error'}");
            }
        }
    }
}