using CosmosBooksFunctions.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosBooksFunctions.Services
{
    public class BookService : IBookService
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Book> _booksCollection;

        public BookService(
            MongoClient mongoClient,
            IConfiguration configuration)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(configuration[Constants.DATABASE_NAME_CONFIG_INDEX]);
            _booksCollection = _database.GetCollection<Book>(configuration[Constants.COLLECTION_NAME_CONFIG_INDEX]);
        }

        public async Task CreateBook(Book bookIn)
        {
            await _booksCollection.InsertOneAsync(bookIn);
        }

        public async Task<Book> GetBook(string id)
        {
            var book = await _booksCollection.FindAsync(book => book.Id == id);
            return book.FirstOrDefault();
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = await _booksCollection.FindAsync(book => true);
            return books.ToList();
        }

        public async Task RemoveBook(Book bookIn)
        {
            await _booksCollection.DeleteOneAsync(book => book.Id == bookIn.Id);
        }

        public async Task RemoveBookById(string id)
        {
            await _booksCollection.DeleteOneAsync(book => book.Id == id);
        }

        public async Task UpdateBook(string id, Book bookIn)
        {
            await _booksCollection.ReplaceOneAsync(book => book.Id == id, bookIn);
        }
    }
}