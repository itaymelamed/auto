using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder
{
    public class ApiObject
    {
        public JObject GetRequest(string url)
        {
            using (var client = new WebClient())
            {
                var responseString = client.DownloadString(url);
                return JObject.Parse(responseString);
            }
        }

        public void GetRequestVoid(string url)
        {
            using (var client = new WebClient())
            {
                var responseString = client.DownloadString(url);
            }
        }

        public WebResponse MakeRequest(string url, string method, string reference = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            if (reference != null)
            {
                byte[] requestBytes = Encoding.UTF8.GetBytes(reference);
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }

                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
                request.ContentLength = 0;

            return request.GetResponse();
        }

        public WebResponse MakeJsonRequest(string url, string method, string payload)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;

            if (payload != null)
            {
                request.ContentType = "text/json";
                request.ContentLength = payload.Length;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else
                request.ContentLength = 0;

            return request.GetResponse();
        }
    }
}