using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbCourseExamples
{
    public class MongoDbClient
    {
        private const string MONGODB_CONNECTION_STRING = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "Biblioteca";
        private const string COLLECTION_NAME = "Livros";

        private readonly IMongoClient MongoClient;
        private readonly IMongoDatabase LibraryDatabase;
        private readonly IMongoCollection<BsonDocument> BookCollection;

        public MongoDbClient()
        {
            MongoClient = new MongoClient(MONGODB_CONNECTION_STRING);
            LibraryDatabase = MongoClient.GetDatabase(DATABASE_NAME);
            BookCollection = LibraryDatabase.GetCollection<BsonDocument>(COLLECTION_NAME);
        }

        public async Task AccessServerAsync()
        {
            var doc = GetBsonDocument();
            await BookCollection.InsertOneAsync(doc);

            System.Console.WriteLine($"Document Included");
        }

        private BsonDocument GetBsonDocument()
        {
            var doc = new BsonDocument
            {
                {
                    "Titulo", "Guerra dos Tronos"
                }
            };

            doc.Add("Autor", "Dandara");
            doc.Add("Ano", 1234);
            doc.Add("Paginas", 856);

            var assuntoArray = new BsonArray();
            assuntoArray.Add("Fantasia");
            assuntoArray.Add("Acao");

            doc.Add("Assunto", assuntoArray);

            return doc;
        }
    }
}
