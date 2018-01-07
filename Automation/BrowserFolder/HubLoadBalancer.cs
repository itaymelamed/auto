using System;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        string _hub1;
        string _hub2;
        Configurations _config;
        static readonly object _syncObject = new object();
        bool Balancer { get; set; } 

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _hub1 = $"http://{_config.GetIp(0)}:4444/wd/hub";
            _hub2 = _hub1 = $"http://{_config.GetIp(1)}:4444/wd/hub";
            Balancer = true;
        }

        public string GetAvailbleHub()
        {
            lock(_syncObject)
            {
                Balancer = !Balancer;
                return Balancer ? _hub1 : _hub2;
            }
        }
    }
}
