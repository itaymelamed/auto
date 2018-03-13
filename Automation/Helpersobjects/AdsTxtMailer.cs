﻿using Automation.ApiFolder;
using Automation.TestsFolder;

namespace Automation.Helpersobjects
{
    public class AdsTxtMailer
    {
        public string To;
        string _host;
        string _brand;
        ApiObject _api;

        public AdsTxtMailer(string to)
        {
            _api = new ApiObject();
            _host = Base._config.Host;
            _brand = Base._config.SiteName;
            To = to;
        }

        public void SendEmail(string errors)
        {
            _api.GetRequestVoid($"http://{_host}:32002/users/send?msg=<div><b><h2 style='color: red;'>{_brand} - The%20following%20errors%20were%20found%20in%20the%20ads.txt%20file:</h2></b></div>{errors}</br></br>&to={To}&subject={_brand} | Automation%20Google%20Ads.txt%20file%20-Tests%20failed");
        }
    }
}
