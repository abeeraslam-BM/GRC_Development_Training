using Day1Task1.Models;
namespace Day1Task1.Data;

public static class InMemoryStore
{
    public static List<Author> Authors { get; set; } = new();
    public static List<Book> Books { get; set; } = new();
    public static List<Tag> Tags { get; set; } = new();
    public static List<BookTag> BookTags { get; set; } = new();

    static InMemoryStore()
    {
        Seed();
    }

    private static void Seed()
    {
        //3 Authors
        var tolkien = new Author { Id = 1, Name = "J.R.R. Tolkien" };
        var asimov = new Author { Id = 2, Name = "Isaac Asimov" };
        var christie = new Author { Id = 3, Name = "Agatha Christie" };

        Authors.AddRange(new[] { tolkien, asimov, christie });

        //8 Books
        var books = new List<Book>
        {
            new() { Id = 1, Title = "The Hobbit",                  Year = 1937, PageCount = 310, AuthorId = tolkien.Id },
            new() { Id = 2, Title = "The Fellowship of the Ring",  Year = 1954, PageCount = 423, AuthorId = tolkien.Id },
            new() { Id = 3, Title = "The Two Towers",              Year = 1954, PageCount = 352, AuthorId = tolkien.Id },
            new() { Id = 4, Title = "Foundation",                  Year = 1951, PageCount = 255, AuthorId = asimov.Id },
            new() { Id = 5, Title = "I, Robot",                    Year = 1950, PageCount = 224, AuthorId = asimov.Id },
            new() { Id = 6, Title = "The Caves of Steel",          Year = 1954, PageCount = 206, AuthorId = asimov.Id },
            new() { Id = 7, Title = "Murder on the Orient Express",Year = 1934, PageCount = 256, AuthorId = christie.Id },
            new() { Id = 8, Title = "And Then There Were None",    Year = 1939, PageCount = 272, AuthorId = christie.Id },
        };

        Books.AddRange(books);

        //navigation properties both directions
        foreach (var book in Books)
        {
            book.Author = Authors.First(a => a.Id == book.AuthorId);
            book.Author.Books.Add(book);
        }

        //4 Tags
        Tags.AddRange(new[]
        {
            new Tag { Id = 1, Name = "Fantasy" },
            new Tag { Id = 2, Name = "Science Fiction" },
            new Tag { Id = 3, Name = "Mystery" },
            new Tag { Id = 4, Name = "Classic" },
        });

        //6 links BookTags
        BookTags.AddRange(new[]
        {
            new BookTag { BookId = 1, TagId = 1 }, 
            new BookTag { BookId = 2, TagId = 1 }, 
            new BookTag { BookId = 1, TagId = 4 }, 
            new BookTag { BookId = 4, TagId = 2 }, 
            new BookTag { BookId = 5, TagId = 2 }, 
            new BookTag { BookId = 7, TagId = 3 }, 
        });

        foreach (var bt in BookTags)
        {
            bt.Book = Books.First(b => b.Id == bt.BookId);
            bt.Tag = Tags.First(t => t.Id == bt.TagId);
        }
    }
}