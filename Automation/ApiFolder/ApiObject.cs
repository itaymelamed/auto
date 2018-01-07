using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class ApiObject
    {
        public JObject GetRequest(string url)
        {
            using (var client = new WebClient())
            {
                var responseString = client.DownloadString(new Uri(url));
                return JObject.Parse(responseString);
            }

        }
    }
}
