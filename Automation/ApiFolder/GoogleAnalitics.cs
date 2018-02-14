using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using AutomatedTester.BrowserMob.HAR;
using Automation.TestsFolder;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class GoogleAnalitics
    {
        Func<List<Request>> _requests;
        static string _url = "https://www.google-analytics.com/collect";

        public GoogleAnalitics(Func<List<Request>> requests)
        {
            _requests = requests;
        }

        public string ValidateEventRequest(string eventAction, BsonValue exJsonBson, BsonArray ignorProp, bool post = false, int timeOut = 120)
        {
            var acJson = GetEvent(eventAction, timeOut, post);
            var exJson = JObject.Parse(exJsonBson.ToString());

            acJson.Remove("a");
            acJson.Remove("z");
            exJson.Remove("a");
            exJson.Remove("z");

            var ignorList = ignorProp.Select(i => i.ToString()).ToList();
            ignorList.AddRange(acJson.Properties().Select(p => p.Name).Where(k => !k.Contains("cd")));
            var acJsonKeys = acJson.Properties().Select(p => p.Name).ToList();
            var intersect = ignorList.Intersect(acJsonKeys).ToList();

            intersect.ForEach(p => CustumJson(acJson, exJson, p));
            acJson["gtm"] = acJson["gtm"].ToString().Remove(0, 3);
            exJson["gtm"] = exJson["gtm"].ToString().Remove(0, 3);

            return CompareJsons(exJson, acJson);
        }

        JObject GetEvent(string eventAction, int timeOut, bool post = false)
        {
            JObject jObject = null;
            List<Request> requests = !post ? _requests().Where(r => r.Url.Contains(_url)).ToList() : _requests().Where(r => r.Url.Contains(_url) && r.Method == "POST").ToList();
            var reqs = !post ? requests.Where(r => r.Url.Contains(_url)).Select(r => RequestToJobject(r)).ToList() : requests.Where(r => r.Url ==_url).Select(r => PostDataToJobject(r.PostData.Text)).ToList();
            var e = reqs.Where(r => r.Properties().Select(p => p.Name).Contains("ea")).ToList();
            jObject = e.Where(r => r["ea"].ToString() == eventAction).FirstOrDefault();
            if (post)
                reqs.ForEach(r => r.Properties().Select(p => p.Name).ToList().ForEach(n => jObject[n] = jObject[n].ToString().Replace("%20", " ")));

            return jObject;
        }

        void CustumJson(JObject acJson, JObject exJson, string parameterName)
        {
            if (!string.IsNullOrEmpty(acJson[parameterName].ToString()))
                exJson[parameterName] = acJson[parameterName];
        }

        JObject RequestToJobject(Request request)
        {
            try
            {
                Uri uri = new Uri(request.Url);
                string query = uri.Query;
                var parameters = HttpUtility.ParseQueryString(query);
                var paramsDic = parameters.AllKeys.ToDictionary(k => k, k => parameters[k]);
                parameters.AllKeys.ToList().ForEach(k => paramsDic[k] = paramsDic[k].Replace("%20", " "));
                var jsonString = JsonConvert.SerializeObject(paramsDic);
                return JObject.Parse(jsonString);
            }
            catch
            {
                return JObject.Parse(@"{'error' : 'error'}");
            }
        }

        JObject PostDataToJobject(string text)
        {
            try
            {
                Uri uri = new Uri(_url + "?" + text);
                string query = uri.Query;
                var parameters = HttpUtility.ParseQueryString(query);
                var paramsDic = parameters.AllKeys.ToDictionary(k => k, k => parameters[k]);
                parameters.AllKeys.ToList().ForEach(k => paramsDic[k] = paramsDic[k].Replace("%20", " "));
                var jsonString = JsonConvert.SerializeObject(paramsDic);
                return JObject.Parse(jsonString);
            }
            catch(Exception e)
            {
                return JObject.Parse(@"{'error' : 'error'}");
            }
        }

        string CompareJsons(JObject ex, JObject ac)
        {
            string diffs = string.Empty;
            var exJsonNames = ex.Properties().Select(p => p.Name).ToList();
            var acJsonNames = ac.Properties().Select(p => p.Name).ToList();

            try
            {
                exJsonNames.ForEach(n =>
                {
                    Base.MongoDb.UpdateSteps($"Validate parameter {n} value.");
                    diffs += ex[n].ToString() == ac[n].ToString() ? "" : $"*) Expected value for parameter {n}: {ex[n]}. But Actual is: {ac[n]}.     {Environment.NewLine}";
                });
            }
            catch
            {

            }

            return diffs;
        }

        public JObject WaitForRequest(Func<JObject> func, int timeOut)
        {
            int i = 0;
            JObject jObject = null;

            while(jObject == null && i < timeOut)
            {
                Thread.Sleep(1000);
                i++;
                try
                {
                    jObject = func();
                }
                catch
                {
                    jObject = null;
                }
            }

            return jObject;
        }
    }
}