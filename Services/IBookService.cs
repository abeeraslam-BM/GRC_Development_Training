using Day1Task1.Models;

namespace Day1Task1.Services;

public interface IBookService
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book Create(Book book);
    bool Update(Book book);
    bool Delete(int id);
}