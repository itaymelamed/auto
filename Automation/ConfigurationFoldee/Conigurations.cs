using NUnit.Framework;
using System;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;
using Automation.MongoDbObject;
using System.Linq;

namespace Automation.ConfigurationFolder
{
    public class Configurations
    {
        public ConfigObject ConfigObject { get; }
        public ApiConfig ApiConfig { get; }
        public FacebookApiConfig FacebookApiConfig { get; }
        public BsonDocument GlobalConfigObject { get; }
        public Enviroment Env { get; }
        public BrowserType BrowserT { get; }
        public string SiteName { get; }
        public string Url { get; }
        public string Host { get; }
        public bool Local { get; }
        public static string MongoDbConnectionString { get; set; }
        MongoDb _mongoDb;

        public enum Enviroment
        {
            utest,
            testo,
            demo,
            qa,
            qa1,
            qa2,
            qa3,
            qa4,
            qa5,
            qa6,
            qa7,
            qa8,
            qa9,
            qa10,
            qa11,
            qa12,
            Production
        }

        public enum BrowserType
        {
            Desktop,
            Mobile,
            Chrome,
            FireFox,
            Safari,
            SafariIos,
            ChromeAndroid
        }

        public Configurations()
        {
            Local = Environment.MachineName.Replace("-", " ").Replace(".", " ").Contains("local");
            Host = GetHost();
            MongoDbConnectionString = $"mongodb://{Host}:32001";
            _mongoDb = new MongoDb("Configurations");
			Env = GetEnvType();
            SiteName = GetSiteName();
            BrowserT = GetBrowserType();
            GlobalConfigObject = BsonSerializer.Deserialize<BsonDocument>(GetGlobalConfig() as BsonDocument);
            ConfigObject = BsonSerializer.Deserialize<ConfigObject>(GetConfigJson(SiteName) as BsonDocument);
            ApiConfig = GetConfig<ApiConfig>("ApiConfig");
            FacebookApiConfig = GetConfig<FacebookApiConfig>("FacebookApiConfig");
            Url = $"http://{Env}.{ConfigObject.Url}".Replace("Production", "www");
        }

        static BrowserType GetBrowserType()
        {
            string browserT = TestContext.Parameters.Get("browser", BrowserType.Desktop.ToString());
            return (BrowserType)Enum.Parse(typeof(BrowserType), browserT);
        }

        static Enviroment GetEnvType()
        {
            string env = TestContext.Parameters.Get("env", Enviroment.utest.ToString());
            return (Enviroment)Enum.Parse(typeof(Enviroment), env);
        }

        static string GetSiteName()
        {
            return TestContext.Parameters.Get("siteName", "90Min");
        }

        BsonValue GetConfigJson(string siteName)
        {
            return _mongoDb.GetConfig(siteName);
        }

        T GetConfig<T>(string prop)
        {
            return BsonSerializer.Deserialize<T>(GlobalConfigObject[prop] as BsonDocument);
        }

        BsonDocument GetGlobalConfig()
        {
            return _mongoDb.GetAllDocuments("Configurations").First();
        }

        string GetHost()
        {
            var hostTxt = "/host/ip.txt";
            string host = File.ReadAllText(hostTxt).Split(';').First();
            return host;
        }
    }
}