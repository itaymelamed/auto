using System.Linq;
using System.Text.RegularExpressions;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using Automation.TestsFolder;
using Newtonsoft.Json.Linq;

namespace Automation.ApiFolder.FacebookApi
{
    public class FacebookClient
    {
        readonly ApiObject _apiObject;
        readonly FacebookApiConfig _facebookApiConfig;
        readonly string _accessToken;

        public FacebookClient()
        {
            _apiObject = new ApiObject();
            _facebookApiConfig = Base._config.FacebookApiConfig;
            _accessToken = _facebookApiConfig.Token;
        }

        JObject Get(string endpoint, string args = null)
        {
            return _apiObject.GetRequest($"{_facebookApiConfig.Url}/{endpoint}?access_token={_accessToken}&{args}");
        }

        JObject GetGroupPosts()
        {
            return Get($"{_facebookApiConfig.GroupId}/feed");
        }

        public bool SearchPost(string postTitle)
        {
            Base.MongoDb.UpdateSteps("Validate post creation on Facebook API.");
            var posts = GetGroupPosts()["data"];
            JsonHelper jsonHelper = new JsonHelper();
            return jsonHelper.WaitUntill(() => posts.Any(p =>  Regex.Replace(p["message"].ToString().Replace('-', ' ').ToLower(), @"[\d-]", string.Empty)  == postTitle), 60);
        }
    }
}