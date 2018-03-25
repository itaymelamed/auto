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
        static string _url = "www.google-analytics.com/collect";

        public GoogleAnalitics(Func<List<Request>> requests)
        {
            _requests = requests;
        }

        public string ValidateEventRequest(string eventAction, BsonValue exJsonBson, BsonArray ignorProp, bool post = false, int timeOut = 120)
        {
            JObject acJson;
            JObject exJson;

            try
            {
                acJson = GetEvent(eventAction, timeOut, post);
                var a = acJson.ToString();
                exJson = JObject.Parse(exJsonBson.ToString());
            }
            catch (Exception e)
            {
                throw new NUnit.Framework.AssertionException($"Event {eventAction} was not sent. {e.Message}");
            }

            try
            {
                acJson.Remove("a");
                acJson.Remove("z");
                exJson.Remove("a");
                exJson.Remove("z");

                ignorProp.Where(p => p.ToString().Contains("cd")).Select(i => i.ToString()).ToList().ForEach(p => CustumJson(acJson, exJson, p));
                acJson["gtm"] = acJson["gtm"].ToString().Remove(0, 3);
                exJson["gtm"] = exJson["gtm"].ToString().Remove(0, 3);
            }
            catch { }

            return CompareJsons(exJson, acJson);
        }

        JObject GetEvent(string eventAction, int timeOut, bool post = false)
        {
            JObject jObject = null;

            try
            {
                List<Request> requests = !post ? _requests().Where(r => r.Url.Contains(_url)).ToList() : _requests().Where(r => r.Url.Contains(_url) && r.Method == "POST").ToList();
                var reqs = !post ? requests.Where(r => r.Url.Contains(_url)).Select(r => RequestToJobject(r)).ToList() : requests.Where(r => r.Url == _url).Select(r => PostDataToJobject(r.PostData.Text)).ToList();
                var e = reqs.Where(r => r.Properties().Select(p => p.Name).Contains("ea")).ToList();
                jObject = e.Where(r => r["ea"].ToString() == eventAction).FirstOrDefault();
                if (post)
                    reqs.ForEach(r => r.Properties().Select(p => p.Name).ToList().ForEach(n => jObject[n] = jObject[n].ToString().Replace("%20", " ")));
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException($"Event {eventAction} was not sent. {e.Message}");
            }

            return jObject;
        }

        void CustumJson(JObject acJson, JObject exJson, string parameterName)
        {
            try
            {
                if(string.IsNullOrEmpty(acJson[parameterName].ToString()))
                    throw new NUnit.Framework.AssertionException($"Parameter {parameterName} is empty.");

                Base.MongoDb.UpdateSteps($"Validate {parameterName} is not empty.");
                acJson.Property(parameterName).Remove();
                exJson.Property(parameterName).Remove();
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException(e.Message);
            }
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
            catch
            {
                return JObject.Parse(@"{'error' : 'error'}");
            }
        }

        string CompareJsons(JObject ex, JObject ac)
        {
            string diffs = string.Empty;
            var exJsonNames = ex.Properties().Select(p => p.Name).Where(k => k.Contains("cd")).ToList();
            var acJsonNames = ac.Properties().Select(p => p.Name).Where(k => k.Contains("cd")).ToList();

            try
            {
                exJsonNames.ForEach(n =>
                {
                    Base.MongoDb.UpdateSteps($"Validate parameter {n} = {ex[n]}.");
                    diffs += ex[n].ToString() == ac[n].ToString() ? "" : $"*) Expected value for parameter {n}: {ex[n]}. But Actual is: {ac[n]}.     {Environment.NewLine}";
                    if (ex[n].ToString() != ac[n].ToString())
                        Base.MongoDb.UpdateSteps($"Parameter {n} value is diffrent than expected.");
                });
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException(e.Message);
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