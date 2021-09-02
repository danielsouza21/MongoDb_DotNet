using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbCourseExamples
{
    public class MongoDbClient<T>
    {
        private const string MONGODB_CONNECTION_STRING = "mongodb://localhost:27017";

        private readonly IMongoClient MongoClient;
        private readonly IMongoDatabase LibraryDatabase;
        private readonly IMongoCollection<T> BookCollection;

        public MongoDbClient(string databaseName, string collectionName)
        {
            MongoClient = new MongoClient(MONGODB_CONNECTION_STRING);

            LibraryDatabase = MongoClient.GetDatabase(databaseName);
            BookCollection = LibraryDatabase.GetCollection<T>(collectionName);
        }

        public async Task InsertAsync(T element)
        {
            await BookCollection.InsertOneAsync(element);
            Console.WriteLine($"Document Included");
        }

        public async Task InsertManyAsync(List<T> element)
        {
            await BookCollection.InsertManyAsync(element);
            Console.WriteLine($"Many Documents Included - Count: {element.Count}");
        }

        public async Task<List<T>> GetAllAsync()
        {
            var elements = await BookCollection.Find(new BsonDocument()).ToListAsync();
            return elements;
        }

        public bool CheckIfIsEmptyCollection()
        {
            var elements = BookCollection.Find(new BsonDocument()).ToList();
            return elements.Any();
        }
    }
}
