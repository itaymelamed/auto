using Automation.ApiFolder;

namespace Automation.LoadBalancerFolder
{
    public class Hub
    {
        string _baseUrl;
        string _url;
        string _hubApiUrl;
        ApiObject _api;

        public Hub(int port, int HubNum)
        {
            _api = new ApiObject();
            _baseUrl = $"http://hub{HubNum}:{port}";
            _url = $"{_baseUrl}/wd/hub";
            _hubApiUrl = $"{_baseUrl}/grid/api/hub";
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
