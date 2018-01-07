using System;
using Automation.ConfigurationFolder;
using Automation.TestsFolder;

namespace Automation.BrowserFolder
{
    public class HubLoadBalancer
    {
        public static bool _hub { get; set; }
        Configurations _config;
        static readonly object _syncObject = new object();

        public HubLoadBalancer(Configurations config)
        {
            _config = config;
            _hub = true;
        }

        public static void UpdateHubs()
        {
            _hub = _hub ? false : true;
        }

        public static bool GetHub()
        {
            return _hub;
        }
    }
}
