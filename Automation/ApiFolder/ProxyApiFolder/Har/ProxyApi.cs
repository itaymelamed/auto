using System;
using System.IO;
using System.Web;
using AutomatedTester.BrowserMob.HAR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class ProxyApi
    {
        readonly string _url;
        readonly Int16 _port;
        readonly string _proxy;
        readonly string _baseUrlProxy;
        readonly ApiObject _api;

        public ProxyApi(string url)
        {
            _api = new ApiObject();
            _url = url;
            _baseUrlProxy = $"{_url}/proxy";
            using (var response = _api.MakeRequest(_baseUrlProxy, "POST"))
            {
                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                    throw new Exception("No response from proxy");

                using (var responseStreamReader = new StreamReader(responseStream))
                {
                    var jsonReader = new JsonTextReader(responseStreamReader);
                    var token = JToken.ReadFrom(jsonReader);
                    var portToken = token.SelectToken("port");
                    if (portToken == null)
                        throw new Exception("No port number returned from proxy");

                    _port = (Int16)portToken;
                }
            }

            var parts = url.Split(':');
            _proxy = parts[1].TrimStart('/') + ":" + _port;
        }

        public void NewHar(string reference = null)
        {
            _api.MakeRequest(String.Format("{0}/{1}/har", _baseUrlProxy, _port), "PUT", reference);
        }


        public void NewPage(string reference)
        {
            _api.MakeRequest(String.Format("{0}/{1}/har/pageRef", _baseUrlProxy, _port), "PUT", reference);
        }

        public HarResult GetHar()
        {
            var response = _api.MakeRequest($"{_baseUrlProxy}/{_port}/har", "GET");
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                    return null;

                using (var responseStreamReader = new StreamReader(responseStream))
                {
                    var json = responseStreamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<HarResult>(json);
                }
            }
        }

        public string SeleniumProxy
        {
            get { return _proxy; }
        }

        static string FormatBlackOrWhiteListFormData(string regexp, int statusCode)
        {
            return String.Format("regex={0}&status={1}", HttpUtility.UrlEncode(regexp), statusCode);
        }

        public void Close()
        {
            _api.MakeRequest($"{_baseUrlProxy}/{_port}", "DELETE");
        }
    }
}
