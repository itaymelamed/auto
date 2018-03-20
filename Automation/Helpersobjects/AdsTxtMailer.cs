using Automation.ApiFolder;
using Automation.TestsFolder;

namespace Automation.Helpersobjects
{
    public class AdsTxtMailer
    {
        public string To;
        protected string _host;
        protected string _brand;
        protected ApiObject _api;
        protected string _url;

        public AdsTxtMailer(string to)
        {
            _api = new ApiObject();
            _host = Base._config.Host;
            _brand = Base._config.SiteName;
            To = to;
        }

        public virtual void SendEmail(string errors)
        {
            _url = $"http://{_host}:32002/users/send?msg=<div><b><h2 style='color: red;'>{_brand} - The%20following%20errors%20were%20found%20in%20the%20ads.txt%20file:</h2></b></div>{errors}&to={To}&subject={_brand} | Automation%20Google%20Ads.txt%20file%20-Tests%20failed";
            _api.GetRequestVoid(_url);
        }
    }
}
