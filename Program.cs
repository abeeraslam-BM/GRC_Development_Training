// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// app.Run();
using Day1Task1.Queries;
using Day1Task1.Models;
using Day1Task1.Services;

Console.WriteLine("=== TASK 1.3: LINQ Query Demos ===");
Linq.RunAll();

Console.WriteLine("\n\n=== TASK 1.5: BookService Demo ===");

IBookService bookService = new BookService();

Console.WriteLine("\n-- GetAll --");
foreach (var b in bookService.GetAll())
    Console.WriteLine($"  [{b.Id}] {b.Title}");

Console.WriteLine("\n-- GetById(3) --");
var found = bookService.GetById(3);
Console.WriteLine(found is null ? "  not found" : $"  {found.Title}");

Console.WriteLine("\n-- Create new book --");
var created = bookService.Create(new Book
{
    Title = "Foundation and Empire",
    Year = 1952,
    PageCount = 247,
    AuthorId = 2
});
Console.WriteLine($"  Created with new Id = {created.Id}: {created.Title}");

Console.WriteLine("\n-- Update book Id = 1 (change page count) --");
var updateResult = bookService.Update(new Book
{
    Id = 1,
    Title = "The Hobbit",
    Year = 1937,
    PageCount = 320,
    AuthorId = 1
});
Console.WriteLine($"  Update succeeded: {updateResult}");
Console.WriteLine($"  New page count: {bookService.GetById(1)?.PageCount}");

Console.WriteLine("\n-- Delete book Id = 8 --");
var deleteResult = bookService.Delete(8);
Console.WriteLine($"  Delete succeeded: {deleteResult}");
Console.WriteLine($"  Total books remaining: {bookService.GetAll().Count()}");