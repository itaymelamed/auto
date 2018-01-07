using System;
using Automation.ApiFolder;
using Automation.TestsFolder;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        string _hub1;
        string _hub2;
        ApiObject _apiObj;

        public HubLoadBalancer()
        {
            _hub1 = $"http://{Base._config.Ip}:4444";
            _hub2 = $"http://ip-10-0-6-142.us-west-2.compute.internal:4444";
            _apiObj = new ApiObject(); 
        }

        public int CheckHubLoad(string hubUrl)
        {
            return int.Parse(_apiObj.GetRequest(hubUrl)["used"].ToString());
        }

        public string GetIdleHub()
        {
            int hub1Load = CheckHubLoad(_hub1);
            int hub2Load = CheckHubLoad(_hub2);

            return hub1Load >= hub2Load ? $"{_hub2}/wd/hub" : $"{_hub1}/wd/hub";
        }
    }
}
