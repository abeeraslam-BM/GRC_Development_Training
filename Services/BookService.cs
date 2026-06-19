using Day1Task1.Data;
using Day1Task1.Models;

namespace Day1Task1.Services;

public class BookService : IBookService
{
    public IEnumerable<Book> GetAll()
    {
        return InMemoryStore.Books;
    }

    public Book? GetById(int id)
    {
        return InMemoryStore.Books.FirstOrDefault(b => b.Id == id);
    }

    public Book Create(Book book)
    {
        int nextId = InMemoryStore.Books.Count == 0
            ? 1
            : InMemoryStore.Books.Max(b => b.Id) + 1;

        book.Id = nextId;
        book.Author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == book.AuthorId);

        InMemoryStore.Books.Add(book);
        book.Author?.Books.Add(book);

        return book;
    }

    public bool Update(Book book)
    {
        var existing = GetById(book.Id);
        if (existing is null)
            return false;

        existing.Title = book.Title;
        existing.Year = book.Year;
        existing.PageCount = book.PageCount;
        existing.AuthorId = book.AuthorId;
        existing.Author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == book.AuthorId);

        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null)
            return false;

        InMemoryStore.Books.Remove(existing);
        existing.Author?.Books.Remove(existing);

        return true;
    }
}