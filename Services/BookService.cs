using Day1Task1.Data;
using Day1Task1.Models;
using Day1Task1.Exceptions;

namespace Day1Task1.Services;

public class BookService : IBookService
{
    
    public Task<List<Book>> GetAllAsync()
    {
        return Task.FromResult(InMemoryStore.Books);
    }
    public Task<Book?> GetByIdAsync(int id)
    {
    var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);
    if (book == null)
    {
        throw new BookNotFoundException(id);
    }
    return Task.FromResult<Book?>(book);
    }

    public Task<Book> CreateAsync(Book book)
    {
    book.Id =InMemoryStore.Books.Max(b => b.Id) + 1;

    InMemoryStore.Books.Add(book);

    return Task.FromResult(book);
    }

    public Task<Book?> UpdateAsync(int id, Book book)
    {
         var existingBook =
        InMemoryStore.Books.FirstOrDefault(b => b.Id == id);

    if (existingBook == null)
        return Task.FromResult<Book?>(null);

    existingBook.Title = book.Title;

    return Task.FromResult<Book?>(existingBook);
    }
    public Task<bool> DeleteAsync(int id)
    {
    var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);

    if (book == null)
        return Task.FromResult(false);

    InMemoryStore.Books.Remove(book);

    return Task.FromResult(true);
    }
}