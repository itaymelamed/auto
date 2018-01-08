using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automation.ApiFolder;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;
using Automation.TestsObjects;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        Configurations _config;
        ApiObject _api;
        int _hub1;
        int _hub2;
        static readonly object _syncObject = new object();

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _api = new ApiObject();
        }

        public void UpdateHubs()
        {
            _hub1 = int.Parse(_api.GetRequest($"http://{_config.GetIp(0)}:4444/grid/api/hub")["slotCounts"]["free"].ToString());
            _hub2 = int.Parse(_api.GetRequest($"http://{_config.GetIp(1)}:4444/grid/api/hub")["slotCounts"]["free"].ToString());
        }

        public bool IsQueue()
        {
            UpdateHubs();
            return _hub1 == 0 && _hub2 >= 0;
        }

        public string GetAvailbleHub()
        {
            lock(_syncObject)
            {
                while (IsQueue())
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                return _hub1 >= _hub2 ? $"http://{_config.GetIp(0)}:4444/wd/hub" : $"http://{_config.GetIp(1)}:4444/wd/hub";
            }
        }
    }
}
