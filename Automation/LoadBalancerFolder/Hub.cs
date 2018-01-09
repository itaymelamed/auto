using System;
using Automation.ApiFolder;
using Automation.ConfigurationFolder;

namespace Automation.LoadBalancerFolder
{
    public class Hub
    {
        string _baseUrl;
        string _url;
        string _hubApiUrl;
        ApiObject _api;
        Configurations _config;

        public Hub(string host, int port)
        {
            _api = new ApiObject();
            _baseUrl = $"http://{host}:{port}";
            _url = $"http://{_baseUrl}/wd/hub";
            _hubApiUrl = $"http://{_baseUrl}/grid/api/hub";
        }

        public bool IsHubAvalible()
        {
            try
            {
                return int.Parse(_api.GetRequest(_hubApiUrl)["slotCounts"]["free"].ToString()) != 0;
            }
            catch
            {
                return false;
            }
        }

        public int GetWaitingSessions()
        {
            try
            {
                return int.Parse(_api.GetRequest(_hubApiUrl)["newSessionRequestCount"].ToString());
            }
            catch
            {
                return 0;
            }
        }

        public string GetHubUrl()
        {
            return _url;
        }

        public int GetHubFreeNodes()
        {
            try
            {
                return int.Parse(_api.GetRequest(_hubApiUrl)["slotCounts"]["free"].ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
}
