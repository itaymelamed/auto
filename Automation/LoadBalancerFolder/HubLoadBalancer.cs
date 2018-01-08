using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automation.ApiFolder;
using Automation.ConfigurationFolder;
using Automation.LoadBalancerFolder;
using Automation.TestsFolder;
using Automation.TestsObjects;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        Configurations _config;
        Hub _hub1;
        Hub _hub2;
        static readonly object _syncObject = new object();

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _hub1 = new Hub(_config, 0);
            _hub2 = new Hub(_config, 1); 
        }

        public bool IsQueued()
        {
            return !_hub1.IsHubAvalible() && !_hub2.IsHubAvalible();
        }

        public string GetAvailbleHub()
        {
            lock(_syncObject)
            {
                while (IsQueued())
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));

                if (_hub1.GetHubFreeNodes() > _hub2.GetHubFreeNodes())
                    return _hub1.GetHubUrl();
                else
                    return
                        _hub2.GetHubUrl();
            }
        }
    }
}
