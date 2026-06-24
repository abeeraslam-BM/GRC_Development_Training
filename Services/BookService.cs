using Day1Task1.Data;
using Day1Task1.DTOs;
using Day1Task1.Exceptions;
using Day1Task1.Mappers;

namespace Day1Task1.Services;

public class BookService : IBookService
{
    public Task<List<BookResponseDTO>> GetAllAsync(string? author, int page, int pageSize)
    {
        var books = InMemoryStore.Books;

        if (!string.IsNullOrWhiteSpace(author))
        {
            books = books
                .Where(b => b.Author.Name.Contains(author))
                .ToList();
        }

        books = books
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = books
            .Select(b => BookMapper.ToResponse(b))
            .ToList();

        return Task.FromResult(response);
    }

    public Task<BookResponseDTO> GetByIdAsync(int id)
    {
        var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
            throw new BookNotFoundException(id);

        var response = BookMapper.ToResponse(book);

        return Task.FromResult(response);
    }

    public Task<BookResponseDTO> CreateAsync(BookCreateDTO dto)
    {
        var book = BookMapper.ToEntity(dto);

        book.Id = InMemoryStore.Books.Max(b => b.Id) + 1;

        InMemoryStore.Books.Add(book);

        var response = BookMapper.ToResponse(book);

        return Task.FromResult(response);
    }

    public Task<BookResponseDTO> UpdateAsync(int id, BookUpdateDTO dto)
    {
        try{
        var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
            throw new BookNotFoundException(id);

        BookMapper.ApplyUpdate(book, dto);

        var response = BookMapper.ToResponse(book);

        return Task.FromResult(response);
        }
        catch (BookNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");

            throw;
        }
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