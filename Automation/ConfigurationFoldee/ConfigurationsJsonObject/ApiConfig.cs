using Automation.TestsFolder;
using static Automation.ApiFolder.JsonHelper;

namespace Automation.ConfigurationFoldee.ConfigurationsJsonObject
{
    public class ApiConfig
    {
        public string Feed { get; set; }
        public string Category { get; set; }
        public string League { get; set; }

        public string GetFeedUrl(int team)
        {
            string baseUrl = $"{Base._config.Url}/{Feed}";
            string languageUrl = baseUrl.Replace("{language}", Base._config.ConfigObject.Language);
            string teamUrl = languageUrl.Replace("{team}", team.ToString());

            return teamUrl;
        }

        public string GetFeedUrlOtherBrand(string language, string brand, int team)
        {
            string baseUrl = $"http://{brand}.com/{Feed}";
            string languageUrl = baseUrl.Replace("{language}", language);
            string teamUrl = languageUrl.Replace("{team}", team.ToString());

            return teamUrl;
        }

        public string GetCategoryUrl(string category)
        {
            string baseUrl = $"{Base._config.Url}/{Category}";
            string languageUrl = baseUrl.Replace("{language}", Base._config.ConfigObject.Language);
            string categoryUrl = languageUrl.Replace("{category}", category);

            return categoryUrl;
        }

        public string GetLeagueUrl(Leagues league)
        {
            string baseUrl = $"{Base._config.Url}/{League}";
            string languageUrl = baseUrl.Replace("{language}", Base._config.ConfigObject.Language);
            string leagueUrl = languageUrl.Replace("{league}", ((int)league).ToString());

            return leagueUrl;
        }
    }
}