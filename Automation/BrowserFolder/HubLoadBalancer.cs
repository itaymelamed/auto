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
        List<Test> _hub1List;
        List<Test> _hub2List;
        private readonly EventWaitHandle waitHandle = new AutoResetEvent(false);
        private readonly static object _synObject = new object(); 

        public HubLoadBalancer(Configurations config)
        {
            _hub1List = new List<Test>();
            _hub2List = new List<Test>();
            _config = config;
        }

        public bool IsQueue()
        {
            return _hub1List.Count >= 8 && _hub1List.Count >= 8;
        }

        void InsertTestToHub(Test test, int i)
        {
            if (i == 0)
                _hub1List.Add(test);
            else
                _hub2List.Add(test);
        }

        public void CleanTestFromHub(Test test)
        {
            if (_hub1List.Contains(test))
                _hub1List.Remove(test);
            else
                _hub2List.Remove(test);
        }

        public string GetAvailbleHub(Test test)
        {
            lock(_synObject)
            {
                if (IsQueue())
                {
                    waitHandle.WaitOne(TimeSpan.FromMinutes(30), !IsQueue());
                }
            }

            string avHub = _hub1List.Count <= _hub2List.Count ? $"http://{_config.GetIp(0)}:4444/wd/hub" : $"http://{_config.GetIp(1)}:4444/wd/hub";
            if (_hub1List.Count <= _hub2List.Count)
            {
                avHub = $"http://{_config.GetIp(0)}:4444/wd/hub";
                InsertTestToHub(test, 0);
                return avHub;
            }

            avHub = $"http://{_config.GetIp(1)}:4444/wd/hub";
            InsertTestToHub(test, 1);
            return avHub;
        }

        //public string GetAvalibleHub()
        //{
        //    if (int.Parse(_hub1Url) > 0 && int.Parse(_hub1Url) <= 8 && _hub1Count <= 8 && _hub1Count >= _hub2Count)
        //    {
        //        _hub1Count++;
        //        return $"http://{_config.GetIp(0)}:4444/wd/hub";
        //    }

        //    else
        //    {
        //        _hub2Count++;
        //        return $"http://{_config.GetIp(1)}:4444/wd/hub";
        //    }
        //}
    }
}
