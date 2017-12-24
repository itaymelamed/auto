﻿using NUnit.Framework;
using System;
using Automation.ConfigurationFoldee.ConfigurationsJsonObject;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;
using Automation.MongoDbObject;

namespace Automation.ConfigurationFolder
{
    public class Configurations
    {
        public ConfigObject ConfigObject { get; }
        public Enviroment Env { get; }
        public BrowserType BrowserT { get; }
        public string SiteName { get; }
        public string Url { get; }
        public string Ip { get; }
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
            Local = Environment.MachineName.Replace("-", " ").Replace(".", " ").Contains("local");
            Ip = GetIp();
            MongoDbConnectionString = $"mongodb://{Ip}:27017";
            _mongoDb = new MongoDb("Configurations");
			Env = GetEnvType();
            SiteName = GetSiteName();
            BrowserT = BrowserType.Desktop;
            ConfigObject = BsonSerializer.Deserialize<ConfigObject>(GetConfigJson(SiteName) as BsonDocument);
            Url = $"http://{Env}.{ConfigObject.Url}";
        }

        static Enviroment GetEnvType()
        {
            string env = TestContext.Parameters.Get("env", Enviroment.UTest.ToString());
            return (Enviroment)Enum.Parse(typeof(Enviroment), env);
        }

        static string GetSiteName()
        {
            return TestContext.Parameters.Get("siteName", "12Up");
        }

        static string GetParams(string param)
        {
            return TestContext.Parameters.Get(param, "UTest");
        }

        BsonValue GetConfigJson(string siteName)
        {
            return _mongoDb.GetConfig(siteName);
        }

        string GetIp()
        {
            var ipTxtLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigurationFoldee/ConfigurationsJsonObject/Ip.txt");
            string ip = File.ReadAllText(ipTxtLocation);
            return ip;
        }
    }
}
