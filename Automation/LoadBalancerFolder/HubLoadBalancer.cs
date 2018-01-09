using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.ConfigurationFolder;
using Automation.LoadBalancerFolder;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        Configurations _config;
        List<Hub> _hubs;
        int _hubsNum;

        static readonly object _syncObject = new object();

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _hubsNum = config.GlobalConfigObject["HubsNum"].AsInt32;
            _hubs = new List<Hub>();
            for (int i = 0; i < _hubsNum; i++)
            {
                _hubs.Add(new Hub(_config.GetIp(), 4444+i));
            }
        }

        public bool HubsFree()
        {
            return _hubs.All(h => h.IsHubAvalible());
        }

        public string GetAvailbleHub()
        {            
            WaitUntill(() => HubsFree());
            return _hubs.OrderByDescending(h => h.GetHubFreeNodes())
                        .FirstOrDefault().GetHubUrl();
        }

        void WaitUntill(Func<bool> func)
        {
            while (!func())
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }
    }
}