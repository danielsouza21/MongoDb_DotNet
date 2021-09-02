using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbCourseExamples
{
    public class Book
    {
        private const char SUBJECT_SEPARATOR = ',';

        public Book()
        {
        }

        public Book(string title, string author, int year, int pages, string subject)
        {
            Title = title;
            Author = author;
            Year = year;
            Pages = pages;

            var subjectsList = subject.Split(SUBJECT_SEPARATOR).ToList();
            Subject = subjectsList.Select(s => s.Trim()).ToList();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public List<string> Subject { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Title: {Title} - Author: {Author} - Year: {Year} - Pages: {Pages}";
        }
    }
}
