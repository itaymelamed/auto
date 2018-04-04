using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Automation.TestsObjects;
using System.Linq;
using NUnit.Framework.Internal;
using Automation.ConfigurationFolder;
using System;

namespace Automation.MongoDbObject
{
    public class MongoDb
    {
        MongoClient _client;
        IMongoDatabase _database;
        static readonly object _syncObject1 = new object();
        static readonly object _syncObject2 = new object();
        static readonly object _syncObject3 = new object();
        static readonly object _syncObject4 = new object();
        static readonly object _syncObject5 = new object();
        static readonly object _syncObject6 = new object();

        public MongoDb(string db)
        {
            _client = new MongoClient(Configurations.MongoDbConnectionString);
            _database = _client.GetDatabase(db);
        }

        public List<BsonDocument> GetAllDocuments(string collectionName)
        {
            return _database.GetCollection<BsonDocument>(collectionName).AsQueryable().ToList();
        }

        public BsonValue GetConfig(string siteName)
		{
            return _database.GetCollection<BsonDocument>("Configurations").AsQueryable().First()[siteName];
		}

        public void InsertTest(TestsObjects.Test test)
        {
            lock(_syncObject1)
            {
                BsonDocument document = BsonDocument.Parse(JsonConvert.SerializeObject(test));
                UpdateOptions options = new UpdateOptions();
                options.IsUpsert = true;
                var filter = Builders<BsonDocument>.Filter.Where(x => x["TestNumber"] == test.TestNumber);
                var collection = _database.GetCollection<BsonDocument>($"testRun{test.TestRunId}");
                collection.ReplaceOne(filter, document, options);
            }
        }

        public void InserTestRun(TestRun testRun)
        {
            var collection = _database.GetCollection<BsonDocument>("Runs");

            BsonDocument document = BsonDocument.Parse(JsonConvert.SerializeObject(testRun));
            UpdateOptions options = new UpdateOptions();
            options.IsUpsert = true;
            var filter = Builders<BsonDocument>.Filter.Where(x => x["TestRunId"] == testRun.TestRunId);

            collection.ReplaceOne(filter, document, options);
        }

        public void UpdateResult(TestsObjects.Test test)
        {
            lock(_syncObject2)
            {
                var collection = _database.GetCollection<BsonDocument>($"testRun{test.TestRunId}");
                var filter = Builders<BsonDocument>.Filter.Where(x => x["TestNumber"] == test.TestNumber);
                var update = Builders<BsonDocument>.Update.Set(x => x["Result"], BsonDocument.Parse(JsonConvert.SerializeObject(test.Result)));

                var updateStatus = collection.UpdateOne(filter, update); 
            }
        }


        public void UpdateDuration(TestRun testRun)
        {
            var duration = DateTime.ParseExact(DateTime.Now.AddHours(2).ToString("dd/MM HH:mm"), "dd/MM HH:mm", null) - DateTime.ParseExact(testRun.Date, "dd/MM HH:mm", null);
            testRun.Duration = duration.Duration().ToString();

            var collection = _database.GetCollection<BsonDocument>("Runs");
            var filter = Builders<BsonDocument>.Filter.Where(x => x["TestRunId"] == testRun.TestRunId);
            var update = Builders<BsonDocument>.Update.Set(x => x["Duration"], testRun.Duration);

            var updateDuration = collection.UpdateOne(filter, update);
        }

        public void UpdateSteps(TestsObjects.Test test)
        {
            lock(_syncObject3) 
            {
                var collection = _database.GetCollection<BsonDocument>($"testRun{test.TestRunId}");

                var updateSteps = collection.UpdateOne(
                    Builders<BsonDocument>.Filter.Where(x => x["TestNumber"] == test.TestNumber),
                    Builders<BsonDocument>.Update.Push("Steps", test.Steps.Last()));
            }
        }

        public void UpdateSteps(string step)
        {
            lock (_syncObject4)
            {
                var test = (TestExecutionContext.CurrentContext.CurrentTest.Properties.Get("Test")) as TestsObjects.Test;
                test.Steps.Add(step);
                var collection = _database.GetCollection<BsonDocument>($"testRun{test.TestRunId}");

                var updateSteps = collection.UpdateOne(
                    Builders<BsonDocument>.Filter.Where(x => x["TestNumber"] == test.TestNumber),
                    Builders<BsonDocument>.Update.Push("Steps", test.Steps.Last()));
            }
        }

        public void UpdateTestRunResults(TestRun testRun)
        {
            var collection = _database.GetCollection<BsonDocument>("Runs");
            var filter = Builders<BsonDocument>.Filter.Where(x => x["TestRunId"] == testRun.TestRunId);
            var update = Builders<BsonDocument>.Update.Set(x => x["Results"], BsonDocument.Parse(JsonConvert.SerializeObject(testRun.Results)));
            var updateStatus = collection.UpdateOne(filter, update);
        }

        public static BsonDocument GetTestParams(string testCaseId, string siteName)
        {
            lock(_syncObject5) 
            {
                MongoDb mdb = new MongoDb("Tests");
                return mdb.GetParams(testCaseId);
            }
        }

        public BsonDocument GetParams(string testCaseId)
        {
            lock(_syncObject6)
            {
                return _database.GetCollection<BsonDocument>($"TestCases")
                               .Find(x => x["TestCaseId"] == testCaseId).FirstOrDefault();
            }
        }

        public void UpdateAdsTxtResults(TestsObjects.Test test)
        {
            var collection = _database.GetCollection<BsonDocument>("AdsTxt");

            BsonDocument document = BsonDocument.Parse(JsonConvert.SerializeObject(test));
            UpdateOptions options = new UpdateOptions();
            options.IsUpsert = true;
            var filter = Builders<BsonDocument>.Filter.Where(x => x["TestName"] == "Ads | Validate Ads Txt File");

            collection.ReplaceOne(filter, document, options);
        }
    }
}
