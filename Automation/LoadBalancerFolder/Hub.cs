using Automation.ApiFolder;
using Automation.ConfigurationFolder;

namespace Automation.LoadBalancerFolder
{
    public class Hub
    {
        string _url;
        string _hubApiUrl;
        ApiObject _api;
        Configurations _config;

        public Hub(Configurations config, int port)
        {
            _api = new ApiObject();
            _config = config;
            _url = $"http://{_config.GetIp()}:{port}/wd/hub";
            _hubApiUrl = $"http://{_config.GetIp()}:{port}/grid/api/hub";
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
                return 8;
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

        public bool Queued()
        {
            try
            {
                return GetWaitingSessions() != 0;
            }
            catch
            {
                return true;
            }
        }
    }
}
