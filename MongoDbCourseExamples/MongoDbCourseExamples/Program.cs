using System.Threading.Tasks;

namespace MongoDbCourseExamples
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var mongoClient = new MongoDbClient();
            await mongoClient.AccessServerAsync();
        }
    }
}
