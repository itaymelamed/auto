using System;
using Automation.TestsFolder;

namespace Automation.Helpersobjects
{
    public class AdsUnitMailer :AdsTxtMailer
    {
        string _template;

        public AdsUnitMailer(string to, string template)
            :base(to)
        {
            _template = template;
        }

		public override void SendEmail(string errors)
		{
            _url = $"http://{_host}:32002/users/send?msg=<div><b><h2 style='color: red;'>{_brand} | {Base._config.BrowserT} | {_template} | The%20following%20errors%20were%20found%20in%20the%20Ad%20Request:</h2></b></div>{errors}&to={To}&subject={_brand} | {_template} | Automation%20|%20{Base._config.BrowserT} | %20Google%20Ads%20DFP%20-Tests%20failed";
            _api.GetRequestVoid(_url);
		}
	}
}
