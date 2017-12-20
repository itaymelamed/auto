﻿using Automation.ConfigurationFolder;
using Automation.MongoDbObject;
using Automation.TestsFolder;
using Automation.TestsObjects;
using MongoDB.Bson;

namespace Automation.TestsObject
{
    public class Params
    {
        BsonValue _context;
        MongoDb _mongoDb;

        public Params(Test test, Configurations config)
        {
            _mongoDb = new MongoDb("TestCases");
            _context = _mongoDb.GetParams(test.TestNumber)["Params"];
        }

        public BsonValue GetParams()
        {
            return _context["AllBrands"] != "" ? _context["AllBrands"] : _context[Base._testRun.SiteName];
        }
    }
}
