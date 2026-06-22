using Day1Task1.Models;

namespace Day1Task1.Services;

public interface IBookService
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<Book> CreateAsync(Book book);

    Task<Book?> UpdateAsync(int id, Book book);

    Task<bool> DeleteAsync(int id);
}