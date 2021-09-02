using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbCourseExamples
{
    public class Program
    {
        private const string DATABASE_NAME = "Biblioteca";
        private const string COLLECTION_NAME = "Livros";

        static async Task Main(string[] args)
        {
            var mongoClient = new MongoDbClient<Book>(DATABASE_NAME, COLLECTION_NAME);

            if (!mongoClient.CheckIfIsEmptyCollection())
            {
                Console.WriteLine("Adding data to the database");

                var book = new Book("TestTitle", "TestAuthor", 2020, 800, "Assunto1, Assunto2, Assunto3");
                var taskInsertOne = mongoClient.InsertAsync(book);

                var books = new List<Book>()
            {
                new Book("TestTitle1", "TestAuthor1", 2021, 700, "Assunto1, Assunto2, Assunto3"),
                new Book("TestTitle2", "TestAuthor2", 2021, 600, "Assunto1, Assunto2, Assunto3"),
                new Book("TestTitle3", "TestAuthor3", 2021, 500, "Assunto1, Assunto2, Assunto3")
            };

                var taskInsertMany = mongoClient.InsertManyAsync(books);

                Task.WaitAll(taskInsertOne, taskInsertMany);
            }

            var allBooks = await mongoClient.GetAllAsync();
            allBooks.ForEach(e => Console.WriteLine(e.ToString()));
        }
    }
}
