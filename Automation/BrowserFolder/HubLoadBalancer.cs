using System;
using Automation.ApiFolder;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        ApiObject _apiObj;
        public string _hub1 { get; set; }
        public string _hub2 { get; set; }
        Configurations _config;
        static readonly object _syncObject = new object();

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _apiObj = new ApiObject();
            _hub1 = _apiObj.GetRequest("http://ip-10-0-8-224.us-west-2.compute.internal:4444/grid/api/hub")["slotCounts"]["free"].ToString();
            _hub2 = _apiObj.GetRequest("http://ip-10-0-6-142.us-west-2.compute.internal:4444/grid/api/hub")["slotCounts"]["free"].ToString();
        }

        public string GetAvalibleHub()
        {
            try
            {
                if (int.Parse(_hub1) <= int.Parse(_hub2))
                    return $"http://{_config.GetIp(0)}:4444/wd/hub";
                else
                    return $"http://{_config.GetIp(1)}:4444/wd/hub";
            }
            catch
            {
                throw new Exception(_hub1 + _hub2);
            }
        }
    }
}
