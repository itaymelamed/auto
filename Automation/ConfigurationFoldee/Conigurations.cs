﻿using NUnit.Framework;
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
            UTest,
            Testo,
            Demo,
            QA1,
            QA2,
            QA3,
            QA4,
            QA5,
            QA6,
            QA7,
            QA8,
            QA9,
            QA10,
            QA11,
            QA12
        }

        public enum BrowserType
        {
            Desktop,
            Chrome,
            FireFox,
            Safari,
            SafariIos,
            ChromeAndroid
        }

        public Configurations()
        {
            Host = GetHost();
            Local = Environment.MachineName.Replace("-", " ").Replace(".", " ").Contains("local");
            MongoDbConnectionString = $"mongodb://{Host}:32001";
            _mongoDb = new MongoDb("Configurations");
			Env = GetEnvType();
            SiteName = GetSiteName();
            BrowserT = BrowserType.Desktop;
            GlobalConfigObject = BsonSerializer.Deserialize<BsonDocument>(GetGlobalConfig() as BsonDocument);
            ConfigObject = BsonSerializer.Deserialize<ConfigObject>(GetConfigJson(SiteName) as BsonDocument);
            ApiConfig = GetConfig<ApiConfig>("ApiConfig");
            FacebookApiConfig = GetConfig<FacebookApiConfig>("FacebookApiConfig");
            Url = $"http://{Env}.{ConfigObject.Url}";
        }

        static Enviroment GetEnvType()
        {
            string env = TestContext.Parameters.Get("env", Enviroment.UTest.ToString());
            return (Enviroment)Enum.Parse(typeof(Enviroment), env);
        }

        static string GetSiteName()
        {
            return TestContext.Parameters.Get("siteName", "90Min");
        }

        static string GetParams(string param)
        {
            return TestContext.Parameters.Get(param, "QA6");
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