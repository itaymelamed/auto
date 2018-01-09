using System;
using System.Threading;
using Automation.ConfigurationFolder;
using Automation.LoadBalancerFolder;

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
            _hub1 = new Hub(_config, 4444);
            _hub2 = new Hub(_config, 4445);
        }

        public bool IsQueued()
        {
            return !_hub1.IsHubAvalible() && !_hub2.IsHubAvalible();
        }

        public string GetAvailbleHub(bool turnOn = true)
        {
            if (!turnOn)
                return _hub1.GetHubUrl();
            
            while (IsQueued())
                Thread.Sleep(TimeSpan.FromMilliseconds(100));

            if (_hub1.GetHubFreeNodes() > _hub2.GetHubFreeNodes())
                return _hub1.GetHubUrl();
            return _hub2.GetHubUrl();
        }
    }
}
