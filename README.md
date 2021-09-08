# Creating a Web API with Azure Functions, Azure Cosmos DB MongoDB API

Developing a API with Azure Functions that uses a Cosmos DB MongoDB API. 

Main operations (CRUD) were implemented. 
The mapped database is just a collection of books.

Shown below are all endpoints / functions when running locally:

        CreateBook: [POST] http://localhost:7071/api/Book
        DeleteBook: [DELETE] http://localhost:7071/api/Book/{id}
        GetAllBooks: [GET] http://localhost:7071/api/Books
        GetBookById: [GET] http://localhost:7071/api/Book/{id}
        UpdateBook: [PUT] http://localhost:7071/api/Book/{id}

Example of body when creating a new book in the database:

```json
{
  "BookName" : "Clean Code: A Handbook of Agile Software Craftsmanship",
  "Price": 202.71,
  "Category": "Technology",
  "Author": "Robert C. Martin"
}
```

